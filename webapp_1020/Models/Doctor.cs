using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapp_1020.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ডাক্তারের নাম আবশ্যক")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "ডিগ্রী বা কোয়ালিফিকেশন দিন")]
        public string Qualification { get; set; } = string.Empty;

        // Foreign Key for Category
        [Required(ErrorMessage = "ডিপার্টমেন্ট বা ক্যাটাগরি সিলেক্ট করুন")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Navigation for Appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}