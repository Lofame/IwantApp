using IWantApp.Domain.Users;
using static IWantApp.Endpoints.Clients.ClientPost;

namespace IWantApp.Endpoints.Employee;

public class EmployeePost
{
    public static string Template => "/employees";

    public static string[] Methods = new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    public async static Task<IResult> Action(Employee.EmployeeRequest employeeRequest,HttpContext http, UserCreator userCreator)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
       
        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode",employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId)
        };


        (IdentityResult identity, string userId) result =
        await userCreator.Create(employeeRequest.Email, employeeRequest.Password, userClaims);


        if (!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemsDatails());



        return Results.Created($"/employees/{result.userId}", result.userId);
    }

}
