using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SOSOWJB.Framework.Configuration;
using SOSOWJB.Framework.Web;

namespace SOSOWJB.Framework.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class FrameworkDbContextFactory : IDesignTimeDbContextFactory<FrameworkDbContext>
    {
        public FrameworkDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FrameworkDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            FrameworkDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FrameworkConsts.ConnectionStringName));

            return new FrameworkDbContext(builder.Options);
        }
    }
}