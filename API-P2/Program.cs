using API_P2.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
});

app.MapGet("/list", IEnumerable<Nomina> () => {
    using (var db = new SqliteDBContext())
    {
        IEnumerable<Nomina> data = db.Nominas.ToList();
        return data;
    }

});

app.MapGet("/add", async Task<string> () =>
{
    using (var db = new SqliteDBContext())
    {
        db.Database.EnsureCreatedAsync();
        var nomina = new Nomina
        {
        
            ClaveNomina = "Sas",
            Salario_ISR = "asa",
            Nombres = "1234",
            Regalia_Pascual = "f",
            Ingresos_Externos_ISR = "as",
            NumeroDocumento = "as",
            TipoDocumento = "12121212",
            SApellido = "Psicologia",
            PApellido = "arroz",
            RNC_Agente = "aasas",
            Otros_ISR = "asas",
            Remuneracio_Otros_Empleados = "aroz",
            Preaviso = "dsd",
            Sexo = "sasa",
            Salario_Infotep = "asasas",
            TipoRegistro = "aasas",
            Tipo_Ingreso = "sas"
        };
       


        await db.AddAsync(nomina);

        await db.SaveChangesAsync();


    }
    return "Agregado bien";


});
    app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}