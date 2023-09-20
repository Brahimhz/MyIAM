using IdentityServer4.Stores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyIAM.AppService.Contracts;
using MyIAM.AppService.Implementations;
using MyIAM.Configurations;
using MyIAM.Configurations.Store;
using MyIAM.Data;
using MyIAM.Databases;
using MyIAM.Databases.Contracts;
using MyIAM.Databases.Implementations;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


//Stores
builder.Services.AddTransient<IResourceStore, ResourceStore>();
builder.Services.AddTransient<IClientStore, ClientStore>();

/*
builder.Services.AddIdentityServer()
        .AddDeveloperSigningCredential() // This is for development only
        .AddInMemoryApiResources(IAMConfiguartionResources.GetApiResources()) // Configure your API resources
        .AddInMemoryClients(IAMConfiguartionResources.GetClients())
        .AddInMemoryApiScopes(IAMConfiguartionResources.GetApiScopes()); // Configure your clients

*/
builder.Services.AddIdentityServer()
        .AddDeveloperSigningCredential() // This is for development only
        .AddClientStore<ClientStore>()
        .AddResourceStore<ResourceStore>();


builder.Services.AddDbContext<IAMDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient(typeof(IGenericAppService<,,>), typeof(GenericAppService<,,>));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllers();

app.Run();
