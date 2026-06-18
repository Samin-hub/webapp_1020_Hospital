using System;
using System.ComponentModel.DataAnnotations;

namespace webapp_1020.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        [Required]
        public string PatientEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "ডায়াগনোসিস বা রোগের বিবরণ দিন")]
        public string Diagnosis { get; set; } = string.Empty;

        [Required(ErrorMessage = "প্রেসক্রিপশন বা ওষুধের তালিকা দিন")]
        public string Prescription { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}