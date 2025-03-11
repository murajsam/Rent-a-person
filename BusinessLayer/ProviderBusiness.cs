using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class ProviderBusiness
    {
        private readonly ReservationRepository reservationRepository;
        private readonly ProviderRepository providerRepository;

        public ProviderBusiness()
        {
            this.reservationRepository = new ReservationRepository();
            this.providerRepository = new ProviderRepository();
        }

        public bool RegisterProvider(Provider provider)
        {
            return (this.providerRepository.RegisterProvider(provider));
        }

        public Provider LoginProvider(string username, string password)
        {
            List<Provider> providers = providerRepository.GetAllProviders()
            .Where(provider => provider.ProviderDetails.Username == username && provider.ProviderDetails.Password == password).ToList();
            return providers.Count > 0 ? providers[0] : null;
        }

        public bool UpdateProvider(Provider provider)
        {
            return this.providerRepository.UpdateProvider(provider);
        }

        public List<Provider> GetAllProviders()
        {
            return this.providerRepository.GetAllProviders();
        }

        public Provider GetProviderFromReservation(Reservation reservation)
        {
            List<Provider> providers = providerRepository.GetAllProviders()
             .Where(provider => provider.ProviderId == reservation.ProviderId).ToList();
            return providers.Count > 0 ? providers[0] : null;
        }
    }
}
