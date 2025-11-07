using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CoffeeCreamerListManager : MonoBehaviour
{
    [System.Serializable]
    public class CreamerButton
    {
        public Button button;
        public Sprite normalSprite;
        public Sprite pressedSprite;
        public Image image;
        public CreamerType creamerType;
    }
    
    public List<CreamerButton> creamerButtons = new List<CreamerButton>();

    private int selectedIndex = -1;
    private bool isLocked = false;

    private void Start()
    {
        for (int i = 0; i < creamerButtons.Count; i++)
        {
            CreamerButton cb = creamerButtons[i];
            
            cb.image = cb.button.GetComponent<Image>();
            cb.image.sprite = cb.normalSprite;
            
            int index = i;
            cb.button.onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int index)
    {
        if (isLocked)
        {
            Logger.Instance.Info("Can't change creamer while pouring!");
            return;
        }
        
        if (selectedIndex == index)
        {
            creamerButtons[index].image.sprite = creamerButtons[index].normalSprite;
            selectedIndex = -1;
            Logger.Instance.Info("Deselected creamer");
            return;
        }
        
        if (selectedIndex >= 0 && selectedIndex < creamerButtons.Count)
        {
            creamerButtons[selectedIndex].image.sprite = creamerButtons[selectedIndex].normalSprite;
        }
        
        selectedIndex = index;
        creamerButtons[index].image.sprite = creamerButtons[index].pressedSprite;
        Logger.Instance.Info($"Selected: {creamerButtons[index].creamerType}");
    }

    public int GetSelectedIndex() => selectedIndex;
    public bool HasSelection() => selectedIndex >= 0;
    
    public CreamerType GetSelectedCreamerType()
    {
        if (selectedIndex >= 0 && selectedIndex < creamerButtons.Count)
        {
            return creamerButtons[selectedIndex].creamerType;
        }
        return CreamerType.Milk;
    }

    public void LockSelection()
    {
        isLocked = true;
        Logger.Instance.Info("Creamer selection locked");
    }

    public void UnlockSelection()
    {
        isLocked = false;
        Logger.Instance.Info("Creamer selection unlocked");
    }

    public bool IsLocked() => isLocked;

    public void DeselectAll()
    {
        if (selectedIndex >= 0 && selectedIndex < creamerButtons.Count)
        {
            creamerButtons[selectedIndex].image.sprite = creamerButtons[selectedIndex].normalSprite;
        }
        selectedIndex = -1;
        isLocked = false;
    }
}