using Microsoft.EntityFrameworkCore;
using Watchzen_Api.Models;

namespace Watchzen_Api.Data
{
    public class Appdbcontext : DbContext
    {
        public Appdbcontext(DbContextOptions<Appdbcontext> options) : base(options)
        {

        }
        public DbSet<ProductInfo> Product { get; set; }
    }
}
