using System;
using System.Text.RegularExpressions;

namespace NetworkIdValidator{
    public class NetworkIdInputValidator{
        public static bool NetworkIDInputValidator(string networkId)
        {
            string formatPattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}/\d{1,2}$";
            string valueLimitPattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\/\d{1,2}$";

            bool isValidFormat = Regex.IsMatch(networkId, formatPattern);

            if (!isValidFormat)
            {
                return false;
            }

            bool isValidValue = Regex.IsMatch(networkId, valueLimitPattern);

            if (!isValidValue)
            {
                return false;
            }

            return true;
        }
    }
}
