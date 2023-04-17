using DollyHotel.Server.Data;
using DollyHotel.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DollyHotelServerContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddSingleton(sp =>
{
    // Get the address that the app is currently running at
    var server = sp.GetRequiredService<IServer>();
    var addressFeature = server.Features.Get<IServerAddressesFeature>();
    string baseAddress = addressFeature.Addresses.First();
    return new HttpClient { BaseAddress = new Uri(baseAddress) };
});
builder.Services.AddScoped<DollyHotel.Server.ConDataService>();
builder.Services.AddDbContext<DollyHotel.Server.Data.ConDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderConData = new ODataConventionModelBuilder();
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.AspNetRoleClaim>("AspNetRoleClaims");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.AspNetRole>("AspNetRoles");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.AspNetUserClaim>("AspNetUserClaims");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.AspNetUserLogin>("AspNetUserLogins").EntityType.HasKey(entity => new
    {
        entity.LoginProvider,
        entity.ProviderKey
    });
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.AspNetUserRole>("AspNetUserRoles").EntityType.HasKey(entity => new
    {
        entity.UserId,
        entity.RoleId
    });
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.AspNetUser>("AspNetUsers");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.AspNetUserToken>("AspNetUserTokens").EntityType.HasKey(entity => new
    {
        entity.UserId,
        entity.LoginProvider,
        entity.Name
    });
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.BedType>("BedTypes");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.BookingStatus>("BookingStatuses");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.City>("Cities");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.HotelFacility>("HotelFacilities");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.HotelRoom>("HotelRooms");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.Hotel>("Hotels");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.HotelType>("HotelTypes");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.PaymentStatus>("PaymentStatuses");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.RoomBookingDetail>("RoomBookingDetails");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.RoomBooking>("RoomBookings");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.RoomStatus>("RoomStatuses");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.RoomTypeFacility>("RoomTypeFacilities");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.RoomType>("RoomTypes");
    oDataBuilderConData.EntitySet<DollyHotel.Server.Models.ConData.Search>("Searches");
    opt.AddRouteComponents("odata/ConData", oDataBuilderConData.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<DollyHotel.Client.ConDataService>();
builder.Services.AddHttpClient("DollyHotel.Server").AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddControllers().AddOData(o =>
{
    var oDataBuilder = new ODataConventionModelBuilder();
    oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
    var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.Password)));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.ConfirmPassword)));
    oDataBuilder.EntitySet<ApplicationRole>("ApplicationRoles");
    o.AddRouteComponents("odata/Identity", oDataBuilder.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<DollyHotelServerContext>()
//    .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>();


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>() // Add roles
 .AddEntityFrameworkStores<DollyHotelServerContext>();

builder.Services.AddIdentityServer()
 .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options
=>
 {
     options.IdentityResources["openid"].UserClaims.Add("role");
     options.ApiResources.Single().UserClaims.Add("role");
 });

System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
 .DefaultInboundClaimTypeMap.Remove("role");

builder.Services.AddAuthentication()
    .AddIdentityServerJwt().AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
    }).AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    }); ; ;

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseHeaderPropagation();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
