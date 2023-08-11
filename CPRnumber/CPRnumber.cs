using System.Runtime.ConstrainedExecution;
using System;

namespace CPRnumber
{
    public class CPRnr
    {
        private string cprTrimmed;
        public string CPRtrimmed() { return cprTrimmed; }

        private string cprNoDash;
        public string CPRnoDash() { return cprNoDash; }

        private string cprWithDash;
        public string CPRwithDash () { return cprWithDash; }


        /// <summary>
        /// Initializes a new instance of the CPRnumber class with the provided CPR number.
        /// </summary>
        /// <param name="cprNumber">The CPR number to be processed. Accepts numbers with and without leading 0 and/or dash.</param>
        /// <exception cref="Exception">Thrown when the provided CPR number is not in a recognizable format.</exception>
        public CPRnr(string cprNumber) {
            // Remove dashes from the input CPR number
            cprTrimmed = cprNumber.Replace("-", "");

            // Check if the trimmed CPR number is a valid integer
            if (!long.TryParse(cprTrimmed, out _)) {
                throw new Exception("CPR not in a recognizable format");
            }

            // Ensure the length of the trimmed CPR number is either 9 or 10 digits
            if (cprTrimmed.Length == 9) {
                cprNoDash = "0" + cprTrimmed;
            } else if (cprTrimmed.Length == 10) {
                cprNoDash = cprTrimmed;
            } else {
                throw new Exception("CPR not in a recognizable format");
            }

            // Create the CPR number with dashes
            cprWithDash = cprNoDash.Substring(0, 6) + "-" + cprNoDash.Substring(6, 4);
        }


        /// <summary>
        /// Retrieves the birthdate associated with the CPR number as a DateTime object.
        /// </summary>
        /// <returns>A DateTime object representing the birthdate extracted from the CPR number.</returns>
        public DateTime GetBirthday() {
            // Extract day, month, and year components from the CPR number
            string day = cprNoDash.Substring(0, 2);
            string month = cprNoDash.Substring(2, 2);
            string year = cprNoDash.Substring(4, 2);

            // Determine the century component based on CPR's seventh digit
            string century = "19";
            string seven = cprNoDash.Substring(6, 1);

            if ((seven == "4" || seven == "9") && int.Parse(year) <= 36) {
                century = "20";
            }
            if (int.Parse(seven) >= 5 && int.Parse(seven) <= 8) {
                if (int.Parse(year) <= 57) {
                    century = "20";
                } else {
                    century = "18";
                }
            }

            // Parse year, month, and day components to integers
            int fullYear = int.Parse($"{century}{year}");
            int parsedMonth = int.Parse(month);
            int parsedDay = int.Parse(day);

            // Create and return a DateTime object representing the birthdate
            return new DateTime(fullYear, parsedMonth, parsedDay);
        }


        /// <summary>
        /// Determines whether the CPR number passes the modulus 11 test.
        /// 
        /// Indtil 1. oktober 2007 kunne man ved hjælp af det såkaldte kontrolciffer udføre 
        /// en beregning og afgøre om personnummeret var korrekt angivet.
        /// CPR-kontoret opfordrer derfor alle som bygger computersystemer til at kunne håndtere
        /// personnumre uden modulus-kontrollen.En konsekvens ved ikke at håndtere personnumre
        /// uden modulus-kontrollen er at nogle personer kan blive nægtet adgang til systemet
        /// uden at det er hensigten.
        /// 
        /// </summary>
        /// <returns>True if the CPR number passes the modulus 11 test, false otherwise.</returns>

        public bool DoesPassModulus() {
            // Factors used in the modulus 11 test
            int[] factor = new int[] { 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            // Extract the control number from the CPR number
            char ctrlNumber = cprNoDash[9];

            // Initialize the sum to perform the modulus 11 calculation
            int sum = 0;

            // Calculate the sum of products of corresponding digits and factors
            for (int i = 0; i < factor.Length; i++) {
                sum += factor[i] * int.Parse(cprNoDash[i].ToString());
            }

            // Determine if the CPR number passes the modulus 11 test
            return (sum + int.Parse(ctrlNumber.ToString())) % 11 == 0;
        }

        /// <summary>
        /// Determines the gender associated with the CPR number.
        /// </summary>
        /// <returns>True if the gender is male, false if it's female.</returns>
        public bool IsMale() {
            // Extract the last digit of the CPR number (gender indicator)
            int genderDigit = int.Parse(cprNoDash[9].ToString());

            // Determine if the gender is male based on the gender indicator
            return genderDigit % 2 != 0;
        }
    }
}
