using Microsoft.EntityFrameworkCore;
using ToDoGrpc.Data;
using ToDoGrpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<AddDbContext>(
     opt => {
         opt.UseSqlite("Data Source=ToDoDatabase.db");
     }

    );

var app = builder.Build();
app.MapGrpcService<GreeterService>();
app.MapGrpcService<TodoService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
