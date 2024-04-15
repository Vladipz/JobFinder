using System.Security.Claims;

namespace JobFinder
{
	public static class ClaimsPrincipalExtansions
	{
		public static string GetUserId(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
