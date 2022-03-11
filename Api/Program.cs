using Api.Interfaces;
using Api.Models;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPromotionService, PromotionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "participant",
        pattern: "promo/{action=Get}/{id}/{participant}");
    
    endpoints.MapControllerRoute(
        name: "participantId",
        pattern: "promo/{action=Get}/{id}/{participant}/{participantId}");

    endpoints.MapControllerRoute(
        name: "id",
        pattern: "promo/{action=Get}/{id}");
    
    endpoints.MapControllerRoute(
        name: "prize",
        pattern: "promo/{action=Get}/{id}/prize");

    endpoints.MapControllerRoute(
        name: "prizeId",
        pattern: "promo/{action=Get}/{id}/{prize}/{prizeId}");
    
    endpoints.MapControllerRoute(
        name: "clean",
        pattern: "promo/{action=Get}");
    
    endpoints.MapControllerRoute(
        name: "raffle",
        pattern: "promo/{action=Get}/{id}/raffle");
});

app.Run();