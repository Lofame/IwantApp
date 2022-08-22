using IWantApp.Date;
using IWantApp.Domain.Products;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";

    public static string[] Methods = new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {

        //if(string.IsNullOrWhiteSpace(categoryRequest.Name))
        //    return Results.BadRequest("Name is required");

        var category = new Category(categoryRequest.Name, "luando", "luando");

        if (!category.IsValid)
        {
            //ConvertToProblemsDatails() é uma classe extendida
            return Results.ValidationProblem(category.Notifications.ConvertToProblemsDatails()); //mensagem padrao de erro Api pelo ValodationProblem
        }
            

        context.Categories.Add(category);
        context.SaveChanges();



        return Results.Created($"/categories/{category.Id}", category.Id);
    }

}
