using TecnoNovaCRUD.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregamos MVC y la clase que se comunica con PostgreSQL.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProductoDAO>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// Al abrir la página, mostramos la lista de productos.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Productos}/{action=Index}/{id?}");

app.Run();
