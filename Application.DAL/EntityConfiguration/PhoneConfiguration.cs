using Application.Core.ProfileModule.PhoneAggregate;
using System.Data.Entity.ModelConfiguration;

namespace Application.DAL.EntityConfiguration
{
    class PhoneConfiguration : EntityTypeConfiguration<Phone>
    {
        public PhoneConfiguration()
        {
            this.HasKey(p => p.PhoneId);
            this.Property(p => p.Number).HasMaxLength(25).IsRequired();

            //configure table map
            this.ToTable("Phone");
        }
    }
}
