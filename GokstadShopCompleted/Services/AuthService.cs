using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;

public class AuthService
{
    private readonly ILocalStorageService _localStorage;

    private const string TokenKey = "authToken";
    
    public event Action OnAuthStateChanged;


    public AuthService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<bool> Login(string username, string password)
    {
        //statisk brukernavn og passord for demontrasjon
        if(username=="admin" && password=="password")
        {
            await _localStorage.SetItemAsync(TokenKey, "fake-jwt-token");   //jason web token:jwt   
            NotifyAuthStateChanged();         
            return true;
        }
        return false;
    }


    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(TokenKey);
        NotifyAuthStateChanged();
    }


    public async Task<bool> IsAuthenticated()
    {
        var token = await _localStorage.GetItemAsync<string>(TokenKey);
        return !string.IsNullOrEmpty(token);
    }

        private void NotifyAuthStateChanged()
    {
        OnAuthStateChanged.Invoke();
    }

}