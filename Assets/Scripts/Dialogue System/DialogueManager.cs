using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SearchService;
using Gaskellgames;
using NUnit.Framework.Internal.Commands;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Panels for Character select")]
    [SerializeField] private GameObject characterSelectPanel;
    [SerializeField] private GameObject shipSelectPanel;
    [Header("UI Elements for Dialogue playback")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;
    [Header("UI Elements for Choices")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    

    [Header("Don't need to set this")]
    public TextAsset dialogueTextAsset;
    public Story currentStory;

    

    [Header("Character Attributes")]
     public float characterNumber = 1.0f;
     public string characterName = "DefaultName";
     public string characterRole = "DefaultRole";
     public string characterStyle = "DefaultStyle";
    [Header("Ship Attributes")]
     public string shipStrength1 = "DefaultStrength1";
     public string shipStrength2 = "DefaultStrength2";
     public string shipWeakeness = "DefaultShipWeakness";
    

    private bool dialogueisPlaying;
    private static DialogueManager instance;
    //tag keys
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
       
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);  
        dialogueText.text = "";

        //get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueisPlaying)
        {
            return;
        }
        // Need to add trigger for taking player input and continuing
        
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode()
    {
        currentStory = new Story(dialogueTextAsset.text);
        dialogueisPlaying = true;
        dialoguePanel.SetActive(true);
       characterSelectPanel.SetActive(false);
        //feed chosen character & ship data into debug log as a temporary check until it can be fed into the dialogue.
        Debug.Log("Character name is: "+characterName+" Character Role is "+characterRole+"Character Style is "+characterStyle+" Character number is "+characterNumber);
        Debug.Log("Ship Strength 1 is: " + shipStrength1 + " Ship Strength 2 is: " + shipStrength2 + " Ship Weakness is: " + shipWeakeness);
        ContinueStory();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //set text for current dialogue line
            dialogueText.text = currentStory.Continue();
            //display choices if any for this dialogue line
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }
    
    private void HandleTags(List<string> currentTags)
    {
        //takes the tags and splits them to identify what is to be done

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (currentTags == null)
            {
                Debug.Log("There are no tags?");
                return;
            }
            if (splitTag.Length != 2)
            {
                Debug.LogError("tags not set up right - check ink  " + splitTag.Length + "  " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //handles the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    speakerText.text = tagValue;
                    Debug.Log(tagValue);
                    break;
                case PORTRAIT_TAG:

                    break;
                case LAYOUT_TAG:

                    break;
                default:
                    Debug.LogWarning("Tag not currently being handled" + tag);
                    break;
            }
        }
    }
    private void ExitDialogueMode()
    {
        //disables the dialogue panel and clears the text
        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void DisplayChoices()
    {
        //sets the list of choices available to the current set of choices in the inky script
        List<Choice> currentChoices = currentStory.currentChoices;

        //hides the choices panel if no choice to be made
        if (currentChoices.Count == 0)
        {
            choicePanel.SetActive(false);
            Debug.Log("there are no choices for this line of dialogue");
        }
        //enables the choice panel if choice is to be made
        if (currentChoices.Count > 0)
        {
            choicePanel.SetActive(true);
        }
        //checks if there's more choices than available buttons
        if (currentChoices.Count > choices.Length)
        {
            Debug.Log("more choices than there are buttons!");
        }

        //sets button text to each choice available in the script
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //hide the choices that the UI supports but aren't needed
        for (int i = index;  i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
       // ContinueStory();
    }

    private IEnumerator SelectFirstChoice()
    {
        //clears event system selected and next frame sets it
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
}
