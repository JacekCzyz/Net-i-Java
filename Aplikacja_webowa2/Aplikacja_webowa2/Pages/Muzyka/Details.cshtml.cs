using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aplikacja_webowa2.Data;
using Aplikacja_webowa2.Models;

namespace Aplikacja_webowa2.Pages.Muzyka
{
    public class DetailsModel : PageModel
    {
        private readonly Aplikacja_webowa2.Data.Aplikacja_webowa2Context _context;

        public DetailsModel(Aplikacja_webowa2.Data.Aplikacja_webowa2Context context)
        {
            _context = context;
        }

      public Music Music { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Music == null)
            {
                return NotFound();
            }

            var music = await _context.Music.FirstOrDefaultAsync(m => m.ID == id);
            if (music == null)
            {
                return NotFound();
            }
            else 
            {
                Music = music;
            }
            return Page();
        }
    }
}
