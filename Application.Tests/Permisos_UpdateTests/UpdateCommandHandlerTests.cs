using Application.Permisos.Create;
using Application.Permisos.Update;
using Domain.Abstractions;
using Domain.Permisos;
using Domain.Shared;
using Domain.TipoPermiso;

namespace Application.Tests.Permisos_UpdateTests;

public class UpdateCommandHandlerTests
{
    private Mock<IPermisoRepository>  MoqPermisoRepo { get; set; }= new();
    private Mock<IUnitOfWork> MoqUow{ get; set; }= new();
    private Mock<ITipoPermisoRepository> MoqTipoPermisoRepo{ get; set; }= new();
    private UpdatePermisoCommandHandler Handler { get; set; }

    public UpdateCommandHandlerTests()
    {
        Handler = new UpdatePermisoCommandHandler(MoqPermisoRepo.Object, MoqUow.Object, MoqTipoPermisoRepo.Object);
    }
    [Fact]
    public async Task Handle_ShouldReturnResult_Successful()
    {
        var commandRequest = new UpdatePermisoCommand(
            1,
            "name",
            "surname",
            1,
            DateTime.Now
        );
        var tipoReturned = new TipoPermiso()
        {

        };
        var permisoReturned = new Permiso("a", "b", DateTime.Now);
        MoqPermisoRepo.Setup(p => p.GetById(It.IsAny<PermisoId>()))
            .Returns(permisoReturned);
        MoqTipoPermisoRepo.Setup(x => x.GetById(It.IsAny<TipoPermisoId>()))
            .Returns(tipoReturned);
        MoqPermisoRepo.Setup(y => y.Add(It.IsAny<Permiso>())).Verifiable();
        MoqUow.Setup(z=>z.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        
        var result = await Handler.Handle(commandRequest, default);

        Assert.IsType<Result>(result);
        Assert.True(result.Success);
        MoqPermisoRepo.Verify(v=>v.GetById(It.IsAny<PermisoId>()), Times.Once);
        MoqPermisoRepo.Verify(v=>v.Add(It.IsAny<Permiso>()), Times.Once);
        MoqUow.Verify(v=>v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }
    [Fact]
    public async Task Handle_ShouldReturnResult_Unsuccessful()
    {
        var commandRequest = new UpdatePermisoCommand(
            1,
            "name",
            "surname",
            1,
            DateTime.Now
        );
        var tipoReturned = new TipoPermiso()
        {

        };

        MoqPermisoRepo.Setup(p => p.GetById(It.IsAny<PermisoId>()))
            .Returns((Permiso)null!);
        MoqTipoPermisoRepo.Setup(x => x.GetById(It.IsAny<TipoPermisoId>()))
            .Returns(tipoReturned);
        MoqPermisoRepo.Setup(y => y.Add(It.IsAny<Permiso>())).Verifiable();
        MoqUow.Setup(z=>z.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        
        var result = await Handler.Handle(commandRequest, default);

        Assert.IsType<Result>(result);
        Assert.False(result.Success);
        MoqPermisoRepo.Verify(v=>v.GetById(It.IsAny<PermisoId>()), Times.Once);
        MoqPermisoRepo.Verify(v=>v.Add(It.IsAny<Permiso>()), Times.Never);
        MoqUow.Verify(v=>v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

    }
    
    [Fact]
    public async Task Handle_ShouldReturnResult_NewTipoDoestnExist_Unsuccessful()
    {
        var commandRequest = new UpdatePermisoCommand(
            1,
            "name",
            "surname",
            1,
            DateTime.Now
        );
       
        var permisoReturned = new Permiso("a", "b", DateTime.Now);

        MoqPermisoRepo.Setup(p => p.GetById(It.IsAny<PermisoId>()))
            .Returns(permisoReturned);
        MoqTipoPermisoRepo.Setup(x => x.GetById(It.IsAny<TipoPermisoId>()))
            .Returns((TipoPermiso)null!);
        
        MoqPermisoRepo.Setup(y => y.Add(It.IsAny<Permiso>())).Verifiable();
        MoqUow.Setup(z=>z.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();
        
        var result = await Handler.Handle(commandRequest, default);

        Assert.IsType<Result>(result);
        Assert.False(result.Success);
        MoqPermisoRepo.Verify(v=>v.GetById(It.IsAny<PermisoId>()), Times.Once);
        MoqPermisoRepo.Verify(v=>v.Add(It.IsAny<Permiso>()), Times.Never);
        MoqUow.Verify(v=>v.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

    }
}