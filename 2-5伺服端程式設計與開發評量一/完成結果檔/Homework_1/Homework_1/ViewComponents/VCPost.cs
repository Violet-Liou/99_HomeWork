using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework_1.Models;

namespace Homework_1.ViewComponents
{
    public class VCPost : ViewComponent
    {
        private readonly MessageBoardContext _context;

        public VCPost(MessageBoardContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string MainID, bool isDel = false)
        {
            var rebook = await _context.Response
                        .Where(r => r.MainID == MainID)
                        .OrderByDescending(r => r.CreatedDate)
                        .ToListAsync();

            if (isDel)
                return View("Delete", rebook);

            return View(rebook);
        }
    }
}
