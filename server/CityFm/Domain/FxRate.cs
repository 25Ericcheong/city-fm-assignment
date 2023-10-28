namespace CityFm.Domain;

public class FxRate
{
    public string SourceCurrency { get; set; }

    public string TargetCurrency { get; set; }

    public double Rate { get; set; }
}