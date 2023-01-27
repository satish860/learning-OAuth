using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.AuthorizationPolicies
{
    public static class Policies
    {

        public static AuthorizationPolicy CanAddImage()
        {
            return new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .RequireRole("payinguser")
                        .RequireClaim("country", "be")
                        .Build();
        }
    }
}