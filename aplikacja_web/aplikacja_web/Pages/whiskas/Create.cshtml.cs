using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using aplikacja_web.Data;
using aplikacja_web.models;

namespace aplikacja_web.Pages.whiskas
{
    public class CreateModel : PageModel
    {
        private readonly aplikacja_web.Data.aplikacja_webContext _context;

        public CreateModel(aplikacja_web.Data.aplikacja_webContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public whisky whisky { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.whisky == null || whisky == null)
            {
                return Page();
            }

            _context.whisky.Add(whisky);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
