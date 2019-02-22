using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ValidateInput
    {

        //Validate date input in
        public static DateTime GetDate(string message) //Sent in string
        {
            string userInput = String.Empty;
            DateTime DateTimeValue;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0) //If not first time asking
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }

                Console.Write(message + ": ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!DateTime.TryParse(userInput, out DateTimeValue)) ;

            return DateTime.Parse(userInput);
        }



    }
}
