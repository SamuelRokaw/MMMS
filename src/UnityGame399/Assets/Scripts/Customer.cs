using System;

public class Customer
{
    public string Name { get; private set; }
    private string[] Names = { "Tom", "Sammy", "Ivan", "Jenn", "Pete", "Daniel" };
    public double TipAmount { get; private set; }
    public int TrashChance { get; private set;  }
    public int TipChance { get; private set;  }
    public CustomerPersonality Personality { get; private set; }
    
    public Customer()
    {
        System.Random rng = new System.Random();
        
        double rawDouble = rng.NextDouble();
        double scaledDouble = rawDouble * 5.0;
        TipAmount = Math.Round(scaledDouble, 2);
        
        TrashChance = rng.Next(0, 3);
        TipChance = rng.Next(0, 10);
        
        Name = Names[rng.Next(Names.Length)];
        
        Array personalities = Enum.GetValues(typeof(CustomerPersonality));
        Personality = (CustomerPersonality)personalities.GetValue(rng.Next(personalities.Length));
        
        Logger.Instance.Info($"Customer Generated → Name,: {Name}, Personality: {Personality}, Tip Amount: ${TipAmount}");
    }
}
