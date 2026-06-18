using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace webapp_1020.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Student" };

            // ১. রোলস তৈরি করা (যদি না থাকে)
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // ২. অ্যাডমিন টেস্ট ইউজার তৈরি করা
            string adminEmail = "admin1020@hospital.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var createPowerUser = await userManager.CreateAsync(newAdmin, "Admin@1020");
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }

            // ৩. স্টুডেন্ট টেস্ট ইউজার তৈরি করা
            string studentEmail = "student1020@hospital.com";
            var studentUser = await userManager.FindByEmailAsync(studentEmail);
            if (studentUser == null)
            {
                var newStudent = new IdentityUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    EmailConfirmed = true
                };
                var createStudentUser = await userManager.CreateAsync(newStudent, "Student@1020");
                if (createStudentUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(newStudent, "Student");
                }
            }
        }
    }
}