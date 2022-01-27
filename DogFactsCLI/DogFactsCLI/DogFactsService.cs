using System.Diagnostics;
using System.Text.Json;
using System.Web;

internal class DogFactsService
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    private readonly string _baseUrl = "https://dog-api.kinduff.com/api/facts";
    private readonly string _numberQueryParameter = "number";

    public async Task<DogFactItem?> GetDogFactItemAsync(int number = -1)
    {
        try
        {
            var url = GetRequestUrl(number);

            var httpClient = new HttpClient();
            var jsonResponse = await httpClient.GetStringAsync(url);

            var dogFactItem = JsonSerializer.Deserialize<DogFactItem>(jsonResponse, _options);

            if (dogFactItem != null && dogFactItem.Success)
                return dogFactItem;
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"{GetType().Name} | {nameof(GetDogFactItemAsync)} | {ex}");
        }

        return null;
    }

    private string GetRequestUrl(int number)
    {
        var uriBuilder = new UriBuilder(_baseUrl);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (number > 0)
        {
            query[_numberQueryParameter] = number.ToString();
            uriBuilder.Query = query.ToString();
        }

        return uriBuilder.Uri.AbsoluteUri;
    }
}
