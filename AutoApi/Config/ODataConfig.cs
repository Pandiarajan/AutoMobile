using CarDataContract;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace AutoApi.Config
{
    public class ODataConfig
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Car>("Cars");
            return builder.GetEdmModel();
        }
    }
}
