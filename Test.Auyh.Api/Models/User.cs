﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Test.Auyth.Api.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
