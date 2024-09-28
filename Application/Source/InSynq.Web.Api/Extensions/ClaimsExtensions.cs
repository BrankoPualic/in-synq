using InSynq.Common;
using System.Security.Claims;

namespace InSynq.Web.Api.Extensions;

public static class ClaimsExtensions
{
    public static long GetId(this IEnumerable<Claim> claims) => Convert.ToInt64(GetClaim(claims, Constants.CLAIM_ID));

    public static string GetEmail(this IEnumerable<Claim> claims) => GetClaim(claims, Constants.CLAIM_EMAIL);

    public static string GetUsername(this IEnumerable<Claim> claims) => GetClaim(claims, Constants.CLAIM_USERNAME);

    public static string GetRoles(this IEnumerable<Claim> claims) => GetClaim(claims, Constants.CLAIM_ROLES);

    public static string GetClaim(this IEnumerable<Claim> claims, string claimName) => claims.SingleOrDefault(i => i.Type.Equals(claimName)).Value;
}