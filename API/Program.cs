using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Enregistre tous tes services (DbContext, Identity, CORS, sessions, TokenService, etc.)
builder.Services.AddApplicationServices(builder.Configuration);

// 2️⃣ Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3️⃣ Build de l’application
var app = builder.Build();
using (var scope= app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {         // Initialise la base de données et les rôles
        var dbInitializer = services.GetRequiredService<API.DbInistializer.IDbInitializer>();
        dbInitializer.Initialize();
    }
    catch (Exception ex)
    {
        // Gère les erreurs d'initialisation de la base de données
        var logger = services.GetRequiredService<ILogger<Program>>();
    }
}
// 4️⃣ Middleware pipeline
if (app.Environment.IsDevelopment())
{
    // Génère et sert la spec OpenAPI + UI Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Si tu as activé CORS dans AddApplicationServices, déploie-le ici
app.UseCors(policy =>
    policy.AllowAnyHeader()
          .AllowAnyMethod()
          .WithOrigins("http://localhost:3000") // adapte ton URL React
);

app.UseSession();

// Active l’authentification JWT puis l’autorisation
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
