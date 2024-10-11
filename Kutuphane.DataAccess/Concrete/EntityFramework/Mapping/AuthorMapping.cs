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
    public class AuthorMapping : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable(@"Authors", @"dbo");
            builder.HasKey(x => x.AuthorID);

            //Relationships
            //builder.HasOne(x => x.Banner)
            //    .WithMany(x => x.BannerTranslations)
            //    .HasForeignKey(x => x.BannerId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
