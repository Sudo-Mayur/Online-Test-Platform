using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Online_Test_Platform.Models
{ 
        public class EmailAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                Regex re = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
#pragma warning disable CS8604 // Possible null reference argument.
            if (re.IsMatch(Convert.ToString(value)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
#pragma warning restore CS8604 // Possible null reference argument.

        }
        }

    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Regex re = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$");
#pragma warning disable CS8604 // Possible null reference argument.
            if (re.IsMatch(Convert.ToString(value)))
            {
                return true;
            }
            else
            {
                return false;
            }
#pragma warning restore CS8604 // Possible null reference argument.

        }
    }
}

