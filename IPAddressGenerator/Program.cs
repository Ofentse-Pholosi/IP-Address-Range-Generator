// Intermediate C# Skillset level
// Console Application meant for generating IPv4 IP addresses for small networks given a Subnet Mask

using NetworkIdValidator;
using NetworkIdExtractor;
using SubnestDictionary;
using IPAddressCalculator;

namespace IPAddressGenerator{
    class Program{
        static void Main()
        {
            // Local program variables
            string networkId, subnetMask;
            int requiredSubnets, subnetMaskLength, requiredHostsPerSubnet;

            Console.WriteLine("======= IP Address Range Calculator ======= \n");
            
            Console.WriteLine("Please enter a Network ID");
            Console.WriteLine("Network ID: (E.g 192.168.4.0/24)");
            networkId = Console.ReadLine();

            // Validating inputted network id:
            bool isValidNetworkId = NetworkIdInputValidator.NetworkIDInputValidator(networkId);
            while (!isValidNetworkId)
            {
                Console.WriteLine("Error! The network ID is not in the requested format (###.###.###.###/##). Please try again");
                networkId = Console.ReadLine();
                isValidNetworkId = NetworkIdInputValidator.NetworkIDInputValidator(networkId);
            }

            // Validating given value of subnet mask length
            subnetMaskLength = IdExtractor.ExtractSubnetMaskLength(networkId);
            subnetMask = SubnetMaskDictionary.GetSubnetMasks(subnetMaskLength);

            Console.WriteLine($"\nSubnet Mask for inputted network ID: {subnetMask} \n");

            // Logic to determine which is known to be required: Number of subnets or number of hosts per subnet
            Console.WriteLine("What is known to be required:");
            Console.WriteLine("1. Number  of required subnets \n2. Number of hosts per network \n");
            Console.WriteLine("Please enter a number, either 1 or 2 depicting your choice:");
            int input = int.Parse(Console.ReadLine());

            if(input == 1)
            {
                Console.WriteLine("Please enter how many subnets are required: ");
                requiredSubnets = int.Parse(Console.ReadLine());

                IPAddressRangeCalculator.CalculateIPRanges(networkId, requiredSubnets);
            }
            else if (input == 2)
            {
                Console.WriteLine("Please enter the number of hosts per subnet are required: ");
                requiredHostsPerSubnet = int.Parse(Console.ReadLine());

                requiredSubnets = SubnetMaskDictionary.provisionalHostsPerSubnet(requiredHostsPerSubnet);
                IPAddressRangeCalculator.CalculateIPRanges(networkId, requiredSubnets);
            }

            // Making the program recursive
            bool retryProgram;
            do
            {
                Console.WriteLine("Press 1 to Exit or 2 to retry program again");
                int userInput = int.Parse(Console.ReadLine());
                if (userInput == 1)
                {
                    Program.Main();
                    retryProgram = true;
                }
                else if (userInput == 2)
                {
                    retryProgram = false;
                }
                else
                {
                    Console.WriteLine("Error!: Invalid input. Retrying Program");
                    retryProgram = true;
                }
            }while (retryProgram);
        }
    }
}
