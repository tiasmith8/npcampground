﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int Site_id { get; set; }
        public int Campground_id { get; set; }
        public int Site_Number { get; set; }
        public int Max_Occupancy { get; set; }
        public bool Accessible { get; set; }
        public int Max_Rv_Length { get; set; }
        public bool Utilities { get; set; }

        public override string ToString()
        {
            return $"#{Site_id,-10}{Max_Occupancy,-10}{Accessible,-12}{Max_Rv_Length,-12}{Utilities,-7}";
        }
    }


}


