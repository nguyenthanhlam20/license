using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models
{
    public class Account : IdentityUser<Guid>
    {
        public string Fullname { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set;}
        public bool IsAccountActive { get; set; }
        public string Password { get; set; } = string.Empty;

        #region Relationship
        public virtual ICollection<License>? Licenses { get; set; }
        #endregion
    }
}
