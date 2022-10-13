using Microsoft.EntityFrameworkCore;
using VetAppDal.Repo;
using VetAppDal.Repo.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddDbContext<VetAppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("VetAppConnection")));
builder.Services.AddScoped<IAnimalsRepository, AnimalsRepository>();
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
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthz");
    endpoints.MapControllers();
});

app.Run();
