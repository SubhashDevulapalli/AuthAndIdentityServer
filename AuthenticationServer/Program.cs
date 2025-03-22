var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Adding services for the project
var connectionString = builder.Configuration["ConnectionString"];
var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(migrationsAssembly);
        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
    });
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddConfigurationStore<AuthConfigurationDbContext>(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sqlOptions =>
            sqlOptions.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore<AuthPersistedGrantDbContext>(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sqlOptions =>
            sqlOptions.MigrationsAssembly(migrationsAssembly));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddCors(config =>
{
    config.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseIdentityServer();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
