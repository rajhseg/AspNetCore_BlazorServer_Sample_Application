using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace WebBlazorApp.Filters
{
    public class BlazorAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage sessionStorage;
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public BlazorAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }
    
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userstorageResult = await sessionStorage.GetAsync<UserData>("UserData");
                var session = userstorageResult.Success ? userstorageResult.Value : null;

                if (session == null)
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claims = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                    new Claim(ClaimTypes.Name, session.UserName),
                    new Claim(ClaimTypes.Role, session.Role)
                }, authenticationType:"customAuth"));

                return await Task.FromResult(new AuthenticationState(claims));
            }
            catch(Exception)
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthentication(UserData userData)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userData != null)
            {
                await sessionStorage.SetAsync("UserData", userData);

                 claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                    new Claim(ClaimTypes.Name, userData.UserName),
                    new Claim(ClaimTypes.Role, userData.Role)
                }, authenticationType: "customAuth"));
            }
            else
            {
                await sessionStorage.DeleteAsync("UserData");
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
