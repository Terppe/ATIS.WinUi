using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class Tbl21ClassRepository : Repository<Tbl21Class>, ITbl21ClassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl21ClassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
