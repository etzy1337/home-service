using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extension
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
{
    // Sprawdzamy, czy user nie jest null
    if (user == null)
    {
        throw new ArgumentNullException(nameof(user), "ClaimsPrincipal cannot be null");
    }

    // Pobieramy wartość "givenname", jeśli istnieje
    var givenNameClaim = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"));

    // Jeśli nie ma takiego claimu, zwracamy null lub możesz zwrócić pusty string czy inną wartość
    return givenNameClaim?.Value;
}

    }
}