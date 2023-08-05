﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Zamzam.EF
{
    public class ZamzamDbContextFactory : IDesignTimeDbContextFactory<ZamzamDbContext>
    {
        public ZamzamDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ZamzamDbContext>();

            options.UseLazyLoadingProxies().UseSqlServer("Data Source=.\\SQLEXPRESS;DataBase=ZamzamDb;Integrated Security=True;TrustServerCertificate=True");

            return new ZamzamDbContext(options.Options);
        }
    }
}
