using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace webapp_1020.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ক্যাটাগরির নাম দিতেই হবে")]
        [MinLength(3, ErrorMessage = "নাম অন্তত ৩ অক্ষরের হতে হবে")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // 1-to-Many Relationship: এক ক্যাটাগরিতে অনেক ডাক্তার থাকবেন
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}