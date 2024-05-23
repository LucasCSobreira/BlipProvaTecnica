using BlipProvaTecnica.models.GitHubConfig;
using BlipProvaTecnica.service;
using BlipProvaTecnica.service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
var gitHubConfig = builder.Configuration.GetSection("GitHubConfig").Get<GitHubConfig>();
var httpClient = new HttpClient();

httpClient.DefaultRequestHeaders.Add("Accept", gitHubConfig.AplicationType);
httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", gitHubConfig.APIVersion);
httpClient.DefaultRequestHeaders.Add("User-Agent", gitHubConfig.UserAgent);

builder.Services.AddSingleton(httpClient);
builder.Services.AddSingleton(gitHubConfig);
builder.Services.AddSingleton<IGitHubService, GitHubService>();
builder.Services.AddControllers();


// Adicionar o serviço Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BlipProvaTecnica", Version = "v1" });
});


var app = builder.Build();

// Configura o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}

app.UseRouting();

app.UseAuthorization();

// Habilitar middleware de Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlipProvaTecnica V1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();
