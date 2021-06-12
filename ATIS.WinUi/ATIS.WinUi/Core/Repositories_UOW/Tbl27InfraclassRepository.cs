using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl27InfraclassRepository : Repository<Tbl27Infraclass>, ITbl27InfraclassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl27InfraclassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
