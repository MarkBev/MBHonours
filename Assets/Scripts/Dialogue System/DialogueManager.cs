using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Video;



public class DialogueManager : MonoBehaviour
{
    [Header("UI Panels for Character select")]
    [SerializeField] private GameObject characterSelectPanel;
    [SerializeField] private GameObject shipSelectPanel;
    [Header("UI Elements for Dialogue playback")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private GameObject continueIcon;
    [Header("text print speed")]
    [SerializeField] private float textSpeed = 0.04f;
    [Header("UI Elements for Choices")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [Header("Portrait Elements")]
    [SerializeField] private Animator portraitAnimator;

    

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
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;
    //tag keys
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private PlayerInput playerInput;
    private InputActionAsset inputActions;
    private GameManager gameManager;
    [SerializeField] private GameObject videoPlayer;
    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        playerInput = GetComponent<PlayerInput>();

        playerInput.onActionTriggered += PlayerInput_onActionTriggered;
        gameManager = (GameManager)FindFirstObjectByType(typeof(GameManager));
    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log(context);
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
        gameManager.beginGame(dialogueManager, videoPlayer);
    }

    private void Update()
    {
        if (!dialogueisPlaying)
        {
            return;
        }
               
        
        if (currentStory.currentChoices.Count == 0 && canContinueToNextLine && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
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
            
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            //set text for current dialogue line
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            
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
                    portraitAnimator.Play(tagValue);

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
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
        }
        ContinueStory();
    }

    public void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private IEnumerator SelectFirstChoice()
    {
        //clears event system selected and next frame sets it
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    private IEnumerator DisplayLine (string line)
    {
        //clears any existing text
        // dialogueText.text = " "; Original method
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        //makes sure that lines cannot be skipped without going to the end first, and hides choices and the continue icon
        canContinueToNextLine = false;
        continueIcon.SetActive(false);
        HideChoices();

        bool isAddingRichTextTag = false;


        foreach (char letter in line.ToCharArray())
        {
             //if player presses space jumps to the end of the line - currently breaks the character typing
             //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
             //   {
             //   dialogueText.text = line;
             //   dialogueText.maxVisibleCharacters = line.Length;
             
             //   break;
             //   }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                
                if(letter == '>')
                {
                    isAddingRichTextTag = false;
                }

            }
            else
            {
                //adds each character to the displayed line
                //dialogueText.text += letter;
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        canContinueToNextLine = true;
        continueIcon.SetActive(true);
    
        //display choices if any for this dialogue line
        DisplayChoices();
    }
}
