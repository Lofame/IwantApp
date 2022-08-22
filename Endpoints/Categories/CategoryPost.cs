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

        var category = new Category(categoryRequest.Name)
        {
       
            CreatedBy = "Luando",
            CreatedOn = DateTime.Now,
            EditedBy = "Luando",
            EditedOn = DateTime.Now
        };

        if (!category.IsValid)
            return Results.BadRequest(category.Notifications);

        context.Categories.Add(category);
        context.SaveChanges();



        return Results.Created($"/categories/{category.Id}", category.Id);
    }

}
