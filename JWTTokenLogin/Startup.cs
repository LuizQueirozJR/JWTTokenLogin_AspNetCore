using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace JWTTokenLogin
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(o =>
      {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = "lqueirozjr.com",
          ValidAudience = "lqueirozjr.com",
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jhgayugwdjshdhjgdyugwd")),
        };
      });

      //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddSwaggerGen(options =>
       {
         options.EnableAnnotations();
         options.DescribeAllEnumsAsStrings();
         options.DescribeStringEnumsInCamelCase();
         options.SwaggerDoc("v1", new Info { Title = "JWTTokenLogin", Version = "v1" });
       });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseAuthentication();
      app.UseHttpsRedirection();
      app.UseMvc();

      app.UseSwagger();
      app.UseSwaggerUI(opt =>
      {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTTokenLogin");
      });
    }
  }
}
