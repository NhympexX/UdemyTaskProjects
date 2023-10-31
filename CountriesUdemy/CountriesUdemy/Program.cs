var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var countries = new Dictionary<int, string>();
countries.Add(1, "USA");
countries.Add(2, "Canada");
countries.Add(3, "United Kingdom");
countries.Add(4, "Azerbaijan");
countries.Add(5, "Japan");


app.UseRouting();
app.UseEndpoints(endpoints =>
endpoints.Map("/countries", async (HttpContext context) =>
{
    context.Response.StatusCode = 200;
    foreach (var country in countries)
    {
        context.Response.WriteAsync($"{country.Key},{country.Value}\n");
    }
}));
app.UseEndpoints(endpoints =>
endpoints.Map("/countries/{countryID:int:range(1,100)}", async (HttpContext context) =>
{
    context.Response.StatusCode = 200;
    int id = Convert.ToInt32(context.Request.RouteValues["countryID"]);
    context.Response.WriteAsync(countries[id]);
}));

app.Run();
