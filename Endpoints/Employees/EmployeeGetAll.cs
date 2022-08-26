using IWantApp.Date;


namespace IWantApp.Endpoints.Employee;

public class EmployeeGetAll
{
    public static string Template => "/employees";

    public static string[] Methods = new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(policy: "Employee005Policy")]
    public static async Task<IResult> Action(int? page,int? rows, QueryAllUserWithClaimName query)
    {
        

        return  Results.Ok(await query.ExecuteAsync(page.Value,rows.Value));
    }

}
