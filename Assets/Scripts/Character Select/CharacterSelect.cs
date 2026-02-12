using Ink.Parsed;
using Ink.UnityIntegration;
using JetBrains.Annotations;
using UnityEngine;
using Unity.UI;

public class CharacterSelect : MonoBehaviour
{
    [Header("DialogueManager")]
    public DialogueManager dialogueManager;
    [Header("Character Stories")]
    [SerializeField] private TextAsset chuckStory;
    [SerializeField] private TextAsset simonStory;
    [Header("ui elements")]
    [SerializeField] private GameObject characterSelect;
    [SerializeField] private GameObject shipSelect;
    [Header("Scene elements")]
    [SerializeField] private GameObject shuttle;
    [SerializeField] private GameObject bridge;

    private bool choiceMade = false;
    private bool shipChoiceMade = false;
    private GameManager gameManager;


    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("There is no GameManager");
        }
        dialogueManager = GetComponent<DialogueManager>();
        choiceMade = false;
        shipChoiceMade = false;
        characterSelect.SetActive(false);
        shipSelect.SetActive(false);
    }


    public void StartCharacterSelect()
    {
        characterSelect.SetActive(true);
    }



    public void SetCharacterChuck()
    {
        gameManager.characterName = "Chuck Cluckers";
        gameManager.characterRole = "Soldier";
        gameManager.characterStyle = "Heroic";
        gameManager.characterNumber = 5f;
        dialogueManager.dialogueTextAsset = chuckStory;
        choiceMade = true;
    }
    public void SetCharacterSimon()
    {
        gameManager.characterName = "Stellar Simon";
        gameManager.characterRole = "Explorer";
        gameManager.characterStyle = "Intrepid";
        gameManager.characterNumber = 3f;
        dialogueManager.dialogueTextAsset = simonStory;
        choiceMade = true;
    }

    public void moveToShipSelect()
    {
        if (choiceMade == true)
        {
            Debug.Log("Character name is: " + gameManager.characterName + " Character Role is " + gameManager.characterRole + "Character Style is " + gameManager.characterStyle + " Character number is " + dialogueManager.characterNumber);
            characterSelect.SetActive(false);
            shipSelect.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void SetShipSpeedy()
    {
        gameManager.shipStrength1 = "Fast";
        gameManager.shipStrength2 = "nimble";
        gameManager.shipWeakeness = "Horrible Circuit Breakers";
        shipChoiceMade = true;
    }
    public void SetShipSneaky()
    {
        gameManager.shipStrength1 = "Cloaking Device";
        gameManager.shipStrength2 = "nimble";
        gameManager.shipWeakeness = "Fuel Hog";
        shipChoiceMade = true;
        
    }
    public void SetShipPowerful()
    {
        gameManager.shipStrength1 = "Well armed";
        gameManager.shipStrength2 = "Powerful Shields";
        gameManager.shipWeakeness = "Grim reputation";
        shipChoiceMade = true;
        
    }

    public void StartStory()
    {
        if (shipChoiceMade == true)
        {
            shipSelect.SetActive(false);
            dialogueManager.EnterDialogueMode();
            //shuttle.SetActive(false);
            //bridge.SetActive(true);
        }
        else
        {
            return;
        }
    }
    public void ReturntoCharacter()
    {
        shipChoiceMade = false;
        choiceMade = false;
        shipSelect.SetActive(false);
        characterSelect.SetActive(true);
    }

}
