using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl68SpeciesgroupRepository : Repository<Tbl68Speciesgroup>, ITbl68SpeciesgroupRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl68SpeciesgroupRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
