using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transitions : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject gameManager;
   

    public void startGame()
    {
        SceneManager.LoadScene("Main Scene"); // Replace with character select scene
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void blackScreenFade()
    {

    }

}
