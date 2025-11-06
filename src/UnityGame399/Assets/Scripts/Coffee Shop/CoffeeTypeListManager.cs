using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CoffeeTypeListManager : MonoBehaviour
{
    [System.Serializable]
    public class CoffeeButton
    {
        public Button button;
        public Sprite normalSprite;
        public Sprite pressedSprite;
        public Image image;
    }
    
    public List<CoffeeButton> coffeeButtons = new List<CoffeeButton>();

    private int selectedIndex = -1;

    private void Start()
    {
        for (int i = 0; i < coffeeButtons.Count; i++)
        {
            CoffeeButton cb = coffeeButtons[i];
            
            cb.image = cb.button.GetComponent<Image>();
            
            cb.image.sprite = cb.normalSprite;
            
            int index = i; 
            cb.button.onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int index)
    {
        if (selectedIndex == index)
        {
            coffeeButtons[index].image.sprite = coffeeButtons[index].normalSprite;
            selectedIndex = -1;
            Debug.Log("Deselected");
            return;
        }
        
        if (selectedIndex >= 0 && selectedIndex < coffeeButtons.Count)
        {
            coffeeButtons[selectedIndex].image.sprite = coffeeButtons[selectedIndex].normalSprite;
        }
        
        selectedIndex = index;
        coffeeButtons[index].image.sprite = coffeeButtons[index].pressedSprite;
        Logger.Instance.Info($"Selected: {coffeeButtons[index].button.name}");
    }

    public int GetSelectedIndex() => selectedIndex;
    public bool HasSelection() => selectedIndex >= 0;
    
    public string GetSelectedName()
    {
        if (selectedIndex >= 0 && selectedIndex < coffeeButtons.Count)
        {
            return coffeeButtons[selectedIndex].button.name;
        }
        return "None";
    }
}