namespace ApartmentRentals.Main.Models
{
    public class RentalContract
    {
        public int Id { get; set; }

        public int SpaceId { get; set; } = 0;
        public int TenantId { get; set; } = 0;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public decimal FinalPrice { get; set; } //Итоговая цена
    }
}
