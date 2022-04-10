﻿using System;

namespace XeynergyCodeChallenge.Application.Queries
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid AttachedCustomerId { get; set; }
    }
}
