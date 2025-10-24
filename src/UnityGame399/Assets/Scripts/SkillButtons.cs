using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using PlayerStuff;
using UnityEngine.UI;

public class SkillButtons : MonoBehaviour
{
    public Button[] skillButton;
    private Queue<SkillTypes> skillQueue = new Queue<SkillTypes>();
    public Stats Stats;
    private bool newLoad = true;

    public void OnOpen()
    {
        Logger.Instance.Info("ONOPEN " + Stats.SkillOne + " " + newLoad + " QUEUE COUNT " + skillQueue.Count);
        if (newLoad)
        {
            Logger.Instance.Info("TRIGGERED");
            
            if (Stats.SkillOne != SkillTypes.None)
                skillQueue.Enqueue(Stats.SkillOne);
            if (Stats.SkillTwo != SkillTypes.None)
                skillQueue.Enqueue(Stats.SkillTwo);
        }
        newLoad = false;
        ColorBlock buttonColors;
        if (Stats.SkillTwo != SkillTypes.None)
        {
            buttonColors = skillButton[(int)Stats.SkillTwo].colors;
            buttonColors.disabledColor = Color.green;
            skillButton[(int)Stats.SkillTwo].colors = buttonColors;
            skillButton[(int)Stats.SkillTwo].interactable = false;
        }
        if (Stats.SkillOne != SkillTypes.None)
        {
            buttonColors = skillButton[(int)Stats.SkillOne].colors;
            buttonColors.disabledColor = Color.green;
            skillButton[(int)Stats.SkillOne].colors = buttonColors;
            skillButton[(int)Stats.SkillOne].interactable = false;
        }
    }

    public void OnButtonClick()
    {
        ColorBlock buttonColors;
        Logger.Instance.Info("QUEUE " + skillQueue.Count);
        
        SkillTypes pressedButton;
        pressedButton = (SkillTypes)System.Enum.Parse(typeof(SkillTypes), UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        //Stats.CurrentSkills.Enqueue(pressedButton);
        skillQueue.Enqueue(pressedButton);
        skillButton[(int)pressedButton].interactable = false;
        
        buttonColors = skillButton[(int)pressedButton].colors;
        buttonColors.disabledColor = Color.green;
        skillButton[(int)pressedButton].colors = buttonColors;
        
        if (skillQueue.Count > 2)
        {
            SkillTypes popped = skillQueue.Dequeue();
            skillButton[(int)popped].interactable = true;
        }
        handleSkillUpdates();
        Logger.Instance.Info(Stats.SkillOne + " and " + Stats.SkillTwo);
    }

    private void handleSkillUpdates()
    {
        if (skillQueue.Count < 2)
        {
            skillQueue.Peek();
            Stats.SkillOne = skillQueue.Peek();
            return;
        }
        Stats.SkillTwo = skillQueue.ToArray()[1];
        Stats.SkillOne = skillQueue.Peek();
    }
    
}
