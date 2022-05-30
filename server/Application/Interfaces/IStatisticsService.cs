namespace Application.Interfaces
{
    using System.Collections.Generic;
    using Application.DTO.Response.Statistics;
    using Application.Helpers;
    using Domain.Models;

    public interface IStatisticsService
    {
        List<SalesTimeStatistics> GetOrdersForMonth(User user, StatisticsType type, bool finished);

        SalesStatisticsForPeriod GetSalesForMonth(User user, StatisticsType type);

        SalesStatisticsForPeriod GetSalesTotal(User user, StatisticsType type);

        string GetStatFile(User user, string fromStr, string toStr);
    }
}
