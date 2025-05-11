using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
//TODO: NewtonsoftJson paketini kullanabilmek i�in gerekli ayarlar� yapal�m.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Veri Tabn� i�lemi i�in AddDbContext eklenir. Buna da ilgili context s�n�f� verilir.
//TODO: RepositoryContext s�n�f�n� kullanarak DbContext'i ekleyin.
//TODO: Microsoft.EntityFrameworkCore.SqlServer NuGet paketini y�kleyin.
//TODO: SqlServer i�in gerekli olan ba�lant� dizesini appsettings.json a ekledik.
//TODO: IOC ye DBContext tan�m�n� yapm�� oluyoruz. (Bir DBContexte ihtiyac�m�z oldu�unda bunun somut haline ula�mam�z i�in)
builder.Services.AddDbContext<RepositoryContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));    


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
