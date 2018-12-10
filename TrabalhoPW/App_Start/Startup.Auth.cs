using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TrabalhoPW.Models;

namespace TrabalhoPW
{
    public partial class Startup
    {
        MuseuContext context;
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            context = new MuseuContext();
            CreateRoles();
            SetContext();
        }

        private void SetContext()
        {
            if (!context.Texts.Any()) {

                Texts home = new Texts();
                home.Pagina = "HomePage";
                home.SubT = "Bem Vindo ao Museu XPTO de Cenas!";
                home.Conteudo = "Exemplo de Conteudo de texto.";
                Texts home2 = new Texts();
                home.Pagina = "HomePage1";
                home.SubT = "Bem Vindo ao Museu XPTO de Cenas!";
                home.Conteudo = "Exemplo de Conteudo de texto.";
                Texts home3 = new Texts();
                home.Pagina = "HomePage2";
                home.SubT = "Bem Vindo ao Museu XPTO de Cenas!";
                home.Conteudo = "Exemplo de Conteudo de texto.";

                Texts contactos = new Texts();
                contactos.Pagina = "Contactos";
                contactos.SubT = "Nao deixe de nos contactar ou até mesmo fazer uma visita!";
                contactos.Conteudo = "Exemplo de Conteudo de texto.";
                context.Texts.Add(home);
                context.Texts.Add(home2);
                context.Texts.Add(home3);
                context.Texts.Add(contactos);
                context.SaveChanges();
            }
        }

        private void CreateRoles()
        {
            ApplicationDbContext contextt = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(contextt));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(contextt));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "pw@isec.pt"
                };

                string userPWD = "Pass01!";
                var chkUser = UserManager.Create(user, userPWD);
                Utilizador admin = new Utilizador() { Nome = "admin", Email = "pw@isec.pt", BI = 1, NIF = 1, Tipo = "Admin", Valido = true, UserID = UserManager.FindByName("admin").Id };
                context.Utilizador.Add(admin);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("Especialista"))
            {
                var role = new IdentityRole { Name = "Especialista" };
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Membro"))
            {
                var role = new IdentityRole { Name = "Membro" };
                roleManager.Create(role);

            }
        }
    }
}