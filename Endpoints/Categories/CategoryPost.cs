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
            var erros = category.Notifications.GroupBy(g => g.Key).ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
            return Results.ValidationProblem(erros); //mensagem padrao de erro Api pelo ValodationProblem
        }
            

        context.Categories.Add(category);
        context.SaveChanges();



        return Results.Created($"/categories/{category.Id}", category.Id);
    }

}
