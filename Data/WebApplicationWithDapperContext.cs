using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithDapper.Models;

namespace WebApplicationWithDapper.Data
{
    public class WebApplicationWithDapperContext : DbContext
    {
        public WebApplicationWithDapperContext (DbContextOptions<WebApplicationWithDapperContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplicationWithDapper.Models.Movie> Movie { get; set; } = default!;
        public DbSet<WebApplicationWithDapper.Models.Student> Student { get; set; } = default!;
    }
}
