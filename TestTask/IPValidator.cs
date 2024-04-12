using System.Net;

namespace TestTask
{
    internal class IPValidator
    {
        public bool ValidateIPv4Range(string ipAdress = "0.0.0.0", string ipAdressStart = "0.0.0.0", uint ipAdressMask = 4294967295)
        {
            var ipAdressUInt = ConvertFromIPv4AddressToInteger(ipAdress);
            var ipAdressStartUInt = ConvertFromIPv4AddressToInteger(ipAdressStart);
            return ipAdressUInt > ipAdressStartUInt && ipAdressUInt < ipAdressMask;
        }

        public uint ConvertFromIPv4AddressToInteger(string ipAddress = "0.0.0.0")
        {
            var address = IPAddress.Parse(ipAddress);
            byte[] bytes = address.GetAddressBytes();

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }

        public string ConvertFromIntegerToIPvAddress(uint ipAddress = 4294967295)
        {
            byte[] bytes = BitConverter.GetBytes(ipAddress);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return new IPAddress(bytes).ToString();
        }
    }
}
