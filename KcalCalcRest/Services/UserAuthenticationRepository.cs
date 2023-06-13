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
	private User? _user;
	
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

		var result = await _userManager.CreateAsync(user, userRegistration.Password);
		return result;
	}

	public async Task<bool> ValidateUserAsync(UserLoginDTO userLogin) {
		_user = await _userManager.FindByEmailAsync(userLogin.Email);
		var result = _user != null && await _userManager.CheckPasswordAsync(_user, userLogin.Password);
		return result;
	}

	public async Task<string> CreateTokenAsync() {
		var signingCredentials = GetSigningCredentials();
		var claims = await GetClaims();
		var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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

	private async Task<List<Claim>> GetClaims() {
		var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, _user.UserName)
		};
		var roles = await _userManager.GetRolesAsync(_user);
		foreach (var role in roles) {
			claims.Add(new Claim(ClaimTypes.Role, role));
		}
		return claims;
	}

	private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) {
		var jwtSettings = _configuration.GetSection("JwtConfig");
		var tokenOptions = new JwtSecurityToken
		(
			issuer: jwtSettings["validIssuer"],
			audience: jwtSettings["validAudience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expirationInMinutes"])),
			signingCredentials: signingCredentials
		);
		return tokenOptions;
	}
}