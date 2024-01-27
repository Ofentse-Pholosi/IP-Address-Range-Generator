using System;
using System.Net;
using SubnestDictionary;

namespace IPAddressCalculator
{
    public class IPAddressRangeCalculator{
        public static void CalculateIPRanges(string cidr, int requiredSubnets)
        {
            IPAddress networkAddress;
            int subnetMaskLength;

            // Parse the CIDR notation
            var cidrParts = cidr.Split('/');
            string networkAddressString = cidrParts[0];
            string subnetMaskLengthString = cidrParts[1];
            string binarySubnetMask = SubnetMaskDictionary.GetBinarySubnetMask(subnetMaskLengthString);

            Console.WriteLine("\n ==============================================================");
            Console.WriteLine($"Network Address: {networkAddressString}");
            Console.WriteLine($"Original Subnet Mask Length: {subnetMaskLengthString}\n");
            Console.WriteLine($"Binary Subnet Mask: {binarySubnetMask}");

            // Parse the CIDR notation
            if (IPAddress.TryParse(networkAddressString, out networkAddress) && int.TryParse(subnetMaskLengthString, out subnetMaskLength))
            {
                // Calculate the number of host bits 
                int hostBits = 32 - subnetMaskLength;
                double numBitsToBorrow = Math.Round(Math.Log2(requiredSubnets),0); 
                Console.WriteLine("\n ============ Calculate the number of host bits ============");
                Console.WriteLine($"Number of bits to borrow: {numBitsToBorrow}");
                hostBits -= (int)numBitsToBorrow;
                Console.WriteLine($"New number of host bits: {hostBits}");

                // New subnet mask after borrowing
                subnetMaskLength += (int)numBitsToBorrow;

                    // Showcase IPv4 subnet mask as well
                Console.WriteLine($"New Subnet Mask after borrowing: /{subnetMaskLength}");

                
                // Calculate the number of hosts per subnet
                Console.WriteLine("\n ============ Calculate the number of usable host addresses per subnet ============");
                int hostsPerSubnet = (int)Math.Pow(2, hostBits);
                Console.WriteLine($"Number of hosts per subnet: {hostsPerSubnet}");

                // Check if the required number of subnets is feasible
                if (requiredSubnets != 0 && requiredSubnets < 128 )
                {
                    requiredSubnets = SubnetMaskDictionary.provisionalSubnetsDictionary(requiredSubnets);
                    // Amend requiredSubnets to new value

                    Console.WriteLine("============ IP Address Ranges: ============ ");
                    Console.WriteLine("\tNetwork Address - Broadcast Address");
                    showIPAddressRanges(networkAddress.ToString(), hostsPerSubnet, requiredSubnets);
                }
                else
                {
                    Console.WriteLine("Error: Required number of subnets exceeds available subnets with the given subnet mask.");
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid CIDR notation.");
            }
        }

        private static void showIPAddressRanges(string networkAddress, int hostsPerSubnet, int provisionedSubnets )
        {
            var spliNetworkAddress = networkAddress.Split('.');
            const int octetMax = 256;
            // Splitting network address into different int octets to perform arithmetic operations
            int fourthOctet = int.Parse(spliNetworkAddress[3]);
            int thirdOctet = int.Parse(spliNetworkAddress[2]);
            int secondOctet = int.Parse(spliNetworkAddress[1]);
            int firstOctet = int.Parse(spliNetworkAddress[0]);

            for( int i = 1; i < provisionedSubnets; i++ )
            {
                int counter = 1;
                //  to handle fourth octet addition
                while(counter <= provisionedSubnets && fourthOctet < octetMax)
                {

                    var originalNetworkAddress = $"{firstOctet}.{secondOctet}.{thirdOctet}.{fourthOctet}";

                    fourthOctet += hostsPerSubnet -1; 
                    var joinNetworkAddress = $"{firstOctet}.{secondOctet}.{thirdOctet}.{fourthOctet}";
                    Console.WriteLine($"Subnet {counter}: {originalNetworkAddress} - {joinNetworkAddress}");
                    fourthOctet += 1;
                    counter++;

                    // Resetting fourthOctet if it exceeds octetMax
                    if (counter <= provisionedSubnets && (fourthOctet + hostsPerSubnet) > 256)
                    {
                        thirdOctet++;
                        fourthOctet = 0;
                    }

                    i = counter;
                }
                

            }
        }
    }
}
