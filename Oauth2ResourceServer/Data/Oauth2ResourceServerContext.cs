using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Oauth2ResourceServer.Models
{
    public class Oauth2ResourceServerContext : DbContext
    {
        public Oauth2ResourceServerContext (DbContextOptions<Oauth2ResourceServerContext> options)
            : base(options)
        {
        }

        public DbSet<Oauth2ResourceServer.Models.Song> Song { get; set; }
    }
}
