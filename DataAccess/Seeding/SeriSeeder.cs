using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seeding
{
    public class SeriSeeder
    {
        private readonly ModelBuilder modelBuilder;

        public SeriSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            // Seed role data
            modelBuilder.Entity<Seri>().HasData(
     new Seri() { SeriId = 1, Title = "A", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
     new Seri() { SeriId = 2, Title = "B", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
     new Seri() { SeriId = 3, Title = "C", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
     new Seri() { SeriId = 4, Title = "D", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
     new Seri() { SeriId = 5, Title = "E", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 6, Title = "F", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 7, Title = "G", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 8, Title = "H", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 9, Title = "I", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 10, Title = "J", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 11, Title = "K", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 12, Title = "L", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 13, Title = "M", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 14, Title = "N", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 15, Title = "O", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 16, Title = "P", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 17, Title = "Q", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 18, Title = "R", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 19, Title = "S", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 20, Title = "T", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 21, Title = "U", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 22, Title = "V", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
    new Seri() { SeriId = 23, Title = "W", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
     new Seri() { SeriId = 24, Title = "X", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
     new Seri() { SeriId = 25, Title = "Y", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" },
     new Seri() { SeriId = 26, Title = "Z", Description = "Cấp cho xe của doanh nghiệp, Ban quản lý dự án thuộc doanh nghiệp, các tổ chức xã hội, xã hội – nghề nghiệp, xe của đơn vị sự nghiệp ngoài công lập, xe của Trung tâm đào tạo sát hạch lái xe công lập, xe của cá nhân" }
 );
        }
    }
}
