using System.Security.Claims;
using System.Text.Encodings.Web;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Google.Apis.Auth;
using NET6_API_with_Generic_SQL_Repository.Entities;

namespace EqspensesAPI.Authentication;

public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private static User? _user;
    private readonly FirebaseApp _firebaseApp;

    public FirebaseAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, FirebaseApp firebaseApp) : base(options, logger, encoder, clock)
    {
        _firebaseApp = firebaseApp;
    }
    private void SetUser(IReadOnlyDictionary<string, object> firebaseTokenClaims)
    {
        var name = ("name", firebaseTokenClaims["name"].ToString());
        var names = name.Item2.Split();
        _user = new User()
        {
            GoogleUserId = ("id", firebaseTokenClaims["user_id"].ToString()).Item2,
            Email = ("email", firebaseTokenClaims["email"].ToString()).Item2,
            Name = ("name", firebaseTokenClaims["name"].ToString()).Item2,

        };
    }

    public static User? GetUser()
    {
        return _user;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.NoResult();
        }

        string bearerToken = Context.Request.Headers["Authorization"];

        if (bearerToken == null || !bearerToken.StartsWith("Bearer "))
        {
            return AuthenticateResult.Fail("Invalid Scheme");
        }


        var token = bearerToken["Bearer ".Length..];


        try
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance
                .VerifyIdTokenAsync(token);
            string uid = decodedToken.Uid;
        
        
            var firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);
 
        
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                
                new(ToClaims(firebaseToken.Claims),nameof(FirebaseAuthenticationHandler))
            }), JwtBearerDefaults.AuthenticationScheme));
        }
        catch (Exception e)
        {
            return AuthenticateResult.Fail(e);
        }

      


    }
    
    

    private IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> firebaseTokenClaims)
    {

        SetUser(firebaseTokenClaims);
        
        return new List<Claim>
        {
            new Claim("id", firebaseTokenClaims["user_id"].ToString()),
            new Claim("email", firebaseTokenClaims["email"].ToString()),
            new Claim("name", firebaseTokenClaims["name"].ToString())
  
        };
        
        
    }



}