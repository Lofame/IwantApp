namespace IWantApp.Endpoints.Orders;

public class OderGet
{
    public static string Template => "/orders/{id}";

    public static string[] Methods = new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(
        Guid id, HttpContext http,ApplicationDbContext context,UserManager<IdentityUser> userManager)
    {
        var clientClaims = http.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeClaim = http.User.Claims
            .FirstOrDefault(c => c.Type == "EmployeeCode");

        var order = context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);

        if (order.ClientId != clientClaims.Value && employeeClaim == null)
            return Results.Forbid();

        var client = await userManager.FindByIdAsync(order.ClientId);

        var productResponse = order.Products.Select(p => new OrderProduct(p.Id, p.Name));

        var orderResponse = new OrderResponse(
            order.Id, client.Email, productResponse, order.DeliveryAdress);

        return Results.Ok(orderResponse);
    }
}
