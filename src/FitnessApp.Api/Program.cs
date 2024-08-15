using FitensApp.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(builder.Configuration["apiUrl"]!);
var app = builder.Build();


app.RegisterUserEndpoints();
app.RegisterActivityEndpoints();
app.RegisterPersonalizationEndpoints();

app.Run();
