using System;
public class Coffee
{
    public bool IsCaffeinated { get; private set; }
    public double CreamPercent { get; private set; }
    public CreamerType CreamerType { get; private set; }

    public Coffee()
    {
        System.Random rng = new System.Random();
        
        IsCaffeinated = rng.Next(0, 2) == 1;

        double[] possiblePercentages = { 0, .1, .2, .3, .4, .5, .6, .7, .8, .9, 1 };
        CreamPercent = possiblePercentages[rng.Next(0, possiblePercentages.Length)];
        
        Array values = Enum.GetValues(typeof(CreamerType));
        CreamerType = (CreamerType)values.GetValue(rng.Next(values.Length));
        
        Logger.Instance.Info($"Coffee Generated → Caffeinated: {IsCaffeinated}, Creamer: {CreamPercent}%, Type: {CreamerType}");
    }
}
