using System.Net;
using System.Net.Http.Json;
using Application.Permisos.Create;
using Application.Permisos.Update;

namespace IntegrationTests;

public class PermisosControllerTests : IntegrationTests
{
    [Fact]
    public async Task GetAll_Should_ReturnOK()
    {
        const HttpStatusCode expectedStatus = HttpStatusCode.OK;
        
        var response = await TestClient.GetAsync("api/Permiso/Get");
        
        await response.Content.ReadAsStringAsync();

        Assert.Equal(expectedStatus, response.StatusCode);
    }
    
    [Fact]
    public async Task Create_Should_ReturnOK()
    {
        const HttpStatusCode expectedStatus = HttpStatusCode.OK;
        var request = new CreatePermisoCommand("", "",1, DateTime.Now);
        var response = await TestClient.PostAsJsonAsync("api/Permiso/Request", request ) ;

        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Contains("\"success\":true", content);
    }
    
    [Fact]
    public async Task Update_Should_ReturnOK()
    {
        const HttpStatusCode expectedStatus = HttpStatusCode.OK;
        var request = new UpdatePermisoCommand(1, "","",2, DateTime.Now);
        var response = await TestClient.PutAsJsonAsync("api/Permiso/Modify", request ) ;

        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(expectedStatus, response.StatusCode);
        Assert.Contains("\"success\":true", content);
    }
}