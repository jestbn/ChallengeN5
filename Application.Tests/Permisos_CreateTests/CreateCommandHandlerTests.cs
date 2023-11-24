using Application.Permisos.Create;
using Domain.Abstractions;
using Domain.Permisos;
using Domain.Shared;
using Domain.TipoPermiso;

namespace Application.Tests.Permisos_CreateTests;

public class CreateCommandHandlerTests
{
    private Mock<IPermisoRepository>  MoqPermisoRepo { get; set; }= new();
    private Mock<IUnitOfWork> MoqUow{ get; set; }= new();
    private Mock<ITipoPermisoRepository> MoqTipoPermisoRepo{ get; set; }= new();
    private CreatePermisoCommandHandler Handler { get; set; }

    public CreateCommandHandlerTests()
    {
        Handler = new CreatePermisoCommandHandler(MoqPermisoRepo.Object, MoqUow.Object, MoqTipoPermisoRepo.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_Successful()
    {
        var commandRequest = new CreatePermisoCommand(
            "name",
            "surname",
            1,
            DateTime.Now
        );
        var tipoReturned = new TipoPermiso()
        {

        };
        MoqTipoPermisoRepo.Setup(x => x.GetById(It.IsAny<TipoPermisoId>()))
            .Returns(tipoReturned);
        MoqPermisoRepo.Setup(y => y.Add(It.IsAny<Permiso>())).Verifiable();
        MoqUow.Setup(z=>z.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        
        var result = await Handler.Handle(commandRequest, default);

        Assert.IsType<Result>(result);
        Assert.True(result.Success);
        MoqPermisoRepo.Verify(v=>v.Add(It.IsAny<Permiso>()), Times.Once);
        MoqUow.Verify(v=>v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    } 
    
    [Fact]
    public async Task Handle_ShouldReturnResult_Unsuccessful()
    {
        var commandRequest = new CreatePermisoCommand(
            "name",
            "surname",
            1,
            DateTime.Now
        );
        
        MoqTipoPermisoRepo.Setup(x => x.GetById(It.IsAny<TipoPermisoId>()))
            .Returns((TipoPermiso)null!);
        
        MoqPermisoRepo.Setup(y => y.Add(It.IsAny<Permiso>())).Verifiable();
        MoqUow.Setup(z=>z.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        
        var result = await Handler.Handle(commandRequest, default);

        Assert.IsType<Result>(result);
        Assert.False(result.Success);
        MoqPermisoRepo.Verify(v=>v.Add(It.IsAny<Permiso>()), Times.Never);
        MoqUow.Verify(v=>v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

    } 
}