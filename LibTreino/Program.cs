using LibTreino.Data;
using LibTreino.Services;
using DotNetEnv;

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
//Adicionar depois
//builder.Services.AddSingleton<UsuarioService>();

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

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
