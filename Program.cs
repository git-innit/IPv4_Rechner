using System.Net;

namespace IPv4_Rechner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string[] clearcommands = ["c", "cls", "clear"];
            bool usermode = false;
            if (args.Length <= 0) { usermode = true; }
            else { usermode = false; }

            if (usermode)
            {
                Console.Clear();
                Console.WriteLine("Nutze c um das Konsolenfenster zu leeren.\n");
            }

            do
            {
                IPAddress ipAddress = null;
                string input = null;
                int prefix = 0;
                do
                {
                    if (usermode)
                    {
                        Console.Write("Gib die IP-Adresse in der CIDR-Notation ein (z. B. 192.168.1.0/24): ");
                        input = Console.ReadLine();
                        if (clearcommands.Contains(input.ToLower()))
                        {
                            Console.Clear();
                            Console.WriteLine("Nutze c um das Konsolenfenster zu leeren.\n");
                            continue;
                        }
                    }
                    else
                    {
                        input = args[0];
                    }
                    var parts = input.Split('/');
                    if (parts.Length != 2 || !IPAddress.TryParse(parts[0], out ipAddress) || !int.TryParse(parts[1], out prefix) || prefix > 32 || prefix < 0)
                    {
                        Console.WriteLine("Ungültige Eingabe. Bitte eine gültige IP-Adresse in der CIDR-Notation eingeben.");
                        if (usermode == false) { Environment.Exit(1); }
                        else { Console.Write("\n"); continue; }
                    }
                    break;
                }
                while (usermode);

                var subnetMaskTask = Task.Run(() => GetSubnetMask(prefix));
                var networkAddressTask = subnetMaskTask.ContinueWith(t => GetNetworkAddress(ipAddress, t.Result));
                var broadcastAddressTask = networkAddressTask.ContinueWith(t => GetBroadcastAddress(t.Result, subnetMaskTask.Result));
                var smallestHostTask = networkAddressTask.ContinueWith(t => GetSmallestHost(t.Result));
                var biggestHostTask = broadcastAddressTask.ContinueWith(t => GetBiggestHost(t.Result));
                var usableHostsTask = Task.Run(() => GetUsableHosts(prefix));
                var NetworkClassTask = Task.Run(() => GetNetworkClass(prefix));

                var subnetMask = await subnetMaskTask;
                var networkAddress = await networkAddressTask;
                var broadcastAddress = await broadcastAddressTask;
                var smallestHost = await smallestHostTask;
                var biggestHost = await biggestHostTask;
                var usableHosts = await usableHostsTask;
                var networkClass = await NetworkClassTask;

                int potenz = 32 - prefix;

                Console.WriteLine("\nIP Adresse:\t{0}", ipAddress);
                Console.WriteLine("Subnetzmaske:\t{0}", subnetMask);
                Console.WriteLine("");
                Console.WriteLine("Netzadresse:\t{0}", networkAddress);
                Console.WriteLine("Kleinster Host:\t{0}", smallestHost);
                Console.WriteLine("Grösster Host:\t{0}", biggestHost);
                Console.WriteLine("Broadcast:\t{0}", broadcastAddress);
                Console.WriteLine("");
                Console.WriteLine("Anzahl Hosts:\t2^{0}-2 = {1}", potenz, usableHosts);
                Console.WriteLine("Netzklasse:\t{0}", networkClass);
                if (usermode) { Console.Write("\n"); }
            }
            while (usermode);

                static IPAddress GetSubnetMask(int prefixLength)
            {
                uint mask = 0xffffffff << (32 - prefixLength);
                return new IPAddress(BitConverter.GetBytes(mask).Reverse().ToArray());
            }

            static IPAddress GetNetworkAddress(IPAddress ip, IPAddress mask)
            {
                byte[] ipBytes = ip.GetAddressBytes();
                byte[] maskBytes = mask.GetAddressBytes();
                byte[] networkBytes = new byte[ipBytes.Length];

                for (int i = 0; i < ipBytes.Length; i++)
                    networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);

                return new IPAddress(networkBytes);
            }

            static IPAddress GetBroadcastAddress(IPAddress network, IPAddress mask)
            {
                byte[] networkBytes = network.GetAddressBytes();
                byte[] maskBytes = mask.GetAddressBytes();
                byte[] broadcastBytes = new byte[networkBytes.Length];

                for (int i = 0; i < networkBytes.Length; i++)
                    broadcastBytes[i] = (byte)(networkBytes[i] | (maskBytes[i] ^ 0xFF));

                return new IPAddress(broadcastBytes);
            }

            static IPAddress GetSmallestHost(IPAddress networkAddress)
            {
                var addressBytes = networkAddress.GetAddressBytes();
                addressBytes[^1] += 1;
                return new IPAddress(addressBytes);
            }

            static IPAddress GetBiggestHost(IPAddress broadcastAddress)
            {
                var addressBytes = broadcastAddress.GetAddressBytes();
                addressBytes[^1] -= 1;
                return new IPAddress(addressBytes);
            }

            static int GetUsableHosts(int prefix)
            {
                int useablehosts = (int)Math.Pow(2, 32 - prefix) - 2;
                return useablehosts;
            }

            static string GetNetworkClass(int prefix)
            {
                if (prefix <= 8) return "A";
                else if (prefix <= 16) return "B";
                else if (prefix <= 24) return "C";
                else if (prefix <= 28) return "D (Multicast)";
                else return "E (Experimental)";
            }
        }
    }
}