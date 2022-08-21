namespace IWantApp.Domain.Products;

public class Category : Entity
{ 
    public string Name { get; set; }
    public bool Active { get; set; } = true; //propriedade para saber se esta ativa ou não
}
