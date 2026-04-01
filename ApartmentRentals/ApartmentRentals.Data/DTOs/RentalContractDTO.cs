namespace ApartmentRentals.Data.DTOs
{
    public class RentalContractDTO
    {
        public string Id { get; set; }

        public string SpaceId { get; set; } = "0";
        public string TenantId { get; set; } = "0";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        //public decimal FinalPrice { get; set; } //Итоговая цена
    }
}
