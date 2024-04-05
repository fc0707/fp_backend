using Loginoperations.Context;
using Loginoperations.Dto;
using Loginoperations.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DContext>(x => x.UseSqlite(("Data Source=project.db")));
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<DtoConverter>();

// CORS ayarlarını yapma
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

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
app.UseAuthorization();

// CORS politikasını uygulama
app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();