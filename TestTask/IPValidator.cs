using System.Net;
using System.Text.RegularExpressions;

namespace TestTask
{
    internal class IPValidator
    {
        public bool ValidateIPv4(uint ipAdress, uint ipAdressStart = uint.MinValue, uint ipAdressMask = uint.MaxValue)
        {
            return ipAdress > ipAdressStart && ipAdress < ipAdressMask;
        }

        public bool ValidateIPv4(uint ipAdress, uint ipAdressStart = uint.MinValue, string ipAdressMask = "255.255.255.255")
        {
            var ipAdressMaskUint = ConvertFromIPv4AddressToInteger(ipAdressMask);
            return ipAdress > ipAdressStart && ipAdress < ipAdressMaskUint;
        }

        public bool ValidateIPv4(uint ipAdress, string ipAdressStart = "0.0.0.0", string ipAdressMask = "255.255.255.255")
        {
            var ipAdressStartUInt = ConvertFromIPv4AddressToInteger(ipAdressStart);
            var ipAdressMaskUint = ConvertFromIPv4AddressToInteger(ipAdressMask);
            return ipAdress > ipAdressStartUInt && ipAdress < ipAdressMaskUint;
        }

        public bool ValidateIPv4(string ipAdress, string ipAdressStart = "0.0.0.0", string ipAdressMask = "255.255.255.255")
        {
            var ipAdressUInt = ConvertFromIPv4AddressToInteger(ipAdress);
            var ipAdressStartUInt = ConvertFromIPv4AddressToInteger(ipAdressStart);
            var ipAdressMaskUint = ConvertFromIPv4AddressToInteger(ipAdressMask);
            return ipAdressUInt > ipAdressStartUInt && ipAdressUInt < ipAdressMaskUint;
        }

        public bool ValidateIPv4(string ipAdress, string ipAdressStart = "0.0.0.0", uint ipAdressMask = uint.MaxValue)
        {
            var ipAdressUInt = ConvertFromIPv4AddressToInteger(ipAdress);
            var ipAdressStartUInt = ConvertFromIPv4AddressToInteger(ipAdressStart);
            return ipAdressUInt > ipAdressStartUInt && ipAdressUInt < ipAdressMask;
        }

        public bool ValidateIPv4(string ipAdress, uint ipAdressStart = uint.MinValue, uint ipAdressMask = uint.MaxValue)
        {
            var ipAdressUInt = ConvertFromIPv4AddressToInteger(ipAdress);
            return ipAdressUInt > ipAdressStart && ipAdressUInt < ipAdressMask;
        }

        public bool ValidateIPv4(string ipAdress, uint ipAdressStart = uint.MinValue, string ipAdressMask = "255.255.255.255")
        {
            var ipAdressUInt = ConvertFromIPv4AddressToInteger(ipAdress);
            var ipAdressMaskUint = ConvertFromIPv4AddressToInteger(ipAdressMask);
            return ipAdressUInt > ipAdressStart && ipAdressUInt < ipAdressMaskUint;
        }

        public bool ValidateIPv4(uint ipAdress, string ipAdressStart = "0.0.0.0", uint ipAdressMask = uint.MaxValue)
        {
            var ipAdressStartUInt = ConvertFromIPv4AddressToInteger(ipAdressStart);
            return ipAdress > ipAdressStartUInt && ipAdress < ipAdressMask;
        }

        private uint ConvertFromIPv4AddressToInteger(string ipAddress)
        {
            var regex = new Regex(@"^[\d\.]+");
            var match = regex.Match(ipAddress);
            if (!match.Success)
                throw new FormatException();

            var address = IPAddress.Parse(ipAddress);
            byte[] bytes = address.GetAddressBytes();

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToUInt32(bytes, 0);
        }

        private string ConvertFromIntegerToIPvAddress(uint ipAddress)
        {
            byte[] bytes = BitConverter.GetBytes(ipAddress);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return new IPAddress(bytes).ToString();
        }
    }
}
