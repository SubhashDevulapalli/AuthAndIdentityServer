using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
namespace AuthenticationServer.DBContext
{
    public class AuthPersistedGrantDbContext : PersistedGrantDbContext<AuthPersistedGrantDbContext>
    {
        public AuthPersistedGrantDbContext(DbContextOptions<AuthPersistedGrantDbContext> options) 
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");
        }
    }
}