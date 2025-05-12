using RestaurantSys.DTOs.Address.Request;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class AddressService : IAddress
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public AddressService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddNewAddress(AddNewAddressDTO input)
        {
            try
            {
                bool addressExists = _context.Addresses.Any(x =>
                 x.UserId == input.UserId &&
                (x.Title == input.Title || (x.Title == null && input.Title == null)) &&
                (x.BuildingName == input.BuildingName || (x.BuildingName == null && input.BuildingName == null)) &&
                (x.BuildingNumber == input.BuildingNumber || (x.BuildingNumber == null && input.BuildingNumber == null)) &&
                (x.Floor == input.Floor || (x.Floor == null && input.Floor == null)) &&
                (x.ApartmentNumber == input.ApartmentNumber || (x.ApartmentNumber == null && input.ApartmentNumber == null)) &&
                x.Latitude == input.Latitude &&
                x.Longitude == input.Longitude &&
                (x.Province == input.Province || (x.Province == null && input.Province == null)) &&
                (x.Region == input.Region || (x.Region == null && input.Region == null)));
                if (addressExists)
                {
                    throw new Exception("The Address Already Exist");
                }
                else
                {
                    var newAddress = new Address
                    {
                        UserId = input.UserId,
                        Title = input.Title,
                        BuildingName = input.BuildingName,
                        BuildingNumber = input.BuildingNumber,
                        Floor = input.Floor,
                        ApartmentNumber = input.ApartmentNumber,
                        AdditionalDetails = input.AdditionalDetails,
                        Latitude = input.Latitude,
                        Longitude = input.Longitude,
                        Province = input.Province,
                        Region = input.Region
                    };
                    await _context.AddAsync(newAddress);
                    await _context.SaveChangesAsync();
                }
                return "Address Added Successfully";



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
