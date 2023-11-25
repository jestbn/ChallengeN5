using System.Net;

namespace IntegrationTests;

public class PermisosControllerTests : IntegrationTests
{
    [Fact]
    public async Task GetAll_ReturnsEmpty()
    {
        
        var expectedStatus = HttpStatusCode.OK;
        
        var response = await TestClient.GetAsync("api/Permiso/Get");
        
        string content = await response.Content.ReadAsStringAsync();

        // Hacer algo con el contenido
        Console.WriteLine(content);

        Assert.Equal(expectedStatus, response.StatusCode);
    }
    
}