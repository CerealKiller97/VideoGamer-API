using Aplication.Searches;
using EntityConfiguration;

namespace EFServices.Services
{
    public class EFGameService: BaseService
    {
        public EFGameService(VideoGamerDbContext context) : base(context)
        {
        }

        protected override void BuildingQuery(BaseSearchRequest request)
        {
            
        }

        protected override void EntityDatabaseAvailability()
        {
            
        }
    }
}