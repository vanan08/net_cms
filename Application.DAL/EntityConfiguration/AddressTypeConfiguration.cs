using Application.Core.ProfileModule.AddressAggregate;
using System.Data.Entity.ModelConfiguration;

namespace Application.DAL.EntityConfiguration
{
    class AddressTypeConfiguration : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeConfiguration()
        {
            this.HasKey(at => at.AddressTypeId);
            this.Property(at => at.Name).HasMaxLength(50).IsRequired();

            //configure table map
            this.ToTable("AddressType");
        }
    }
}
