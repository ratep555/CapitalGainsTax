using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
{
    public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (typeof(T) == typeof(string))
        {
            return (T)Convert.ChangeType(loggedInUserId, typeof(T));
        }
        else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
        {
            return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
        }
        else
        {
            throw new Exception("Invalid type provided");
        }
    }

    public static string GetLoggedInUserName(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirstValue(ClaimTypes.Name);
    }
   /*  public static string GetUserId(this IIdentity identity)
{
    if (identity == null)
    {
        throw new ArgumentNullException("identity");
    }
    var ci = identity as ClaimsIdentity;
    if (ci != null)
    {
        return ci.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    return null;
} */


    public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirstValue(ClaimTypes.Email);
    }
     public static string RetrieveIdFromPrincipal(this ClaimsPrincipal user) 
     {
           return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
           .Value;
     }
     public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user) 
     {
           return user.FindFirstValue(ClaimTypes.Email);
     }
     public static string RetrieveUserIdFromPrincipal(this ClaimsPrincipal user) 
     {
           return user.FindFirstValue(ClaimTypes.NameIdentifier);
     }
}
}







