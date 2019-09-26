using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Connect4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<User> ActiveUsers { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}
