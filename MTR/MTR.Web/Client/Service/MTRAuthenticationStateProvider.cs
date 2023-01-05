using Microsoft.AspNetCore.Components.Authorization;

using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;

using System.Net.Http.Json;
using System.Security.Claims;

namespace MTR.Web.Client.Service;

public class MTRAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly UserInfo? _userInfoCache;
    private readonly HttpClient _httpClient;

    public MTRAuthenticationStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SignInAsync(SignInUserCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("auth/signin", command);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        result.EnsureSuccessStatusCode();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task SignOutAsync()
    {
        var result = await _httpClient.PostAsync("auth/signout", null);
        result.EnsureSuccessStatusCode();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task SignUpAsync(SignUpUserCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync("auth/signup", command);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        result.EnsureSuccessStatusCode();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<UserInfo> GetUserInfo()
    {
        if (_userInfoCache != null && _userInfoCache.IsAuthenticated)
        {
            return _userInfoCache;
        }

        var result = await _httpClient.GetFromJsonAsync<UserInfo>("auth/userinfo");
        return result;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        try
        {
            var userInfo = await GetUserInfo();
            if (userInfo.IsAuthenticated)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, userInfo.Username) }.Concat(userInfo.ExposedClaims.Select(c => new Claim(c.Key, c.Value)));
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

}
