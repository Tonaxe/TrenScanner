using DavxeShop.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DavxeShop.Persistance
{
    public class TrenDboHelper : ITrenDboHelper
    {
        private readonly TrenScannerContext _context;

        public TrenDboHelper(TrenScannerContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextTandaAsync()
        {
            var maxTanda = await _context.Trenes
                .MaxAsync(t => (int?)t.Tanda) ?? 0;

            return maxTanda == 0 ? 1 : maxTanda + 1;
        }
    }
}
