using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_FontAsset currentFontAsset;

    [Header("Character Attributes")]
    public float characterNumber = 1.0f;
    public string characterName = "DefaultName";
    public string characterRole = "DefaultRole";
    public string characterStyle = "DefaultStyle";
    [Header("Ship Attributes")]
    public string shipStrength1 = "DefaultStrength1";
    public string shipStrength2 = "DefaultStrength2";
    public string shipWeakeness = "DefaultShipWeakness";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


   
}
