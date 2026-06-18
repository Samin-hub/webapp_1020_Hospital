using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using webapp_1020.Data;

namespace webapp_1020.Pages.Admin_1020
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalCategories { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalAppointments { get; set; }

        public async Task OnGetAsync()
        {
            TotalCategories = await _context.Categories.CountAsync();
            TotalDoctors = await _context.Doctors.CountAsync();
            TotalAppointments = await _context.Appointments.CountAsync();
        }
    }
}