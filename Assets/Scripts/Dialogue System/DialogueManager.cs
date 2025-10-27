using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    [Header("UI Elements for Dialogue playback")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("UI Panels for Character select")]
    [SerializeField] private GameObject characterSelectPanel;

    [SerializeField] private GameObject shipSelectPanel;
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
        dialogueText.text = "";
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
        Debug.Log("Character name is: "+characterName+" Character Role is "+characterRole+"Character Style is "+characterStyle+" Character number is "+characterNumber);
        Debug.Log("Ship Strength 1 is: " + shipStrength1 + " Ship Strength 2 is: " + shipStrength2 + " Ship Weakness is: " + shipWeakeness);
        ContinueStory();
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
    
    private void ExitDialogueMode()
    {
        dialogueisPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
