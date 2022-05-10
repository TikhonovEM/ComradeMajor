namespace ComradeMajor;

public interface IStatisticsRepository<T>
{
    int ExecuteScalar(string command);
    T GetStatsByDate(DateTime date);
}