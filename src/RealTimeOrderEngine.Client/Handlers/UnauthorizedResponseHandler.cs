using System.Net;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;

namespace RealTimeOrderEngine.Client.Handlers;

public class UnauthorizedResponseHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorage;

    public UnauthorizedResponseHandler(NavigationManager navigationManager, ILocalStorageService localStorage)
    {
        _navigationManager = navigationManager;
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await _localStorage.RemoveItemAsync("authToken");
            _navigationManager.NavigateTo("/staff", true);
        }

        return response;
    }
}