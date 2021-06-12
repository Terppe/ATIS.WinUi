using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl24SubclassRepository : Repository<Tbl24Subclass>, ITbl24SubclassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl24SubclassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
