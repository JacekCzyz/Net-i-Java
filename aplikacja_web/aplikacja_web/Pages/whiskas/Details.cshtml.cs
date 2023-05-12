﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly aplikacja_web.Data.aplikacja_webContext _context;

        public DetailsModel(aplikacja_web.Data.aplikacja_webContext context)
        {
            _context = context;
        }

      public whisky whisky { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.whisky == null)
            {
                return NotFound();
            }

            var whisky = await _context.whisky.FirstOrDefaultAsync(m => m.ID == id);
            if (whisky == null)
            {
                return NotFound();
            }
            else 
            {
                whisky = whisky;
            }
            return Page();
        }
    }
}
