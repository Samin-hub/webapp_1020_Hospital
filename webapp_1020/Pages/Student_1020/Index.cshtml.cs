using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp_1020.Data;
using webapp_1020.Models;

namespace webapp_1020.Pages.Student_1020
{
    [Authorize(Roles = "Student")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; } // সার্চ বক্সের ডেটা রাখার জন্য

        public async Task OnGetAsync()
        {
            string currentStudentEmail = User.Identity?.Name ?? "";

            // প্রশ্ন অনুযায়ী লজিক: শুধুমাত্র লগইন করা নির্দিষ্ট স্টুডেন্টের ডেটাই আসবে
            var appointmentsQuery = _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientEmail == currentStudentEmail);

            // সার্চ ফিল্টারিং লজিক (ডাক্তারের নাম অথবা অ্যাপয়েন্টমেন্ট স্ট্যাটাস অনুযায়ী)
            if (!string.IsNullOrEmpty(SearchString))
            {
                appointmentsQuery = appointmentsQuery.Where(s => s.Doctor!.Name.Contains(SearchString)
                                                              || s.Status.Contains(SearchString));
            }

            Appointment = await appointmentsQuery.ToListAsync();
        }
    }
}