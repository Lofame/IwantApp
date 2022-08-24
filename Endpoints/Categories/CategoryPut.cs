using IWantApp.Date;
using IWantApp.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";

    public static string[] Methods = new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action([FromRoute] Guid Id,HttpContext http, CategoryRequest categoryRequest, ApplicationDbContext context)
    {

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var caregory = context.Categories.Where(c => c.Id == Id).FirstOrDefault();
        if (caregory == null)
            return Results.Problem();

        caregory.EditInfo(categoryRequest.Name, categoryRequest.Active,userId);

            if (!caregory.IsValid)
            return Results.ValidationProblem(caregory.Notifications.ConvertToProblemsDatails());
       
        context.SaveChanges();
        return Results.Ok();
        
        
        
    }

}
