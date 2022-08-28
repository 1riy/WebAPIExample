using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public class CEPService
{
    private HttpClient client = null;
    private string baseUrl = null;

    public CEPService(string service)
    {
        client = new HttpClient();
        baseUrl = service;
    }

    public async Task<CEP> RequestCEP(string cep)
    {
        var url = baseUrl.Replace("{cep}", cep);
        var response = await client.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception("Invalid data.");
        else if (response.StatusCode != HttpStatusCode.OK)
            throw new Exception("Server unavaliable.");

        var content = response.Content;
        var cepObj = await content.ReadFromJsonAsync<CEP>();

        if (cepObj == null)
            throw new Exception("Invalid Response.");

        return cepObj;
    }
}