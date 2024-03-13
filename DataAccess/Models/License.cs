namespace DataAccess.Models
{
    public class License
    {
        public int LicenseId { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public Guid Id { get; set; }
        public int DistrictId { get; set; }

        #region Relationship
        public virtual Account? Account { get; set; }
        public virtual District? District { get; set; }
        #endregion
    }
}
