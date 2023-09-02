using AuctionService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt=>{
  opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// pokretanje aplikacije - auctionServerice folder
// dotnet watch 

//provjera instaliranog EF
// dotnet tool list -g
// dotnet tool install dotnet-ef -g
// dotnet tool update dotnet-ef -g

//create new migration
// dotnet ef migrations add "InitialCreate" -o Data/Migrations


//pokretanje dockera
// nakon što se napravi yml file i konfigurira pokreće se docker
// -d detached mode da ne pokazuje logove u terminalu
// docker compose up -d 
// nakon toga kreiranje tablica na postgre serveru
// dotnet ef database update


// za dropanje baze - auctionServerice folder
//dotnet ef database drop

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
  DbInitializer.InitDb(app);
}
catch(Exception e)
{
  Console.WriteLine(e);
}


app.Run();
