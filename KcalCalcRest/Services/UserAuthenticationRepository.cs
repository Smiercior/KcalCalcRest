using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KcalCalcRest.Services; 

internal sealed class UserAuthenticationRepository : IUserAuthenticationRepository {
	private readonly UserManager<User> _userManager;
	private readonly IConfiguration _configuration;
	private readonly IMapper _mapper;
	
	public UserAuthenticationRepository(UserManager<User> userManager, IConfiguration configuration, IMapper mapper) {
		_userManager = userManager;
		_configuration = configuration;
		_mapper = mapper;
	}
	
	public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistration) {
		// Check if userRegistration.Password == userRegistration.Password2.
		if (userRegistration.Password != userRegistration.Password2) {
			return IdentityResult.Failed(new IdentityError {
				Code = "PasswordMismatch",
				Description = "Passwords do not match"
			});
		}
		
		var user = _mapper.Map<User>(userRegistration);
		user.JoinedAt = DateTime.UtcNow;

		var result = await _userManager.CreateAsync(user, userRegistration.Password!);
		return result;
	}

	private async Task<bool> ValidateUserAsync(UserLoginDTO userLogin) { // TODO: remove this
		var user = await _userManager.FindByEmailAsync(userLogin.Email!);
		if (user == null) {
			
		}
		var result = user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password!);
		return result;
	}

	public async Task<string?> CreateTokenAsync(string email) {
		var user = await _userManager.FindByEmailAsync(email);
		if (user is null) {
			return null;
		}
		
		var signingCredentials = GetSigningCredentials();
		var claims = await GetClaims(user);
		
		var jwtSettings = _configuration.GetSection("JwtConfig");
		var securityToken = new JwtSecurityToken(
			issuer: jwtSettings["validIssuer"],
			audience: jwtSettings["validAudience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expirationInMinutes"])),
			signingCredentials: signingCredentials
		);
		return new JwtSecurityTokenHandler().WriteToken(securityToken);
	}

	public Task<User?> GetUserAsync(string email) {
		return _userManager.FindByEmailAsync(email);
	}

	private SigningCredentials GetSigningCredentials() {
		var jwtConfig = _configuration.GetSection("JwtConfig");
		var key = Encoding.UTF8.GetBytes(jwtConfig["secret"]!);
		var secret = new SymmetricSecurityKey(key);
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}

	private async Task<List<Claim>> GetClaims(User user) {
		var claims = new List<Claim>();
		if (user.UserName is not null) {
			claims.Add(new Claim(ClaimTypes.Name, user.UserName));
		}
		if (user.Email is not null) {
			claims.Add(new Claim(ClaimTypes.Email, user.Email));
		}
		var roles = await _userManager.GetRolesAsync(user);
		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
		return claims;
	}
}