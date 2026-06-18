using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapp_1020.Data;
using webapp_1020.Models;

namespace webapp_1020.Pages.Admin_1020.Doctors
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctor { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Doctor = await _context.Doctors
                .Include(d => d.Category).ToListAsync();
        }
    }
}