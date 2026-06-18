using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using webapp_1020.Data;
using webapp_1020.Models;

namespace webapp_1020.Pages.Student_1020
{
    [Authorize(Roles = "Student")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // আমরা এখানে সরাসরি প্রপার্টিগুলোকে বাইন্ড করব যাতে ডাটা মিস না হয়
        [BindProperty]
        public string PatientName { get; set; } = string.Empty;

        [BindProperty]
        public string Phone { get; set; } = string.Empty;

        [BindProperty]
        public int DoctorId { get; set; }

        [BindProperty]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        public IActionResult OnGet()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ব্যাকএন্ডে আমরা নতুন একটি Appointment অবজেক্ট তৈরি করে ডাটাবেজে সেভ করব
            var newAppointment = new Appointment
            {
                PatientName = this.PatientName,
                PatientEmail = User.Identity?.Name ?? "student1020@hospital.com", // লগইন করা ইউজারের ইমেইল অটো নিবে
                Phone = this.Phone,
                DoctorId = this.DoctorId,
                AppointmentDate = this.AppointmentDate,
                Status = "Pending"
            };

            // ডাটাবেজে সেভ করা
            _context.Appointments.Add(newAppointment);
            await _context.SaveChangesAsync();

            // সেভ হওয়ার পর সরাসরি মেইন ড্যাশবোর্ড বা ইনডেক্স পেজে পাঠিয়ে দেবে
            return RedirectToPage("./Index");
        }
    }
}