using ComradeMajor.Interfaces;
using ComradeMajor.Models;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace ComradeMajor;

public class StatisticsRepository : IStatisticsRepository<IScanResult>
{
    public void AddResult(IScanResult result)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IScanResult> GetStatsByDate(DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        var scanResults = new List<IScanResult>();
        using (var connection = new SqliteConnection("Data Source=stats.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @$"
                SELECT * FROM Stats 
                WHERE strftime('%s', Date) 
                BETWEEN strftime('%s', '{startOfDay.ToString("yyyy-MM-dd HH:mm:ss.fff")}') 
                AND strftime('%s', '{endOfDay.ToString("yyyy-MM-dd HH:mm:ss.fff")}');
            ";
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var scanResult = new ScanResult()
                    {
                        Date = reader.GetDateTime(0)
                    };
                    scanResult.ActiveProcess = JsonConvert.DeserializeObject<ProcessInfo>(reader.GetString(1));
                    scanResult.Processes.AddRange(JsonConvert.DeserializeObject<List<ProcessInfo>>(reader.GetString(2)));

                    scanResults.Add(scanResult);
                }
            }

            connection.Close();
        }

        return scanResults;
    }
}