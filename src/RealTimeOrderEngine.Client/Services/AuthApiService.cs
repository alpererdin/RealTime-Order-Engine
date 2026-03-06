using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RealTimeOrderEngine.Client.Auth;
using RealTimeOrderEngine.Shared.DTOs.Auth;

namespace RealTimeOrderEngine.Client.Services;

public class AuthApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthApiService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> Login(string pinCode)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new LoginDto { PinCode = pinCode });

        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result == null) return false;

        await _localStorage.SetItemAsync("authToken", result.Token);
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
        
        return true;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}