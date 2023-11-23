
using VotieAPI.Data;
using VotieAPI.Responses;
using VotieAPI.Requests;
using VotieAPI.Services;
using Org.BouncyCastle.Asn1.Ocsp;
using O9d.AspNet.FluentValidation;
using VotieAPI.EndPoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VotieAPI.Auth.Entity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using VotieAPI.Auth;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<VotieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VoteDb")!)
);
builder.Services.AddTransient<IVotingService, VotingService>();
builder.Services.AddTransient<IDistrictsService, DistrictsService>();
builder.Services.AddTransient<ICandidateService, CandidateService>();
builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IElectionService, ElectionService>();
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddScoped<AuthDbSeeder>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddIdentity<VotieUser,IdentityRole>()
    .AddEntityFrameworkStores<VotieDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters.ValidAudience = builder.Configuration["Jwt:ValidAudience"];
    options.TokenValidationParameters.ValidIssuer = builder.Configuration["Jwt:ValidIssuer"];
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]));
});

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();


app.MapGet("/", () => "Hello world");

var voterGroup = app.MapGroup("/api").WithValidationFilter();
var districtsGroup = app.MapGroup("/api").WithValidationFilter(); 
var candidatesGroup = app.MapGroup("/api").WithValidationFilter();
var votesGroup = app.MapGroup("/api").WithValidationFilter(); ;
var electionsGroup = app.MapGroup("/api").WithValidationFilter(); ;

VotersEndPoints.AddVotersEndPoints(voterGroup);
DistrictsEndPoints.AddDistrictsEndPoints(districtsGroup);
CandidatesEndPoints.AddCandidatesEndPoints(candidatesGroup);
VotesEndPoints.AddVotesEndPoints(votesGroup);
ElectionsEndPoints.AddElectionsEndPoints(electionsGroup);

app.AddAuthApi();

app.UseAuthentication();
app.UseAuthorization();

using var scope2 = app.Services.CreateScope();
var context = scope2.ServiceProvider.GetRequiredService<VotieDbContext>();
context.Database.EnsureCreated();
using var scope = app.Services.CreateScope();
var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();

await dbSeeder.SeedAsync();

app.Run();
