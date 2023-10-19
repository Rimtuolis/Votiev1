using Microsoft.EntityFrameworkCore;
using VotieAPI.Data;
using VotieAPI.Responses;
using VotieAPI.Requests;
using VotieAPI.Services;
using Org.BouncyCastle.Asn1.Ocsp;
using O9d.AspNet.FluentValidation;
using VotieAPI.EndPoints;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<VotieDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("VoteDb")!)
);
builder.Services.AddTransient<IVotingService, VotingService>();
builder.Services.AddTransient<IDistrictsService, DistrictsService>();
builder.Services.AddTransient<ICandidateService, CandidateService>();
builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IElectionService, ElectionService>();
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

app.UseAuthorization();

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
app.Run();
