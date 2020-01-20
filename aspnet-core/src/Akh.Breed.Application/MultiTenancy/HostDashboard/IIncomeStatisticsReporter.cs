using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akh.Breed.MultiTenancy.HostDashboard.Dto;

namespace Akh.Breed.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}
