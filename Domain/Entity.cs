namespace IWantApp.Domain;


//abstract so pode ser herdada e não instanciada 
public abstract class Entity
{

    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }


    public Entity()
    {
        Id = Guid.NewGuid();
    }
}
