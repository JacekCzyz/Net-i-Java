using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using aplikacja_web.Data;
using aplikacja_web.models;

namespace aplikacja_web.Pages.whiskas
{
    public class IndexModel : PageModel
    {
        private readonly aplikacja_web.Data.aplikacja_webContext _context;

        public IndexModel(aplikacja_web.Data.aplikacja_webContext context)
        {
            _context = context;
        }

        public IList<whisky> whisky { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.whisky != null)
            {
                whisky = await _context.whisky.ToListAsync();
            }
        }
    }
}
