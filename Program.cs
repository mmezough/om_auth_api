using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Objets100cLib;
using om_auth_api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

var app = builder.Build();

// Connexion à la base Sage
var om_CIAL = new BSCIALApplication100c();
om_CIAL.CompanyServer = ".\\SAGE100";
om_CIAL.CompanyDatabaseName = "BIJOU";
om_CIAL.Loggable.UserName = "<Administrateur>";
om_CIAL.Loggable.UserPwd = string.Empty;
om_CIAL.Open();
var om_CPTA = om_CIAL.CptaApplication;

// Liste des clients Sage
app.MapGet("/clients", [Authorize(Roles = "sage100.compta")] () =>
{
    List<Client> clients = new List<Client>();
    foreach (IBOClient3 om_Client in om_CPTA.FactoryClient.List)
    {
        Client client = new Client() { 
            CT_Num = om_Client.CT_Num, 
            CT_Intitule = om_Client.CT_Intitule 
        };
        clients.Add(client);
    }
    return clients.ToArray();
});
// Liste des articles Sage
app.MapGet("/articles", [Authorize(Roles = "sage100.gestcom")] () =>
{
    List<Article> articles = new List<Article>();
    foreach (IBOArticle3 om_Article in om_CIAL.FactoryArticle.List)
    {
        Article article = new Article()
        {
            AR_Ref = om_Article.AR_Ref,
            AR_Design = om_Article.AR_Design
        };
        articles.Add(article);
    }
    return articles.ToArray();
});


app.MapPost("/clients", [Authorize(Roles = "sage100.compta")] (Client client) =>
{
    var om_Client = om_CPTA.FactoryClient.Create() as IBOClient3;
    om_Client.CT_Num = client.CT_Num;
    om_Client.CT_Intitule = client.CT_Intitule;
    om_Client.SetDefault();
    om_Client.WriteDefault();
    return client;
});

app.UseAuthentication();
app.UseAuthorization();
app.Run();