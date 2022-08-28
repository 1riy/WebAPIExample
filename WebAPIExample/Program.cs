var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CpfService>(p =>
{
   int seed = DateTime.Now.Millisecond;
   CpfService service = new CpfService(seed);
   return service;

});

builder.Services.AddSingleton<CEPService>(p =>
{   
    string baseUrl = "https://viacep.com.br/ws/{cep}/json/";
    CEPService service = new CEPService(baseUrl);
    return service;
});

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
