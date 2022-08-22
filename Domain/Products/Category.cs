﻿using Flunt.Validations;
namespace IWantApp.Domain.Products;

public class Category : Entity
{ 
    public string Name { get; set; }
    public bool Active { get; set; }


   public Category(string name,string createdBy,string editedBy)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrWhiteSpace(name, "Name")
            .IsGreaterOrEqualsThan(name,3,"Name")
            .IsNotNullOrWhiteSpace(createdBy, "CreatedBy")
            .IsNotNullOrWhiteSpace(editedBy, "EditedBy");
            

        AddNotifications(contract);
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

    }

}
