﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Practica1.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base(options) 
        {
        }

        public DbSet<equipos> equipos { get; set; }
    }
}
