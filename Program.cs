

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(
    x =>
    {
        x.AddDefaultPolicy( cp =>
        {
            cp.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    }
);





RegisterServices(builder.Services);

var app = builder.Build();





Configure(app);

var apis = app.Services.GetServices<IApi>();
foreach (var api in apis)
{
    if (api is null) throw new InvalidProgramException("Api Not found");
    api.Register(app);
}

app.Run();




void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddDbContext<WebApiDb>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    });


    services.AddSingleton<ITokenService>(new TokenService());

    services.AddSingleton<IAuthUserService>(new AuthUserService());
    services.AddScoped<IPostRepository, PostRepository>();
    services.AddScoped<IUserRepository, UserRepository>();


    services.AddSingleton<IMapPost, MapPost>();
    services.AddSingleton<IUserMap, UserMap>();


    services.AddAuthorization();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

    services.AddTransient<IApi, PostApi>();
    services.AddTransient<IApi, AuthApi>();
    services.AddAutoMapper(typeof(AppMappingProfile));



}

void Configure(WebApplication app)
{

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<WebApiDb>();
        db.Database.EnsureCreated();
    }

}
