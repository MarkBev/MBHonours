using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UICharacterSelect : MonoBehaviour
{
    [Header("Profile Assets")]
    [SerializeField] private Image profilePicture;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text bio;

    [SerializeField] private Sprite chuckProfilePic;
    [SerializeField] private string chuckBio;
    [SerializeField] private Sprite simonProfilePic;
    [SerializeField] private string simonBio;


    public void chuckProfile()
    {

        profilePicture.sprite = chuckProfilePic;
        title.text = "Chuck Cluckers";
        bio.text = chuckBio;
    }

    public void simonProfile()
    {

        profilePicture.sprite = simonProfilePic;
        title.text = "StellarSimon";
        bio.text = simonBio;

    }



}
