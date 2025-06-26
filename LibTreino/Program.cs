using DotNetEnv;
using LibTreino.Data;
using LibTreino.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

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
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<ShoppingListService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<TokenService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => option.AddDefaultPolicy(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
