using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Seeding
{
    public class DistrictSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public DistrictSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            // Seed role data
            modelBuilder.Entity<District>().HasData(
      new District() { DistrictId = 1, Name = "Cao Bằng", Prefix = "11" },
      new District() { DistrictId = 2, Name = "Lạng Sơn", Prefix = "12" },
      new District() { DistrictId = 3, Name = "Quảng Ninh", Prefix = "14" },
      new District() { DistrictId = 4, Name = "Hải Phòng", Prefix = "15" },
      new District() { DistrictId = 5, Name = "Thái Bình", Prefix = "17" },
      new District() { DistrictId = 6, Name = "Nam Định", Prefix = "18" },
      new District() { DistrictId = 7, Name = "Phú Thọ", Prefix = "19" },
      new District() { DistrictId = 8, Name = "Thái Nguyên", Prefix = "20" },
      new District() { DistrictId = 9, Name = "Yên Bái", Prefix = "21" },
      new District() { DistrictId = 10, Name = "Tuyên Quang", Prefix = "22" },
      new District() { DistrictId = 11, Name = "Hà Giang", Prefix = "23" },
      new District() { DistrictId = 12, Name = "Lào Cai", Prefix = "24" },
      new District() { DistrictId = 13, Name = "Lai Châu", Prefix = "25" },
      new District() { DistrictId = 14, Name = "Sơn La", Prefix = "26" },
      new District() { DistrictId = 15, Name = "Điện Biên", Prefix = "27" },
      new District() { DistrictId = 16, Name = "Hòa Bình", Prefix = "28" },
      new District() { DistrictId = 17, Name = "Hà Nội", Prefix = "29" },
      new District() { DistrictId = 18, Name = "Hải Dương", Prefix = "34" },
      new District() { DistrictId = 19, Name = "Ninh Bình", Prefix = "35" },
      new District() { DistrictId = 20, Name = "Thanh Hóa", Prefix = "36" },
      new District() { DistrictId = 21, Name = "Nghệ An", Prefix = "37" },
      new District() { DistrictId = 22, Name = "Hà Tĩnh", Prefix = "38" },
      new District() { DistrictId = 23, Name = "TP. Đà Nẵng", Prefix = "43" },
      new District() { DistrictId = 24, Name = "Đắk Lắk", Prefix = "47" },
      new District() { DistrictId = 25, Name = "Đắk Nông", Prefix = "48" },
      new District() { DistrictId = 26, Name = "Lâm Đồng", Prefix = "49" },
      new District() { DistrictId = 27, Name = "Tp. Hồ Chí Minh", Prefix = "41" },
      new District() { DistrictId = 28, Name = "Đồng Nai", Prefix = "39, 60" },
      new District() { DistrictId = 29, Name = "Bình Dương", Prefix = "61" },
      new District() { DistrictId = 30, Name = "Long An", Prefix = "62" },
      new District() { DistrictId = 31, Name = "Tiền Giang", Prefix = "63" },
      new District() { DistrictId = 32, Name = "Vĩnh Long", Prefix = "64" },
      new District() { DistrictId = 33, Name = "Cần Thơ", Prefix = "65" },
      new District() { DistrictId = 34, Name = "Đồng Tháp", Prefix = "66" },
      new District() { DistrictId = 35, Name = "An Giang", Prefix = "67" },
      new District() { DistrictId = 36, Name = "Kiên Giang", Prefix = "68" },
      new District() { DistrictId = 37, Name = "Cà Mau", Prefix = "69" },
      new District() { DistrictId = 38, Name = "Tây Ninh", Prefix = "70" },
      new District() { DistrictId = 39, Name = "Bến Tre", Prefix = "71" },
      new District() { DistrictId = 40, Name = "Bà Rịa - Vũng Tàu", Prefix = "72" },
      new District() { DistrictId = 41, Name = "Quảng Bình", Prefix = "73" },
      new District() { DistrictId = 42, Name = "Quảng Trị", Prefix = "74" },
      new District() { DistrictId = 43, Name = "Thừa Thiên Huế", Prefix = "75" },
      new District() { DistrictId = 44, Name = "Quảng Ngãi", Prefix = "76" },
      new District() { DistrictId = 45, Name = "Bình Định", Prefix = "77" },
      new District() { DistrictId = 46, Name = "Phú Yên", Prefix = "78" },
      new District() { DistrictId = 47, Name = "Khánh Hòa", Prefix = "79" },
      new District() { DistrictId = 48, Name = "Gia Lai", Prefix = "81" },
      new District() { DistrictId = 49, Name = "Kon Tum", Prefix = "82" },
      new District() { DistrictId = 50, Name = "Sóc Trăng", Prefix = "83" },
      new District() { DistrictId = 51, Name = "Trà Vinh", Prefix = "84" },
      new District() { DistrictId = 52, Name = "Ninh Thuận", Prefix = "85" },
      new District() { DistrictId = 53, Name = "Bình Thuận", Prefix = "86" },
      new District() { DistrictId = 54, Name = "Vĩnh Phúc", Prefix = "88" },
      new District() { DistrictId = 55, Name = "Hưng Yên", Prefix = "89" },
      new District() { DistrictId = 56, Name = "Hà Nam", Prefix = "90" },
      new District() { DistrictId = 57, Name = "Quảng Nam", Prefix = "92" },
      new District() { DistrictId = 58, Name = "Bình Phước", Prefix = "93" },
      new District() { DistrictId = 59, Name = "Bạc Liêu", Prefix = "94" },
      new District() { DistrictId = 60, Name = "Hậu Giang", Prefix = "95" },
      new District() { DistrictId = 61, Name = "Bắc Cạn", Prefix = "97" },
      new District() { DistrictId = 62, Name = "Bắc Giang", Prefix = "98" },
      new District() { DistrictId = 63, Name = "Bắc Ninh", Prefix = "99" },
      new District() { DistrictId = 64, Name = "Hải Phòng", Prefix = "16" },
      new District() { DistrictId = 65, Name = "Hà Nội", Prefix = "33" },
      new District() { DistrictId = 66, Name = "Hà Nội", Prefix = "40" }

  );

        }
    }
}
