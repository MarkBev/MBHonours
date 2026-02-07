using JetBrains.Annotations;
using System.Collections;
using TMPro;
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



    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
        cutsceneScript.PlayVideo(introclip);
        characterSelect.StartCharacterSelect();

    }
    
}
