using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aplikacja_webowa2.Data;
using Aplikacja_webowa2.Models;
using Microsoft.Data.SqlClient;
using System.Web.Mvc;

namespace Aplikacja_webowa2.Pages.Muzyka
{
    public class IndexModel : PageModel
    {
        private readonly Aplikacja_webowa2.Data.Aplikacja_webowa2Context _context;

        public IndexModel(Aplikacja_webowa2.Data.Aplikacja_webowa2Context context)
        {
            _context = context;
        }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string DateSort { get; set; }
        public string GenreSort { get; set; }
        public string AlbumSort { get; set; }
        public string CurrentFilter { get; set; }

        public IList<Music> Music { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            if (_context.Music != null)
            {
                AuthorSort = String.IsNullOrEmpty(sortOrder) ? "Author_desc" : "Author";
                TitleSort = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "Title";
                AlbumSort = String.IsNullOrEmpty(sortOrder) ? "Album_desc" : "Album";
                GenreSort = String.IsNullOrEmpty(sortOrder) ? "Genre_desc" : "";
                DateSort = sortOrder == "Date" ? "Date_desc" : "Date";


                IQueryable<Music> music = from s in _context.Music
                                          select s;

                if (!string.IsNullOrEmpty(searchString))
                {
                    music = music.Where(s => s.Title.ToLower().Contains(searchString.ToLower()));
                }

                switch (sortOrder)
                {
                    case "Title_desc":
                        music = music.OrderByDescending(s => s.Title);
                        break;
                    case "Title":
                        music = music.OrderByDescending(s => s.Title);
                        break;

                    case "Genre_desc":
                        music = music.OrderByDescending(s => s.Genre);
                        break;
                    case "Genre":
                        music = music.OrderByDescending(s => s.Genre);
                        break;

                    case "Date":
                        music = music.OrderBy(s => s.ReleaseYear);
                        break;

                    case "Date_desc":
                        music = music.OrderByDescending(s => s.ReleaseYear);
                        break;

                    case "Album_desc":
                        music = music.OrderByDescending(s => s.Album);
                        break;

                    case "Album":
                        music = music.OrderBy(s => s.Album);
                        break;

                    case "Author":
                        music = music.OrderBy(s => s.Author);
                        break;

                    case "Author_desc":
                        music = music.OrderByDescending(s => s.Author);
                        break;

                    default:
                        music = music.OrderBy(s => s.Title);
                        break;
                }
                Music = await music.ToListAsync();
            }
        }
    }
}
