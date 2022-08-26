namespace IWantApp.Endpoints.Orders;

public record OrderResponse(Guid id,string ClientEmail,IEnumerable<OrderProduct> Products,string DeliveryAddress);

public record OrderProduct(Guid Id,string Name);
