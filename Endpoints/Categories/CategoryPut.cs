using IWantApp.Date;
using IWantApp.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id}";

    public static string[] Methods = new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action([FromRoute] Guid Id,CategoryRequest categoryRequest, ApplicationDbContext context)
    {
       var caregory = context.Categories.Where(c => c.Id == Id).FirstOrDefault();
        if (caregory != null)
        {
            caregory.Name = categoryRequest.Name;
            caregory.Active = categoryRequest.Active;
            context.SaveChanges();
            return Results.Ok();
        }
        return Results.Problem();
        
    }

}
