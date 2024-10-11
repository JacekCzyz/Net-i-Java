using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aplikacja_webowa2.Data;
using Aplikacja_webowa2.Models;

namespace Aplikacja_webowa2.Pages.Muzyka
{
    public class CreateModel : PageModel
    {
        private readonly Aplikacja_webowa2.Data.Aplikacja_webowa2Context _context;

        public CreateModel(Aplikacja_webowa2.Data.Aplikacja_webowa2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Music Music { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Music == null || Music == null)
            {
                return Page();
            }

            _context.Music.Add(Music);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
