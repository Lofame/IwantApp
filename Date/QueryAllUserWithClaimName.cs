using Dapper;
using IWantApp.Endpoints.Employee;
using Microsoft.Data.SqlClient;

namespace IWantApp.Date;

public class QueryAllUserWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUserWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);

        var t = configuration["ConnectionStrings:IWantDb"];
        var query = @"select Email,claimvalue as name 
              from AspNetUsers u inner 
              join AspNetUserClaims c on u.id = c.userId and claimtype = 'Name'
              order by name
              OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
        return db.Query<EmployeeResponse>(
            query,
            new { page, rows }
            );
    }
}
