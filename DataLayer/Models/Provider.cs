namespace DataLayer.Models
{
    public class Provider
    {
        private int providerId;

        public int ProviderId
        {
            get { return providerId; }
            set { providerId = value; }
        }

        private byte[] avatar;

        public byte[] Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double pricePerHour;

        public double PricePerHour
        {
            get { return pricePerHour; }
            set { pricePerHour = value; }
        }

        private Person providerDetails;

        public Person ProviderDetails
        {
            get { return providerDetails; }
            set { providerDetails = value; }
        }

        public Provider() { }
        public Provider(int providerId, byte[] avatar, string type, double pricePerHour, Person providerDetails)
        {
            ProviderId = providerId;
            Avatar = avatar;
            Type = type;
            PricePerHour = pricePerHour;
            ProviderDetails = providerDetails;
        }
    }
}
