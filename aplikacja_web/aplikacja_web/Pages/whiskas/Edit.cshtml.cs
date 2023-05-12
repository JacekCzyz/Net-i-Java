using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aplikacja_web.Data;
using aplikacja_web.models;

namespace aplikacja_web.Pages.whiskas
{
    public class EditModel : PageModel
    {
        private readonly aplikacja_web.Data.aplikacja_webContext _context;

        public EditModel(aplikacja_web.Data.aplikacja_webContext context)
        {
            _context = context;
        }

        [BindProperty]
        public whisky whisky { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.whisky == null)
            {
                return NotFound();
            }

            var whisky =  await _context.whisky.FirstOrDefaultAsync(m => m.ID == id);
            if (whisky == null)
            {
                return NotFound();
            }
            whisky = whisky;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(whisky).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!whiskyExists(whisky.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool whiskyExists(int id)
        {
          return (_context.whisky?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
