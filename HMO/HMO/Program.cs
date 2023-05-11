using Repository.GeneratedModels;
using Microsoft.EntityFrameworkCore;
using Service.UsersService;
using Service.CoronaDetailsService;
using Service.VaccineDetailsService;

var builder = WebApplication.CreateBuilder(args);
var MyAppOrigin = "MyAppOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAppOrigin,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader().AllowAnyMethod(); ;
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var cs = builder.Configuration["MyDBConnectionString"];
builder.Services.AddDbContext<MyDBContext>(options => options.UseNpgsql(cs));

builder.Services.AddScoped<IUserData, UserData>();
builder.Services.AddScoped<ICoronaDetailsData, CoronaDetailsData>();
builder.Services.AddScoped<IVaccineetailsData, VaccineDateilsData>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAppOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
