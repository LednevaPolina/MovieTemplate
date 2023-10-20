using Movie.Extensions;
using Movie.Options;
using Movie.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Console.WriteLine("Key     : "+ builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("ApiKey"));
//Console.WriteLine("BaseUrl : " + builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("BaseUrl"));

//Console.WriteLine("Key     : " + builder.Configuration["ConnectionStrings:ApiKey"]);
//Console.WriteLine("BaseUrl : " + builder.Configuration["ConnectionStrings:BaseUrl"]);

//builder.Services.AddSingleton();
//builder.Services.AddScoped();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IRecentMovieStorage, RecentMovieStorage>();
//builder.Services.Configure<MovieApiOptions>(options =>
//{
//    options.ApiKey = builder.Configuration["ConnectionStrings:ApiKey"];
//    options.BaseUrl = builder.Configuration["ConnectionStrings:BaseUrl"];
//});
//builder.Services.AddScoped<IMovieApiServices, MovieApiServices>();   

builder.Services.AddMovieService(options =>
{
    options.ApiKey = builder.Configuration["ConnectionStrings:ApiKey"];
    options.BaseUrl = builder.Configuration["ConnectionStrings:BaseUrl"];
});

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
