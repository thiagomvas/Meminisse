using Meminisse.Application;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Tests.Repositories;

internal abstract class BaseRepositoryTest
{
    protected MeminisseDbContext Context;

    [SetUp]
    public virtual void SetUp()
    {
        Context = CreateInMemoryDbContext();
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
    }

}