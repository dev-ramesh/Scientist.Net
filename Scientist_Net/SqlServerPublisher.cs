using GitHub;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scientist_Net
{
    class SqlServerPublisher:IResultPublisher
    {

        public Task Publish<T, TClean>(Result<T, TClean> result)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ExperimentDb"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT into SqlPublisher (Date, AppVersion, Name, IsMatch, ControlValue, CandidateValue, ControlDurationMs, CandidateDurationMs, Context) VALUES (@Date, @AppVersion, @Name, @IsMatch, @ControlValue, @CandidateValue, @ControlDurationMs, @CandidateDurationMs, @Context)";

                connection.Open();

                using (SqlCommand insertCommand = new SqlCommand(sql, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    insertCommand.Parameters.AddWithValue("@AppVersion", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    insertCommand.Parameters.AddWithValue("@Name", result.ExperimentName);
                    insertCommand.Parameters.AddWithValue("@IsMatch", result.Matched);
                    insertCommand.Parameters.AddWithValue("@ControlValue", result.Control.Value);
                    insertCommand.Parameters.AddWithValue("@ControlDurationMs", result.Control.Duration.TotalMilliseconds);

                    if (result.Candidates.Any())
                    {
                        insertCommand.Parameters.AddWithValue("@CandidateValue", result.Candidates[0].Value);
                        insertCommand.Parameters.AddWithValue("@CandidateDurationMs", result.Candidates[0].Duration.TotalMilliseconds);
                    }

                    if (result.Contexts.Any())
                    {
                        insertCommand.Parameters.AddWithValue("@Context", result.Contexts.First().Value);
                    }

                    insertCommand.ExecuteNonQuery();
                }
            }

            return Task.FromResult(0);
        }

       
    }
}
