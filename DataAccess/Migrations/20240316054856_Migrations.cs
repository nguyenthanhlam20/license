using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccountActive = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                });

            migrationBuilder.CreateTable(
                name: "Seri",
                columns: table => new
                {
                    SeriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seri", x => x.SeriId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicensePlate",
                columns: table => new
                {
                    LicensePlateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlateNumber = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    SeriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePlate", x => x.LicensePlateId);
                    table.ForeignKey(
                        name: "FK_LicensePlate_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LicensePlate_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "DistrictId");
                    table.ForeignKey(
                        name: "FK_LicensePlate_Seri_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Seri",
                        principalColumn: "SeriId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), "0cf92d6a-955b-4ec0-9946-a9f0f4a1df0d", "Admin", "ADMIN" },
                    { new Guid("d2d63c5b-d09b-4828-8322-f18ba103fe86"), "883c353c-6647-412e-b6c8-107ecac68581", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "IsAccountActive", "JoinedDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("34dd158a-6b96-4149-a3b4-5d1b5cc374a3"), 0, "fa79770d-8ce6-4d82-a813-ea5f1c68db7e", "thanhdao@gmail.com", true, "Thành Đào", true, new DateTime(2024, 3, 16, 12, 48, 55, 998, DateTimeKind.Local).AddTicks(5290), false, null, "THANHDAO@GMAIL.COM", "THANHDAO", "thanhdao", "AQAAAAEAACcQAAAAEIGmCmDZLxa2KiJqO7FcYbsG3a1g6APItF4QlIxOWrN+lEtR3u56KYca635D6AWV1Q==", null, false, "f62d5d3f-500c-439d-9b0e-dfcab1de7a72", false, "thanhdao" },
                    { new Guid("48da158a-6b96-4149-a3b4-5d1b5cc374a3"), 0, "51adb867-a7e3-4343-8677-844b619c2be5", "thuylinh@gmail.com", true, "Thùy Linh", true, new DateTime(2024, 3, 16, 12, 48, 55, 999, DateTimeKind.Local).AddTicks(8542), false, null, "THUYLINH@GMAIL.COM", "THUYLINH", "thuylinh", "AQAAAAEAACcQAAAAEKLxd/hsb3szFdcuPitu/H6KGbr5bTmy6vkD4FF5TaoLLbo8kUC/sEE3CI8TaP8mbg==", null, false, "4e117b6b-a332-45b9-853e-e8406ec495f4", false, "thuylinh" },
                    { new Guid("b8c777a9-55b9-4b3d-860a-d7b56e4c24b7"), 0, "2284ca88-3d89-4c2e-a387-df1cf4a91629", "admin@gmail.com", true, "Admin", true, new DateTime(2024, 3, 16, 12, 48, 55, 997, DateTimeKind.Local).AddTicks(1423), false, null, "ADMIN@GMAIL.COM", "ICPDPHN", "admin", "AQAAAAEAACcQAAAAEGuPvqA1sl30Ji91oTy+7aAaquU9y7lpfzVi4wzs0AB+2TCZ3mN1DHcWZaz2JIccDg==", null, false, "f5ba40b7-a850-44ed-9e50-521500d8c60a", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "District",
                columns: new[] { "DistrictId", "Name", "Prefix" },
                values: new object[,]
                {
                    { 1, "Cao Bằng", "11" },
                    { 2, "Lạng Sơn", "12" },
                    { 3, "Quảng Ninh", "14" },
                    { 4, "Hải Phòng", "15" },
                    { 5, "Thái Bình", "17" },
                    { 6, "Nam Định", "18" },
                    { 7, "Phú Thọ", "19" },
                    { 8, "Thái Nguyên", "20" },
                    { 9, "Yên Bái", "21" },
                    { 10, "Tuyên Quang", "22" },
                    { 11, "Hà Giang", "23" },
                    { 12, "Lào Cai", "24" },
                    { 13, "Lai Châu", "25" },
                    { 14, "Sơn La", "26" },
                    { 15, "Điện Biên", "27" },
                    { 16, "Hòa Bình", "28" },
                    { 17, "Hà Nội", "29" },
                    { 18, "Hải Dương", "34" },
                    { 19, "Ninh Bình", "35" },
                    { 20, "Thanh Hóa", "36" },
                    { 21, "Nghệ An", "37" },
                    { 22, "Hà Tĩnh", "38" },
                    { 23, "TP. Đà Nẵng", "43" },
                    { 24, "Đắk Lắk", "47" },
                    { 25, "Đắk Nông", "48" },
                    { 26, "Lâm Đồng", "49" },
                    { 27, "Tp. Hồ Chí Minh", "41" },
                    { 28, "Đồng Nai", "39, 60" },
                    { 29, "Bình Dương", "61" },
                    { 30, "Long An", "62" },
                    { 31, "Tiền Giang", "63" },
                    { 32, "Vĩnh Long", "64" },
                    { 33, "Cần Thơ", "65" },
                    { 34, "Đồng Tháp", "66" },
                    { 35, "An Giang", "67" },
                    { 36, "Kiên Giang", "68" },
                    { 37, "Cà Mau", "69" }
                });

            migrationBuilder.InsertData(
                table: "District",
                columns: new[] { "DistrictId", "Name", "Prefix" },
                values: new object[,]
                {
                    { 38, "Tây Ninh", "70" },
                    { 39, "Bến Tre", "71" },
                    { 40, "Bà Rịa - Vũng Tàu", "72" },
                    { 41, "Quảng Bình", "73" },
                    { 42, "Quảng Trị", "74" },
                    { 43, "Thừa Thiên Huế", "75" },
                    { 44, "Quảng Ngãi", "76" },
                    { 45, "Bình Định", "77" },
                    { 46, "Phú Yên", "78" },
                    { 47, "Khánh Hòa", "79" },
                    { 48, "Gia Lai", "81" },
                    { 49, "Kon Tum", "82" },
                    { 50, "Sóc Trăng", "83" },
                    { 51, "Trà Vinh", "84" },
                    { 52, "Ninh Thuận", "85" },
                    { 53, "Bình Thuận", "86" },
                    { 54, "Vĩnh Phúc", "88" },
                    { 55, "Hưng Yên", "89" },
                    { 56, "Hà Nam", "90" },
                    { 57, "Quảng Nam", "92" },
                    { 58, "Bình Phước", "93" },
                    { 59, "Bạc Liêu", "94" },
                    { 60, "Hậu Giang", "95" },
                    { 61, "Bắc Cạn", "97" },
                    { 62, "Bắc Giang", "98" },
                    { 63, "Bắc Ninh", "99" },
                    { 64, "Hải Phòng", "16" },
                    { 65, "Hà Nội", "33" },
                    { 66, "Hà Nội", "40" }
                });

            migrationBuilder.InsertData(
                table: "Seri",
                columns: new[] { "SeriId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "A" },
                    { 2, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "B" },
                    { 3, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "C" },
                    { 4, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "D" },
                    { 5, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "E" },
                    { 6, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "F" },
                    { 7, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "G" },
                    { 8, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "H" },
                    { 9, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "I" },
                    { 10, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "J" },
                    { 11, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "K" },
                    { 12, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "L" },
                    { 13, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "M" }
                });

            migrationBuilder.InsertData(
                table: "Seri",
                columns: new[] { "SeriId", "Description", "Title" },
                values: new object[,]
                {
                    { 14, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "N" },
                    { 15, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "O" },
                    { 16, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "P" },
                    { 17, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "Q" },
                    { 18, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "R" },
                    { 19, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "S" },
                    { 20, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "T" },
                    { 21, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "U" },
                    { 22, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "V" },
                    { 23, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "W" },
                    { 24, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "X" },
                    { 25, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "Y" },
                    { 26, "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân", "Z" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d2d63c5b-d09b-4828-8322-f18ba103fe86"), new Guid("34dd158a-6b96-4149-a3b4-5d1b5cc374a3") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d2d63c5b-d09b-4828-8322-f18ba103fe86"), new Guid("48da158a-6b96-4149-a3b4-5d1b5cc374a3") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("b8fd818f-63f1-49ee-bec5-f7b66cafbfca"), new Guid("b8c777a9-55b9-4b3d-860a-d7b56e4c24b7") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlate_DistrictId",
                table: "LicensePlate",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlate_Id",
                table: "LicensePlate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePlate_SeriesId",
                table: "LicensePlate",
                column: "SeriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LicensePlate");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Seri");
        }
    }
}
