using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ABC.BusinessBase;

public class AbcContextFactory : IDesignTimeDbContextFactory<AbcContext>
{
    public AbcContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AbcContext>();
        optionsBuilder.UseSqlServer<AbcContext>("server=systemname\\sql;database=test;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        return new AbcContext(optionsBuilder.Options);
    }
}