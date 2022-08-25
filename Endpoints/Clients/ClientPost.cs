using System.Globalization;

namespace IWantApp.Endpoints.Clients;

public partial class ClientPost
{

    public static string Template => "/clients";

    public static string[] Methods = new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public async static Task<IResult> Action(ClientRequest clientRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var newUser = new IdentityUser { UserName = clientRequest.Email, Email = clientRequest.Email };

        var result = await userManager.CreateAsync(newUser, clientRequest.Password);

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemsDatails());

        var userClaims = new List<Claim>
        {
            new Claim("Cpf",clientRequest.Cpf),
            new Claim("Name", clientRequest.Name),
            
        };

        var claimResult = await userManager.AddClaimsAsync(newUser, userClaims);


        if (!claimResult.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemsDatails());

        return Results.Created($"/clients/{newUser.Id}", newUser.Id);
    }
}
