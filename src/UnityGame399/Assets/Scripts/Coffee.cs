using System;
public class Coffee
{
    public BeanType BeanType { get; private set; }
    public double CreamPercent { get; private set; }
    public CreamerType CreamerType { get; private set; }
    
    public Coffee(BeanType beanType, double creamPercent, CreamerType creamerType)
    {
        BeanType = beanType;
        CreamPercent = creamPercent;
        CreamerType = creamerType;
    
        Logger.Instance.Info($"Coffee Created → Bean Type: {BeanType}, Creamer: {(CreamPercent * 100):F0}%, Type: {CreamerType}");
    }

    public Coffee()
    {
        System.Random rng = new System.Random();
        
        Array beanValues = Enum.GetValues(typeof(BeanType));
        BeanType = (BeanType)beanValues.GetValue(rng.Next(beanValues.Length));

        double[] possiblePercentages = { 0, .1, .2, .3, .4, .5, .6, .7, .8, .9, 1 };
        CreamPercent = possiblePercentages[rng.Next(0, possiblePercentages.Length)];
        
        Array values = Enum.GetValues(typeof(CreamerType));
        CreamerType = (CreamerType)values.GetValue(rng.Next(values.Length));
        
        Logger.Instance.Info($"Coffee Generated → Bean Type: {BeanType}, Creamer: {CreamPercent}%, Type: {CreamerType}");
    }
}
