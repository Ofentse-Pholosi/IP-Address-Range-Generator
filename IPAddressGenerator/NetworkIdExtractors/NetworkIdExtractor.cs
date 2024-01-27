using System;

namespace NetworkIdExtractor
{
    public class IdExtractor{
        public static int ExtractSubnetMaskLength(string networkId)
        {
            string[] parts = networkId.Split('/');
            int subnetMaskLength;
            
            // Check if the array has two parts and the second part can be parsed as an integer
            if (parts.Length == 2 && int.TryParse(parts[1], out int subnetLength))
            {
                // Condition checking if the subnet mask length is within the valid range
                if (subnetLength < 24 && subnetLength > 32)
                {
                    throw new ArgumentException("Error! Invalid subnet mask length. Please try again");
                }
                else
                {
                    subnetMaskLength = subnetLength;
                }
            }
            else
            {
                throw new ArgumentException("Error! Invalid network ID format. Unable to extract subnet mask length. Please try again");
            }

            return subnetMaskLength;
        }
    }
}