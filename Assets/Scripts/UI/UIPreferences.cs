using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPreferences : MonoBehaviour
{
    public static UIPreferences singleton;

    [Header("Font options")]
    [SerializeField] public TMP_FontAsset currentFontAsset;
    [SerializeField] public TMP_Dropdown fontDropdown;
    [SerializeField] public TMP_FontAsset[] fonts;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("There is no GameManager");
        }

        if (!singleton) singleton = this;

        //Clear the dropdown just in case
        fontDropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < fonts.Length; i++)
        {

            string fontName = fonts[i].name;

            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(fontName);
            options.Add(option);
        }
        fontDropdown.AddOptions(options);
    }

    public void UpdateUIFont(int index)
    {
        //Just to be safe the index specified is within range
        if (index < fonts.Length && index >= 0)
        {
            currentFontAsset = fonts[index];
            gameManager.currentFontAsset = currentFontAsset;
        }
        else
        {
            return;
        }

        //Get and force every UIFontUpdater to update
        UIFontUpdater[] scriptArray = FindObjectsOfType(typeof(UIFontUpdater)) as UIFontUpdater[];
        foreach (UIFontUpdater uiUpdater in scriptArray)
            uiUpdater.OnEnable();
    }
}
