namespace RestaurantSys.DTOs.Address.Request
{
    public class AddNewAddressDTO
    {
        public int? UserId { get; set; }

        public string? Title { get; set; }

        public string? BuildingName { get; set; }

        public string? BuildingNumber { get; set; }

        public string? Floor { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? AdditionalDetails { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string? Province { get; set; }

        public string? Region { get; set; }
    }
}
