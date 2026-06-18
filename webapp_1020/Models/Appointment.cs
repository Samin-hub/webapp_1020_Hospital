using System;
using System.ComponentModel.DataAnnotations;

namespace webapp_1020.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "রোগীর নাম আবশ্যক")]
        public string PatientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "লগইন করা রোগীর ইমেইল")]
        public string PatientEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "মোবাইল নম্বর দিন")]
        public string Phone { get; set; } = string.Empty;

        // Foreign Key for Doctor
        [Required(ErrorMessage = "ডাক্তার সিলেক্ট করা আবশ্যক")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "তারিখ ও সময় দিন")]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending"; // Pending, Approved, Cancelled
    }
}