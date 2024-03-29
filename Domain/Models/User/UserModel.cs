﻿using System;
using System.Collections.Generic;

namespace Domain.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Userpassword { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string Role { get; set; } = "DefaultRole";

        public List<Ownership> Ownerships { get; set; }
    }
}
