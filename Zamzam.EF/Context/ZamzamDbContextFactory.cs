﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Zamzam.EF
{
    public class ZamzamDbContextFactory : IDesignTimeDbContextFactory<ZamzamDbContext>
    {
        public ZamzamDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ZamzamDbContext>();
            options.UseSqlServer("Data Source=MOSTAFA\\SQLEXPRESS;Initial Catalog=ZamzamDb;Integrated Security=True;TrustServerConnection=True");
            return new ZamzamDbContext(options.Options);
        }
    }
}
