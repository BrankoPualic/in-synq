﻿using InSynq.Common;
using InSynq.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nexus.Core.Service;

public class TokenService : ITokenService
{
	public string GenerateJwtToken(long userId, string[] roles, string username, string email)
	{
		var tokenHandler = new JwtSecurityTokenHandler();

		var jwtSecrets = new
		{
			Key = Encoding.UTF8.GetBytes(Settings.JwtKey),
			Settings.Issuer,
			Settings.Audience,
		};

		var claims = new List<Claim>
		{
			new(Constants.CLAIM_ID, userId.ToString()),
			new(JwtRegisteredClaimNames.Iss, jwtSecrets.Issuer!),
			new(Constants.CLAIM_USERNAME, username),
			new(Constants.CLAIM_EMAIL, email)
		};

		claims.AddRange(roles.Select(role => new Claim(Constants.CLAIM_ROLES, role)));

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Issuer = jwtSecrets.Issuer,
			Audience = jwtSecrets.Audience,
			Expires = DateTime.UtcNow.AddDays(Constants.TOKEN_EXPIRATION_TIME),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtSecrets.Key), SecurityAlgorithms.HmacSha512Signature),
			Claims = claims.ToDictionary(claim => claim.Type, claim => (object)claim.Value)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}