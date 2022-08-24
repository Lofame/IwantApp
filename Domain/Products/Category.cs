namespace IWantApp.Domain.Products;

public class Category : Entity
{ 
    public string Name { get; set; }
    public bool Active { get; set; }


   public Category(string name,string createdBy,string editedBy)
    {

        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Category>()
                   .IsNotNullOrWhiteSpace(Name, "Name")
                   .IsGreaterOrEqualsThan(Name, 3, "Name")
                   .IsNotNullOrWhiteSpace(CreatedBy, "CreatedBy")
                   .IsNotNullOrWhiteSpace(EditedBy, "EditedBy");
        AddNotifications(contract);
    }

    public void EditInfo(string name,bool active,string editedBy)
    {
        Active = active;
        Name = name;
        EditedBy=editedBy;
        EditedOn = DateTime.Now;

        Validate();
    }

}
