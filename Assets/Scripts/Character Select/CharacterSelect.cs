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

    private bool choiceMade = false;
    private bool shipChoiceMade = false;

    private void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
        choiceMade = false;
        shipChoiceMade = false;
        characterSelect.SetActive(true);
        shipSelect.SetActive(false);
    }
    public void SetCharacterChuck()
    {
        dialogueManager.characterName = "Chuck Cluckers";
        dialogueManager.characterRole = "Soldier";
        dialogueManager.characterStyle = "Heroic";
        dialogueManager.characterNumber = 5f;
        dialogueManager.dialogueTextAsset = chuckStory;
        choiceMade = true;
    }
    public void SetCharacterSimon()
    {
        dialogueManager.characterName = "Stellar Simon";
        dialogueManager.characterRole = "Explorer";
        dialogueManager.characterStyle = "Intrepid";
        dialogueManager.characterNumber = 3f;
        dialogueManager.dialogueTextAsset = simonStory;
        choiceMade = true;
    }

    public void moveToShipSelect()
    {
        if (choiceMade == true)
        {
            Debug.Log("Character name is: " + dialogueManager.characterName + " Character Role is " + dialogueManager.characterRole + "Character Style is " + dialogueManager.characterStyle + " Character number is " + dialogueManager.characterNumber);
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
        dialogueManager.shipStrength1 = "Fast";
        dialogueManager.shipStrength2 = "nimble";
        dialogueManager.shipWeakeness = "Horrible Circuit Breakers";
        shipChoiceMade = true;
    }
    public void SetShipSneaky()
    {
        dialogueManager.shipStrength1 = "Cloaking Device";
        dialogueManager.shipStrength2 = "nimble";
        dialogueManager.shipWeakeness = "Fuel Hog";
        shipChoiceMade = true;
        
    }
    public void SetShipPowerful()
    {
        dialogueManager.shipStrength1 = "Well armed";
        dialogueManager.shipStrength2 = "Powerful Shields";
        dialogueManager.shipWeakeness = "Grim reputation";
        shipChoiceMade = true;
        
    }

    public void StartStory()
    {
        if (shipChoiceMade == true)
        {
            shipSelect.SetActive(false);
            dialogueManager.EnterDialogueMode();
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
