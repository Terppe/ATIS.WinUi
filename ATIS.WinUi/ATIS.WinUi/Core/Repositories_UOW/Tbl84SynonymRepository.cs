using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl84SynonymRepository : Repository<Tbl84Synonym>, ITbl84SynonymRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl84SynonymRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
