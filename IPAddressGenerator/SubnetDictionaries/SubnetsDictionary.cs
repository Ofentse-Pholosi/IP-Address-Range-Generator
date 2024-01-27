using System;

namespace SubnestDictionary {
    public class SubnetMaskDictionary{
        public static string GetSubnetMasks(int id)
        {
            // A dictionary with predefined subnet masks
            Dictionary<int, string> subnetMasks =  new Dictionary<int, string>
            {
                { 32, "255.255.255.255"},   // 255 subnets, 1 host p/n
                { 31, "255.255.255.254"},   // 128 subnets, 2 hosts p/n
                { 30, "255.255.255.252"},   // 64 subnets,  4 hosts p/n
                { 29, "255.255.255.248"},   // 32 subnets,  8 hosts p/n
                { 28, "255.255.255.240"},   // 16 subnets,  16 hosts p/n
                { 27, "255.255.255.224"},   // 8 subnets,   32 host p/n
                { 26, "255.255.255.192"},   // 4 subnets,   64 Hosts p/n
                { 25, "255.255.255.128"},   // 2 subnets,   128 Hosts p/n
                { 24, "255.255.255.0" }     // 1 subnet,    254 Hosts p/n
            };

            // Check if the provided id exists in the dictionary
            if (subnetMasks.ContainsKey(id))
            {
                return subnetMasks[id];
            }
            else
            {
                // Handle the case where the id is not found in the dictionary
                throw new ArgumentException("Error! Subnet mask length not found for small networks. Please try again");
            }
        }

        public static string GetBinarySubnetMask(string subnetmask)
        {
            string binarySubnetMaskNotation;
            // A dictionary with predefined binary subnet masks
            Dictionary<string, string> binarySubnetMask = new Dictionary<string, string>
            {
                { "25", "11111111.11111111.11111111.00000000" },
                { "25", "11111111.11111111.11111111.10000000" },
                { "26", "11111111.11111111.11111111.11000000" },
                { "27", "11111111.11111111.11111111.11100000" },
                { "28", "11111111.11111111.11111111.11110000" },
                { "29", "11111111.11111111.11111111.11111000" },
                { "30", "11111111.11111111.11111111.11111100" },
                { "31", "11111111.11111111.11111111.11111110" },
                { "32", "11111111.11111111.11111111.11111111" }
            };

            if(binarySubnetMask.ContainsKey(subnetmask))
            {
                binarySubnetMaskNotation = binarySubnetMask[subnetmask];
                return binarySubnetMaskNotation;
            }
            else
            {
                throw new ArgumentException("Error! Subnet Mask notation provided not found");
            }
        }

        public static int provisionalSubnetsDictionary(int requiredSubnets)
        {
            int provisionedSubnets;

            Dictionary<int, int> provisionalSubnets = new Dictionary<int, int>
            {
                { 1, 256 },     // 1 Subnet, 256 hosts
                { 2, 128 },     // 2 Subnets, 128 hosts per subnet
                { 4, 64 },      // 4 Subnets, 64 hosts per subnet
                { 8, 32 },      // 8 Subents, 32 hosts per subnet
                { 16, 16 },     // 16 Subnets, 16 hosts per subnet
                { 32, 8 },      // 32 Subnets, 8 hosts per subnet
                { 64, 4 },      // 64 Subents, 4 hosts per subnet
                { 128, 2 },     // 128 Subnets, 2 hosts per Subnet
                { 256, 1 }      // 256 Subnets, 1 host per subnet (Should show error)
            };

            // Check if the required number of subnets is in the dictionary
            if (provisionalSubnets.ContainsKey(requiredSubnets))
            {
                int subnetSize = provisionalSubnets[requiredSubnets];
                int numberOfHosts = subnetSize - 2; // Subtract 2 for network and broadcast addresses
                provisionedSubnets = requiredSubnets;

                Console.WriteLine($"Number of usable IP addresses per subnet: {numberOfHosts}");
                Console.WriteLine($"Provisioning for {requiredSubnets} subnets:");
                return provisionedSubnets;
            }
            else
            {
                // If the required number of subnets is not in the dictionary, find the next higher value
                provisionedSubnets = provisionalSubnets.Keys.FirstOrDefault(k => k > requiredSubnets);
                
                if (provisionedSubnets != 0 && provisionedSubnets < 128 )
                {
                    int subnetSize = provisionalSubnets[provisionedSubnets];
                    int numberOfHosts = subnetSize - 2; // Subtract 2 for network and broadcast addresses

                    Console.WriteLine($"Number of usable IP addresses per subnet: {numberOfHosts}");
                    Console.WriteLine($"Provisioning for {requiredSubnets} subnets - rounded up to {provisionedSubnets}");
                    return provisionedSubnets;
                }
                else
                {
                    throw new ArgumentException("Error: Invalid number of required subnets. Please choose a valid value.");
                }
            }
        }

        public static int provisionalHostsPerSubnet (int requestedHosts)
        {
            int provisionedHosts;

            Dictionary<int, int> provisionalHosts = new Dictionary<int, int>
            {
                { 256, 1 },     // 256 hosts Per Subnet, 1 subnet - Not to be allowed
                { 128, 2 },     // 128 hosts Per Subnet, 2 Subnets
                { 64, 4 },      // 64 hosts Per Subnet, 4 Subnets
                { 32, 8 },      // 8 Subents, 32 hosts per subnet
                { 16, 16 },     // 16 Subnets, 16 hosts per subnet
                { 8, 32 },      // 32 Subnets, 8 hosts per subnet
                { 4, 64 },      // 64 Subents, 4 hosts per subnet
                { 2, 128 },     // 128 Subnets, 2 hosts per Subnet
                { 1, 256 }      // 256 Subnets, 1 host per subnet (Should show error)
            };

            provisionedHosts = provisionalHosts[requestedHosts];

            return provisionedHosts;

        }
    }
}