namespace Company.Example.Infrastructure.Database.Mssql.Internal.Configurations
{
    using Company.Example.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal sealed class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(key => key.Id);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Make).IsRequired();

            builder.Property(p => p.MaxSpeed).IsRequired();

            builder.Property(p => p.Weight).IsRequired();
        }
    }
}
