using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
//TODO: NewtonsoftJson paketini kullanabilmek için gerekli ayarlarý yapalým.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Veri Tabný iþlemi için AddDbContext eklenir. Buna da ilgili context sýnýfý verilir.
//TODO: RepositoryContext sýnýfýný kullanarak DbContext'i ekleyin.
//TODO: Microsoft.EntityFrameworkCore.SqlServer NuGet paketini yükleyin.
//TODO: SqlServer için gerekli olan baðlantý dizesini appsettings.json a ekledik.
//TODO: IOC ye DBContext tanýmýný yapmýþ oluyoruz. (Bir DBContexte ihtiyacýmýz olduðunda bunun somut haline ulaþmamýz için)
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
