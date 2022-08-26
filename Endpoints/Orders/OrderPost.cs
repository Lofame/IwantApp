using IWantApp.Domain.Orders;
using IWantApp.Domain.Products;

namespace IWantApp.Endpoints.Orders;

public class OrderPost
{
    public static string Template => "/orders";

    public static string[] Methods = new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http,ApplicationDbContext context)
    {

        var clientId = http.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims
            .First(c => c.Type == "Name").Value;

        //if (orderRequest.ProductIds == null || !orderRequest.ProductIds.Any())
        //    return Results.BadRequest("Produto é obrigatório para pedido");

        //if(string.IsNullOrEmpty(orderRequest.DeliveryAdress))
        //    return Results.BadRequest("Endereço de entrega é obrigatório");


        //var products = new List<Product>();

        //foreach(var item in orderRequest.ProductIds)
        //{
        //    var product = context.Products.First(p => p.Id == item);
        //    products.Add(product);

        //}
        //subistituido por esse

        List<Product> productsFound = null;

        if(orderRequest.ProductIds != null && orderRequest.ProductIds.Any())
            productsFound = context.Products.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToList();
      

        var order = new Order(clientId, clientName, productsFound, orderRequest.DeliveryAdress);

        if (!order.IsValid)
        {
            return Results.ValidationProblem(order.Notifications.ConvertToProblemsDatails());
        }

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/orders/{order.Id}",order.Id);
    }
}
