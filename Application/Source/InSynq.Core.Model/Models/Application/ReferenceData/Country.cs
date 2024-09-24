namespace InSynq.Core.Model.Models.Application.ReferenceData;

public class Country : BaseDomain<int>, IConfigurableEntity
{
    public string Name { get; set; }

    public string Iso2Code { get; set; }

    public string Iso3Code { get; set; }

    public string DialCode { get; set; }

    public string FlagUrl { get; set; }

    public void Configure(ModelBuilder builder)
    {
        builder.Entity<Country>(_ =>
        {
            _.Property(_ => _.Name).HasMaxLength(100).IsRequired();
            _.Property(_ => _.Iso2Code).HasMaxLength(2).IsRequired();
            _.Property(_ => _.Iso3Code).HasMaxLength(3).IsRequired();
            _.Property(_ => _.DialCode).HasMaxLength(10).IsRequired();
            _.Property(_ => _.FlagUrl).HasMaxLength(100).IsRequired();
        });
    }
}