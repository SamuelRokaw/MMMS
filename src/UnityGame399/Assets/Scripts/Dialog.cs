using System;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    [SerializeField] private CanvasGroup dialogCanvasGroup;
    [SerializeField] private string[] dialog;
    [SerializeField] private bool[] hasDialogChoice;
    [SerializeField] private Button choice1Button;
    [SerializeField] private Button choice2Button;
    [SerializeField] private TMP_Text choice1Text;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private string[] dialogChoices;
    [SerializeField] private InputHandler iH;
    private int _index = 0;
    private int _choiceIndex = 0;
    private bool _previousChoiceState = false;

    public void DoDialog(bool topButtonPressed = true)
    {
        Debug.Log(_index);
        if (_index >= dialog.Length)
        {
            _index = 0;
            gameObject.SetActive(false);
            DoDialog(false);
            iH.inDialogue = false;
            return;
        }
        dialogText.text = dialog[_index];
        if (_previousChoiceState)
        {
            if (topButtonPressed)
            {
                dialogText.text = dialogChoices[_choiceIndex + 1];
                _choiceIndex += 2;
            }
            else
            {
                dialogText.text = dialogChoices[_choiceIndex];
                _choiceIndex += 2;
            }
        }
        
        if (hasDialogChoice[_index])
        {
            _previousChoiceState = true;
            choice1Text.text = "Yes";
            choice2Button.gameObject.SetActive(true);
        }
        else
        {
            _previousChoiceState = false;
            choice1Text.text = "Continue";
            choice2Button.gameObject.SetActive(false);
        }

    }

    private void Start()
    {
        _index = 0;
        DoDialog(false);
        _index++;
        iH.inDialogue = true;
    }

    public void OnChoice1()
    {
        DoDialog(true);
        _index++;
    }
    
    public void OnChoice2()
    {
        DoDialog(false);
        _index++;
    }



}
