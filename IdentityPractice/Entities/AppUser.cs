﻿using Microsoft.AspNetCore.Identity;

namespace IdentityPractice.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? ImagePath { get; set; }
        public string Gender { get; set; }

    }
}
