using Kutuphane.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DataAccess.Concrete.EntityFramework.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(@"Users", @"dbo");
            builder.HasKey(x => x.UserID);

            //Relationships
            //builder.HasOne(x => x.Banner)
            //    .WithMany(x => x.BannerTranslations)
            //    .HasForeignKey(x => x.BannerId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
