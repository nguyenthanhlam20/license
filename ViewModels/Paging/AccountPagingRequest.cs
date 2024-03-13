using ViewModels.Accounts;

namespace ViewModels.Paging
{
    public class AccountPagingRequest : PagingRequestBase<AccountVM>
    {
        public string? RoleName { get; set; } = "";
        public bool? IsAccountActive { get; set; } = true;

    }
}