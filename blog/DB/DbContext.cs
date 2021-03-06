﻿using blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DB
{
    public class BlogContext: DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        /* //alrocha
                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddEnvironmentVariables();
                    IConfiguration configuration = builder.Build();
                    string stringConexao = configuration.GetConnectionString("Blog");
                    optionsBuilder.UseSqlServer(stringConexao);
                }
        */
        
    }
}
