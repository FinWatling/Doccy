using Doccy.Components;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Doccy;
using Doccy.Communication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<IDataAccess, DataAccess>();
builder.Services.AddTransient<IUserData, UserData>();
builder.Services.AddTransient<UserModel>();
builder.Services.AddTransient<UserLogin>();
builder.Services.AddTransient<UserVerification>();
builder.Services.AddTransient<IDocumentData,  DocumentData>();
builder.Services.AddTransient<ISharedDocumentData, SharedDocumentData>();
builder.Services.AddSingleton<AuthService> ();
builder.Services.AddTransient<IEmailService, EmailService>();

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
