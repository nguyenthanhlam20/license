namespace DataAccess.Models
{
    public class LicensePlate
    {
        public int LicensePlateId { get; set; }
        public string LicensePlateNumber { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public Guid Id { get; set; }
        public int DistrictId { get; set; }

        #region Relationship
        public virtual Account Account { get; set; }
        public virtual District District { get; set; }
        #endregion
    }
}
