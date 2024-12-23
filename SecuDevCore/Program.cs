using CryptoManager.Services;
using CryptoManager;
using Microsoft.AspNetCore.Authentication.Cookies;
using SingletonManager;
using CoreDAL.Configuration.Interface;
using CoreDAL.Configuration.Models;
using CoreDAL.Configuration;
using FileIOHelper.Helpers;
using Microsoft.AspNetCore.Hosting.Server;
using SecuDev.Helper;

namespace SecuDev
{
    public static class SetupName
    {
        public static string SHA256 = "SHA256";
        public static string AES256 = "AES256";
        public static string Setupini = "Setupini";
        public static string ConnDB = "ConnDB";
    }
    public class Program
    {
        public static void Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // IHttpContextAccessor 서비스 등록
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDistributedMemoryCache(); // 메모리 캐시 사용
            // 세션 사용과 관련된 설정
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;  // 세션 쿠키가 필수적인지 여부 (GDPR 규정 준수)
            });

            // 인증 관련 리디렉션 페이지 설정
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home";
                    options.LogoutPath = "/Home";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.None;  // http와 https 모두 쿠키 사용
                    options.Cookie.SameSite = SameSiteMode.Strict;
                });

            Singletons.Instance.AddKeyedSingleton<ICryptoManager>(SetupName.SHA256, new SecuSHA256());

            ICryptoManager EncAES256 = new AES256("secu13579");
            Singletons.Instance.AddKeyedSingleton<ICryptoManager>(SetupName.AES256, EncAES256);

            IDatabaseSetup setup = new DatabaseSetup(DatabaseType.MSSQL, new IniFileHelper(Path.Combine(builder.Environment.ContentRootPath, "Upload", "Data", "WebSetup.ini")), "SECUDEV", EncAES256);
            IDbConnectionInfo info = new MsSqlConnectionInfo { Server = "127.0.0.1", UserId = "sa", Password = "s1access!", Database = "SECUDEV" };
            Singletons.Instance.AddKeyedSingleton<IDatabaseSetup>(SetupName.ConnDB, setup);
            setup.UpdateConnectionInfo(info);

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

            SessionHelper.SetHttpContextAccessor(app.Services.GetRequiredService<IHttpContextAccessor>());

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseStatusCodePagesWithRedirects("/Error/{0}"); //point to error page
            app.Run();

        }
    }
}

