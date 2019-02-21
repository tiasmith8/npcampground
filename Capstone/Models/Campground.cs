using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{


    //Represents a row in the database campground table
    public class Campground
    {
        Dictionary<int, string> monthConversion = new Dictionary<int, string>()
        {
            { 1,"January"},
            { 2,"February"},
            { 3,"March"},
            { 4,"April"},
            { 5,"May"},
            { 6,"June"},
            { 7,"July"},
            { 8,"August"},
            { 9,"September"},
            { 10,"October"},
            { 11,"November"},
            { 12,"December"}
        };

        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenMonth { get; set; }
        public int ClosedMonth { get; set; }
        public decimal DailyFee { get; set; }

        //Display the campground information
        public override string ToString()
        {
            return $"#  {CampgroundId.ToString().PadRight(6)} {Name.PadRight(15)} {monthConversion[OpenMonth]}     {monthConversion[ClosedMonth]}      { DailyFee:C2} ";
        }

    }
}
