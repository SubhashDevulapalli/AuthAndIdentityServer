using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
namespace AuthorizationServer.Persistence
{
	public class AuthConfigurationDbContext : ConfigurationDbContext<AuthConfigurationDbContext>
	{
		public AuthConfigurationDbContext(DbContextOptions<AuthConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema("Identity");
		}
	}
}