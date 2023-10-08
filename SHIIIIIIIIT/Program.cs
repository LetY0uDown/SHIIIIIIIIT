using SHIIIIIIIIT.Tools.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();

builder.Services.AddServices()
                .ConfigureAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication()
   .UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();