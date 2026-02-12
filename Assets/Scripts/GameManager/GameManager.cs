using Ink.Parsed;
using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_FontAsset currentFontAsset;
    public GameObject settingsMenu;

    [Header("Character Attributes")]
    public float characterNumber = 1.0f;
    public string characterName = "DefaultName";
    public string characterRole = "DefaultRole";
    public string characterStyle = "DefaultStyle";
    [Header("Ship Attributes")]
    public string shipStrength1 = "DefaultStrength1";
    public string shipStrength2 = "DefaultStrength2";
    public string shipWeakeness = "DefaultShipWeakness";
    [Header("Videoclips to be played")]
    [SerializeField] private VideoClip introclip;
    
    
    [SerializeField] private GameObject player;
    private Cutscene cutsceneScript;
    private DialogueManager dialogueManager;
    private CharacterSelect characterSelect;
    private bool diceRoll;
    private GameObject diceRollSystem;
    private GameObject currentScene;
    private GameObject oldScene;
    private bool gameActive = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        oldScene = null;
    }

    private void Update()
    {

        //Initiates Dice roll || STILL TO BE IMPLEMENTED IF NEEDED ||
        // diceRoll = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariablesState("diceRoll")).value;
        //if (diceRoll)
        //{

        //    DiceRoll();

        // }

        //Changes Active Background Scene
        if (gameActive)
        {
            bool newScene = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariablesState("newScene")).value;
            if (newScene)
            {
                Debug.Log("Scene Change Initiated.");
                string Scene = ((Ink.Runtime.StringValue)DialogueManager.GetInstance().GetVariablesState("scene")).value;
                currentScene.SetActive(false);
                oldScene = currentScene;
                currentScene = GameObject.Find(Scene);
                if (currentScene == null)
                {
                    Debug.LogWarning("Cannot find new Scene object");
                    currentScene = oldScene;
                }
                currentScene.SetActive(true);

            }
        }
    }


    public void openSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void closeSettings()
    {
        settingsMenu.SetActive(false);
    }
   
    public void beginGame(DialogueManager DM, GameObject VP)
    {
        dialogueManager = DM;
        player = VP;
        cutsceneScript = player.GetComponent<Cutscene>();
        if (cutsceneScript != null)
        {
            Debug.Log("cutscene script loaded."); 
        }
        characterSelect = dialogueManager.GetComponent<CharacterSelect>();
        if (characterSelect != null)
        {
            Debug.Log("characterSelect Loaded");
        }
        gameActive = true;
        cutsceneScript.PlayVideo(introclip);
        StartCoroutine(CutsceneRoutine());
        cutsceneScript.StopPlaying();
        characterSelect.StartCharacterSelect();

    }

    public void NextStory (TextAsset textAsset)
    {
        dialogueManager.dialogueTextAsset = textAsset;
        dialogueManager.EnterDialogueMode();
    }



    public void DiceRoll()
    {
        diceRollSystem = GameObject.FindWithTag("diceRollSystem");
        diceRollSystem.SetActive(true);
       
    }

    IEnumerator CutsceneRoutine()
    {

        //waits for the duration of the video, then stops the player and clears the clip.
        cutsceneScript.videoLength = (float)introclip.length;
        Debug.Log("Wait Started");
        yield return new WaitForSeconds(cutsceneScript.videoLength);
        Debug.Log("Wait Finished.");
        
    }
}
