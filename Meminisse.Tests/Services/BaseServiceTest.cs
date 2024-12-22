using Meminisse.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Meminisse.Tests.Services;
internal class BaseServiceTest
{
    protected MeminisseDbContext Context;
    protected ServiceCollection Services = new();
    protected ServiceProvider Provider;

    [SetUp]
    public virtual void SetUp()
    {
        Services.AddScoped<MeminisseDbContext>();
        Provider = Services.BuildServiceProvider();
        Context = Provider.GetRequiredService<MeminisseDbContext>();
        Context.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        Dispose();
    }

    protected MeminisseDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<MeminisseDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new MeminisseDbContext(options);
    }


    protected void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
        Provider.Dispose();
    }
}
