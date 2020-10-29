using AutoMapper;
using PociagDoZyskow.DataAccess.Entities.ExternalDataReads;

namespace PociagDoZyskow.HistoricalDataSeeder.DependencyInjection
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTO.FinancialReportTimeScan, FinancialReportTimeScan>().ReverseMap();
            CreateMap<DTO.CompanyDataScan, CompanyDataScan>().ReverseMap();
        }
    }
}
