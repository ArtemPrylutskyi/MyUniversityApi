using Microsoft.EntityFrameworkCore;
using MyUniversityApi.Data; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseInMemoryDatabase("UniversityDb"));


builder.Services.AddSwaggerGen();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

