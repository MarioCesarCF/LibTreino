using DotNetEnv;
using LibTreino.Data;
using LibTreino.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            var origins = new string[] { "http://localhost:4200", "https://localhost:7050" };
            policy.WithOrigins(origins) // front Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configurações do banco. Estudar melhor essa parte.
// Singleton para garantir apenas 1 conexão do banco
builder.Services.Configure<ConfigDatabaseSettings>(options =>
{
    var mongoUser = Environment.GetEnvironmentVariable("MONGO_USER");
    var mongoPassword = Environment.GetEnvironmentVariable("MONGO_PASSWORD");
    var mongoHost = Environment.GetEnvironmentVariable("MONGO_HOST");
    var mongoDatabase = Environment.GetEnvironmentVariable("MONGO_DATABASE");

    options.ConnectionString = $"mongodb+srv://{mongoUser}:{mongoPassword}@{mongoHost}/?retryWrites=true&w=majority";
    options.DatabaseName = mongoDatabase;
});

builder.Services.Configure<ConfigDatabaseSettings>(builder.Configuration.GetSection("MongoConnection"));
builder.Services.AddSingleton<ProdutoService>();
builder.Services.AddSingleton<ListaComprasService>();
builder.Services.AddSingleton<UsuarioService>();
builder.Services.AddSingleton<TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
    var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
    var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
