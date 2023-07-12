﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Zamzam.EF
{
    public class ZamzamDbContextFactory : IDesignTimeDbContextFactory<ZamzamDbContext>
    {
        public ZamzamDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<ZamzamDbContext>();
            options.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ZamzamDb;Integrated Security=True;TrustServerCertificate=True");
            return new ZamzamDbContext(options.Options);
        }
    }
}
