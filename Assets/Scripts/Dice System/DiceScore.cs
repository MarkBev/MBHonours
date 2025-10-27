using UnityEngine;
using TMPro;
public class DiceScore : MonoBehaviour
{

    [SerializeField] DiceRoll dice1;
    [SerializeField] DiceRoll dice2;
    [SerializeField] DiceRoll dice3;
   
    [SerializeField] TextMeshProUGUI scoreText1;
    [SerializeField] TextMeshProUGUI scoreText2;
    [SerializeField] TextMeshProUGUI scoreText3;



    void Update()
    {
        if (dice1 != null)
        {
            if (dice1.diceFaceNum != 0)
            {
                scoreText1.text = dice1.diceFaceNum.ToString();
            }
        }
        if (dice2 != null)
        {
            if (dice2.diceFaceNum != 0)
            {
                scoreText2.text = dice2.diceFaceNum.ToString();
            }
        }
        if (dice3 != null)
        {
            if (dice3.diceFaceNum != 0)
            {
                scoreText3.text = dice3.diceFaceNum.ToString();
            }
        }
    }
}
            
