using System.Collections.Generic;
using Abp.Application.Services.Dto;
using SOSOWJB.Framework.Editions.Dto;

namespace SOSOWJB.Framework.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}