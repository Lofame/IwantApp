using IWantApp.Date;
using IWantApp.Domain.Products;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";

    public static string[] Methods = new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public async static Task<IResult> Action(CategoryRequest categoryRequest,HttpContext http, ApplicationDbContext context)
    {

        //if(string.IsNullOrWhiteSpace(categoryRequest.Name))
        //    return Results.BadRequest("Name is required");
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name, userId, userId);

        if (!category.IsValid)
        {
            //ConvertToProblemsDatails() é uma classe extendida
            return Results.ValidationProblem(category.Notifications.ConvertToProblemsDatails()); //mensagem padrao de erro Api pelo ValodationProblem
        }
            

       await context.Categories.AddAsync(category);
       await context.SaveChangesAsync();



        return Results.Created($"/categories/{category.Id}", category.Id);
    }

}
