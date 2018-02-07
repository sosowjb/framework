using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SOSOWJB.Framework.EntityFrameworkCore
{
    public static class FrameworkDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FrameworkDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FrameworkDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}