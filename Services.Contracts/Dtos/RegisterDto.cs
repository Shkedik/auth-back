﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts.Dtos
{
    public class RegisterDto
    {
        public String Email { get; set; }

        public String Password { get; set; }

        public String ConfirmPasword { get; set; }

    }
}
