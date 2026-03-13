using BookHive;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.Net.Http.Headers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// On ajoute ici FastEndpoints, un framework REPR et Swagger aux services disponibles dans le projet
builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "ThisIsASuperSecretJwtKeyThatIsAtLeast32CharsLong")
    .AddAuthorization()
    .AddFastEndpoints()
    .AddCors(options =>
    {
        options.AddDefaultPolicy(policyBuilder =>
        {
            policyBuilder
                .WithOrigins("http://localhost:4200")
                .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
                .AllowAnyHeader()
                .WithExposedHeaders(HeaderNames.ContentDisposition);
        });
    })
    .SwaggerDocument(options => { options.ShortSchemaNames = true; });

builder.Services.AddDbContext<BookHiveDbContext>();

builder.Services.AddHttpContextAccessor();


WebApplication app = builder.Build();
app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(options =>
    {
        options.Endpoints.ShortNames = true;
        options.Endpoints.RoutePrefix = "API";
    })
    .UseSwaggerGen();

// app.UseHttpsRedirection();

// app.UseCors();

app.Run();