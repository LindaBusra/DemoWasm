using System.Net.Http;
using System.Net.Http.Json; 
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

    public async Task<Product> GetProductById(int id)
    {
        return await _http.GetFromJsonAsync<Product>($"https://fakestoreapi.com/products/{id}");
    }
}
