using Crud.API.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Crud.API.Extensions
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        readonly string _connection;

        public SqlServerHealthCheck(string connection)
        {
            _connection = connection;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    await connection.OpenAsync(cancellationToken);

                    var command = connection.CreateCommand();
                    command.CommandText = "select count(id) from products";
                    var totalProducts = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));

                    var healthData = new Dictionary<string, object>
                    {
                        { "totalProducts", totalProducts },
                        { "lastUpdated", DateTime.UtcNow }
                    }; 

                    return totalProducts > 0 ? HealthCheckResult.Healthy("Healthy", healthData) : HealthCheckResult.Unhealthy();
                }
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}
