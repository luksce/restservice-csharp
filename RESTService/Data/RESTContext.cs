using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RESTService.Models.Entity;

namespace RESTService.Data
{
    public class RESTContext : DbContext
    {
        private static IConfiguration _configuration { get; set; }
        public RESTContext(DbContextOptions<RESTContext> options) : base(options)
        {

        }
        public RESTContext()
        {

        }
        public DbSet<Coins> Coins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbFilas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
