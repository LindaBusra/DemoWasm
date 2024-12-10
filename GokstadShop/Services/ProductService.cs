using System.Net.Http;
using System.Net.Http.Json; // Eksik olan using direktifi eklendi.
using System.Threading.Tasks;
using System.Collections.Generic;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _http.GetFromJsonAsync<List<Product>>("https://fakestoreapi.com/products");
    }
}
