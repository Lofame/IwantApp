namespace IWantApp.Endpoints.Clients;

public partial class ClientPost
{
    public record ClientRequest(string Email,string Password,string Name,string Cpf);
}
