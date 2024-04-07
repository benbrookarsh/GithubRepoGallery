var builder = WebApplication.CreateBuilder(args);

builder.AddControllers();
builder.AddServices();
builder.ConfigureDatabase();
builder.AddSwagger();
builder.AddAuthentication();

var corsPolicy = "AllowSpecificOrigin";
builder.AddCorsPolicy(corsPolicy);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(corsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserContextMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();