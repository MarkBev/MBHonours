using UnityEngine;
using UnityEngine.UI;
using Wwise;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
        //Editor accessible values
    [Header("Sliders & Text")]
    [SerializeField, Tooltip("Drag and drop the slider for Main Volume")] private Slider mainSlider;
    [SerializeField, Tooltip("Drag and drop the text you want to show the value")] private TMP_Text mainText;
    [Space(10)]
    [SerializeField, Tooltip("Drag and drop the slider for Music Volume")] private Slider musicSlider;
    [SerializeField, Tooltip("Drag and drop the text you want to show the value")] private TMP_Text musicText;
    [Space(10)]
    [SerializeField, Tooltip("Drag and drop the slider for SFX Volume")] private Slider sfxSlider;
    [SerializeField, Tooltip("Drag and drop the text you want to show the value")] private TMP_Text sfxText;
    [Space(10)]
    [SerializeField, Tooltip("Drag and drop the slider for Shark Volume")] private Slider sharkSlider;
    [SerializeField, Tooltip("Drag and drop the text you want to show the value")] private TMP_Text sharkText;
    [Space(10)]
    [SerializeField, Tooltip("Drag and drop the slider for Bee Volume")] private Slider beeSlider;
    [SerializeField, Tooltip("Drag and drop the text you want to show the value")] private TMP_Text beeText;
    [Space(10)]
    [SerializeField, Tooltip("Drag and drop the slider for Alligator Volume")] private Slider alligatorSlider;
    [SerializeField, Tooltip("Drag and drop the text you want to show the value")] private TMP_Text alligatorText;
    [Space(10)]
    [SerializeField, Tooltip("Drag and drop the slider for Skunk Volume")] private Slider skunkSlider;
    [SerializeField, Tooltip("Drag and drop the text you want to show the value")] private TMP_Text skunkText;
    [Space(20)]
    [Header("Default Settings")]
    [Space(10)]
    [SerializeField, Range(1, 100), Tooltip("Set the default value, limited 1-100")] private float mainVolumeDefault;
    [SerializeField, Range(1, 100), Tooltip("Set the default value, limited 1-100")] private float musicVolumeDefault;
    [SerializeField, Range(1, 100), Tooltip("Set the default value, limited 1-100")] private float sfxVolumeDefault;
    [SerializeField, Range(1, 100), Tooltip("Set the default value, limited 1-100")] private float sharkVolumeDefault;
    [SerializeField, Range(1, 100), Tooltip("Set the default value, limited 1-100")] private float beeVolumeDefault;
    [SerializeField, Range(1, 100), Tooltip("Set the default value, limited 1-100")] private float alligatorVolumeDefault;
    [SerializeField, Range(1, 100), Tooltip("Set the default value, limited 1-100")] private float skunkVolumeDefault;

    //value stored in the script, to later be saved to PlayerPrefs
    private float mainValue;
    private float musicValue;
    private float sfxValue;
    private float sharkValue;
    private float beeValue;
    private float alligatorValue;
    private float skunkValue;

        
    void Awake()
    {
        // Loads the saved values when settings screen is activated
        Load();
    }

  
    void Update()
    {
        //Updates the scripts saved value to what the sliders current value is
        //Updates the corresponding RTCP value to the same value
        //Updates the sliders text to show the value rounded to the nearest 2 decimal places

        mainValue = mainSlider.value;
        AkUnitySoundEngine.SetRTPCValue("MainVolume", mainValue);
        mainText.text = mainValue.ToString("#.00");

        musicValue = musicSlider.value;
        AkUnitySoundEngine.SetRTPCValue("MusicVolume", musicValue);
        musicText.text = musicValue.ToString("#.00");

        sfxValue = sfxSlider.value;
        AkUnitySoundEngine.SetRTPCValue("SfxVolume", sfxValue);
        sfxText.text = sfxValue.ToString("#.00");

        sharkValue = sharkSlider.value;
        AkUnitySoundEngine.SetRTPCValue("SharkVolume", sharkValue);
        sharkText.text = sharkValue.ToString("#.00");

        beeValue = beeSlider.value;
        AkUnitySoundEngine.SetRTPCValue("BeeVolume", beeValue);
        beeText.text = beeValue.ToString("#.00");

        alligatorValue = alligatorSlider.value;
        AkUnitySoundEngine.SetRTPCValue("AlligatorVolume", alligatorValue);
        alligatorText.text = alligatorValue.ToString("#.00");

        skunkValue = skunkSlider.value;
        AkUnitySoundEngine.SetRTPCValue("SkunkVolume", skunkValue);
        skunkText.text = skunkValue.ToString("#.00");
    }

    void Save()
    {
        //saves the current value to playerprefs
        //attach as an onclick function to the back button
        //not sure how to implement if a keypress exits the screen but anywhere your code closes Settings - trigger this BEFORE the UI is disabled

        PlayerPrefs.SetFloat("mainValue", mainValue);
        PlayerPrefs.SetFloat("musicValue", musicValue);
        PlayerPrefs.SetFloat("sfxValue", sfxValue);
        PlayerPrefs.SetFloat("sharkValue", sharkValue);
        PlayerPrefs.SetFloat("beeValue", beeValue);
        PlayerPrefs.SetFloat("alligatorValue", alligatorValue);
        PlayerPrefs.SetFloat("skunkValue", skunkValue);
    
    }
    void Load()
    {
        //retrieves any saved values from playerprefs, or sets it to default
        //then immediately adjusts the slider position to match
        mainValue = PlayerPrefs.GetFloat("mainValue",mainVolumeDefault);
        mainSlider.value = mainValue;
        musicValue = PlayerPrefs.GetFloat("musicValue", musicVolumeDefault);
        musicSlider.value = musicValue;
        sfxValue = PlayerPrefs.GetFloat("sfxValue",sfxVolumeDefault);
        sfxSlider.value = sfxValue;
        sharkValue = PlayerPrefs.GetFloat("sharkValue",sharkVolumeDefault);
        sharkSlider.value = sharkValue;
        beeValue = PlayerPrefs.GetFloat("beeValue", beeVolumeDefault);
        beeSlider.value = beeValue;
        alligatorValue = PlayerPrefs.GetFloat("alligatorValue", alligatorVolumeDefault);
        alligatorSlider.value = alligatorValue;
        skunkValue = PlayerPrefs.GetFloat("skunkValue",skunkVolumeDefault);
        skunkSlider.value = skunkValue;
    }

    private void ResetToDefault()
    {
        //Sets Values to default
        //Updates Slider to default
        mainValue = mainVolumeDefault;
        mainSlider.value = mainValue;
        mainText.text = mainValue.ToString("#.00");

        musicValue = musicVolumeDefault;
        musicSlider.value = musicValue;
        musicText.text = musicValue.ToString("#.00");

        sfxValue = sfxVolumeDefault;
        sfxSlider.value = sfxValue;
        sfxText.text = sfxValue.ToString("#.00");

        sharkValue = sharkVolumeDefault;
        sharkSlider.value = sharkValue;
        sharkText.text = sharkValue.ToString("#.00");

        beeValue = beeVolumeDefault;
        beeSlider.value = beeValue;
        beeText.text = beeValue.ToString("#.00");

        alligatorValue = alligatorVolumeDefault;
        alligatorSlider.value = alligatorValue;
        alligatorText.text = alligatorValue.ToString("#.00");

        skunkValue = skunkVolumeDefault;
        skunkSlider.value = skunkValue;
        skunkText.text = skunkValue.ToString("#.00");
        Save();

    }

    private void Mute()
    {
        //force saves the current main volume
        //sets the script value to 0
        //updates wwise
        //sets the slider and the text to 0

        PlayerPrefs.SetFloat("mainValue", mainValue);
        mainValue = 0f;
        AkUnitySoundEngine.SetRTPCValue("MainVolume", mainValue);
        mainSlider.value = mainValue;
        mainText.text = mainValue.ToString("#.00");


    }
    private void UnMute()
    {
        //updates the script value to saved value
        //updates wwise to the value
        //sets the slider and the text to that value
        mainValue = PlayerPrefs.GetFloat("mainValue", mainVolumeDefault);
        AkUnitySoundEngine.SetRTPCValue("MainVolume", mainValue);
        mainSlider.value = mainValue;
        mainText.text = mainValue.ToString("#.00");


    }

}
