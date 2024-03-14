using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViewModels;
using ViewModels.Accounts;
using DataAccess.Models;
using System.Text.RegularExpressions;
using Core.Constants;
using ViewModels.Paging;
using Core.Helpers;
using DataAccess.LicensePlateContext;

namespace Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Account> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly LicensePlateDbContext _context;

        public AccountRepository(
            UserManager<Account> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            LicensePlateDbContext context
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;

        }

        public async Task<string> GetFullnameByUserIdAsync(Guid userId)
        {
            try
            {
                Account? account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == userId);

                if (account != null)
                {
                    return account.Fullname;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public async Task<AccountPagingRequest> GetAll(AccountPagingRequest request)
        {

            List<AccountVM> accounts = new List<AccountVM>();
            try
            {
                var query = from u in _context.Accounts
                            join ur in _context.UserRoles on u.Id equals ur.UserId into userRoles
                            from ur in userRoles.DefaultIfEmpty()
                            join r in _context.Roles on ur.RoleId equals r.Id into roles
                            from r in roles.DefaultIfEmpty()
                            where r.Name != "Admin"
                            select new { u, r };



                if (!String.IsNullOrEmpty(request.SearchTerm))
                {

                    string searchTerm = request.SearchTerm;

                    query = query.Where(x =>
                               x.u.Email.ToLower().Contains(searchTerm) ||
                               x.u.Fullname.ToLower().Contains(searchTerm) ||
                               x.u.IsAccountActive.ToString().Contains(searchTerm)
                           );
                }

                accounts = await query.Select(x => new AccountVM()
                {
                    Email = x.u.Email,
                    Fullname = x.u.Fullname,
                    IsAccountActive = x.u.IsAccountActive,
                    RoleName = x.r.Name,

                }).ToListAsync();

                request.TotalRecord = accounts.Count();
                //Set totoal pages for paging
                request.TotalPages = (int)Math.Ceiling(accounts.Count() / (double)request.PageSize);
                //Get Services in each pages
                accounts = accounts.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();

                accounts = accounts.OrderByDescending(a => a.StartDate).ToList();

                //Set Items in each pages
                request.Items = accounts;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot get accounts");
            }
            return request;
        }
        public async Task<AccountVM> GetAccountByEmail(string email)
        {
            try
            {
                var account = await (from u in _context.Accounts
                                     join ur in _context.UserRoles on u.Id equals ur.UserId into userRoles
                                     from ur in userRoles.DefaultIfEmpty()
                                     join r in _context.Roles on ur.RoleId equals r.Id into roles
                                     from r in roles.DefaultIfEmpty()
                                     where u.Email == email
                                     select new AccountVM
                                     {
                                         Email = u.Email,
                                         Fullname = u.Fullname,
                                         IsAccountActive = u.IsAccountActive,
                                         RoleName = r.Name,
                                     }).FirstOrDefaultAsync();

                return account;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while getting account by email: " + email);
                return null;
            }
        }
        public static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^\S+@\S+\.\S+$";

            // Kiểm tra định dạng email
            bool isMatch = Regex.IsMatch(email, pattern);

            return isMatch;
        }
        public async Task<ResponseVM> CreateAccountManualAsync(Account account, List<string> roles)
        {
            var password = PasswordHepler.GenerateAccountPassword(10);
            try
            {
                Account existing = _context.Accounts.SingleOrDefault(x => x.Email == account.Email);

                if (existing != null)
                {
                    throw new Exception("Account is already exist");
                }
                account.Password = password;
                account.IsAccountActive = true;
                // Insert the account into the database
                var result = await userManager.CreateAsync(account, password);

                // Check if the account creation was successful
                if (result.Succeeded)
                {
                    try
                    {
                        // Add the user to the specified roles from the request
                        IdentityResult idenResult = await userManager.AddToRolesAsync(account, roles);

                        if (idenResult.Succeeded)
                        {
                            return new ResponseVM() { Status = true, Message = password };
                        }
                        else
                        {
                            throw new Exception("Cannot store data to database");
                        }

                        // Return true to indicate a successful account creation
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                        // Rollback data by removing the account if there's an exception
                        _context.Accounts.Remove(account);

                        if (await _context.SaveChangesAsync() > 0)
                        {
                            Console.WriteLine("Xóa tài khoản khi có lỗi");
                        }
                        else
                        {
                            Console.WriteLine("Không xóa được tài khoản khi có lỗi");

                        }

                        // Return false due to the exception during role assignment
                        return new ResponseVM() { Status = false, Message = ex.Message };
                    }
                }
                else
                {
                    throw new Exception("Create account failed");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ResponseVM() { Status = false, Message = ex.Message };
            }
        }
        public async Task<ResponseVM> UpdateAccountManualAsync(Account request)
        {
            try
            {
                // Find the account in the database by email
                Account? account = _context.Accounts.SingleOrDefault(a => a.Email == request.Email);

                // Update account properties with values from the request
                account.Fullname = request.Fullname;
                account.IsAccountActive = request.IsAccountActive;
                //account.isAccountActive = request.IsAccountActive ?? false;

                // Debug: Retrieve the user by email 
                var updatedUser = await userManager.FindByEmailAsync(account.Email);

                // Update the account in the database
                var result = await userManager.UpdateAsync(account);

                // Check if the update was successful
                if (result.Succeeded)
                {
                    return new ResponseVM() { Status = true, Message = "Update account successful" };

                }
                else
                {
                    throw new Exception("Update account failed");
                }


            }
            catch (Exception ex)
            {
                return new ResponseVM() { Status = false, Message = ex.Message };
            }
        }
        public async Task<Guid> GetUserId(string email)
        {
            try
            {
                Account? account = await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);

                if (account != null)
                {
                    return account.Id;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Cannot get userId of " + email);
                return new Guid();
            }
        }

    }
}
