using Microsoft.EntityFrameworkCore;
using RegistrosTecnico.Components;
using RegistrosTecnico.DAL;
using RegistrosTecnico.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Inyeccción del contexto
var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContext<Contexto>(c => c.UseSqlite(ConStr));
builder.Services.AddBlazorBootstrap();

//Inyeccción del service
builder.Services.AddScoped<TecnicoService>();
builder.Services.AddScoped<TiposTecnicoServices>();
builder.Services.AddScoped<ClientesServices>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
