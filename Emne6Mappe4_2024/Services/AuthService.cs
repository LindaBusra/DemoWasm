using Blazored.LocalStorage;
using System;

public class AuthService
{
    private readonly ILocalStorageService _localStorage;  // Brukes til å lagre og hente data lokalt i nettleseren
    private const string TokenKey = "authToken";  // Nøkkelen for å lagre autentiseringstoken i lokal lagring

    public event Action OnAuthStateChanged;  // Hendelse som utløses når autentiseringstilstanden endres

    public AuthService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;  // Initialiserer lokal lagringstjeneste
    }

    public async Task<bool> Login(string username, string password)
    {
        // Sjekker om brukernavn og passord er korrekt
        if (username == "admin" && password == "password")
        {
            await _localStorage.SetItemAsync(TokenKey, "fake-jwt-token");  // Lagrer en 'fake' JWT token i lokal lagring
            NotifyAuthStateChanged();  // Melder om endring i autentiseringstilstand
            return true;  // Returnerer true for å indikere vellykket innlogging
        }
        return false;  // Returnerer false hvis brukernavn eller passord er feil
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(TokenKey);  // Fjerner autentiseringstoken fra lokal lagring
        NotifyAuthStateChanged();  // Melder om endring i autentiseringstilstand
    }

    public async Task<bool> IsAuthenticated()
    {
        var token = await _localStorage.GetItemAsync<string>(TokenKey);  // Henter token fra lokal lagring
        return !string.IsNullOrEmpty(token);  // Sjekker om token eksisterer og returnerer resultatet
    }

    private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();  // Kaller alle lyttere som reagerer på endringer i autentiseringstilstand
}
