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
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(@"Categories", @"dbo");
            builder.HasKey(x => x.CategoryID);

            //Relationships
            //builder.HasOne(x => x.Banner)
            //    .WithMany(x => x.BannerTranslations)
            //    .HasForeignKey(x => x.BannerId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
