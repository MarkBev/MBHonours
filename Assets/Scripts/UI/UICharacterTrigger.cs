using UnityEngine;
using UnityEngine.EventSystems;


public class UICharacterTrigger : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private UICharacterSelect characterDisplay;
    [SerializeField] private bool isChuck;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isChuck)
        {
            characterDisplay.chuckProfile();
            Debug.Log("Mouse Enter");
        }
        else
        {
            characterDisplay.simonProfile();
            Debug.Log("Mouse Enter");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
        //Change Image back to default?
    }


}
