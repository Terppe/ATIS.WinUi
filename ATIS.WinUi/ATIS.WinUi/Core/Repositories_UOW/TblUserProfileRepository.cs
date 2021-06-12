using ATIS.WinUi.Core.Interfaces_UOW;
using ATIS.WinUi.DataLayer.Models;

namespace ATIS.WinUi.Core.Repositories_UOW
{
    public class TblUserProfileRepository : Repository<TblUserProfile>, ITblUserProfileRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public TblUserProfileRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
