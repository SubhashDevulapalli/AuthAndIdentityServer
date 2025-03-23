using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
namespace AuthenticationServer.DBContext
{
	public class AuthConfigurationDbContext : ConfigurationDbContext<AuthConfigurationDbContext>
	{
		public AuthConfigurationDbContext(DbContextOptions<AuthConfigurationDbContext> options) 
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