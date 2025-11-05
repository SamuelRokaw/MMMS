using UnityEngine;

public class Coffee : MonoBehaviour
{
    private bool isCaffeinated;
    private double creamPercent;
    private CreamerType creamerType;
    
    void awake()
    {
        generateOrder();
    }

    private void generateOrder()
    {
        isCaffeinated = Random.value > 0.5f;
        
        double[] possiblePercentages = { 0, .1, .2, .3, .4, .5, .6, .7, .8, .9, 1 };
        creamPercent = possiblePercentages[Random.Range(0, possiblePercentages.Length)];
        
        int creamerCount = System.Enum.GetValues(typeof(CreamerType)).Length;
        creamerType = (CreamerType)Random.Range(0, creamerCount);
        
        Debug.Log($"Coffee Generated → Caffeinated: {isCaffeinated}, Creamer: {creamPercent}%, Type: {creamerType}");
    }
}
