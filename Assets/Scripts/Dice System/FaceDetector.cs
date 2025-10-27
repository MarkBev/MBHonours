using UnityEngine;


public class FaceDetector : MonoBehaviour
{
    //Assign Dice - Make sure that the triggers are given the matching tag
    [SerializeField] DiceRoll dice1;
    [SerializeField] DiceRoll dice2;
    [SerializeField] DiceRoll dice3;
  
    private void OnTriggerStay(Collider other)
    {
        if (dice1 != null)
        {
            if (dice1.GetComponent<Rigidbody>().linearVelocity == Vector3.zero && other.tag == "dice1")
            {
                dice1.diceFaceNum = int.Parse(other.name);
            }
        }
        if (dice2 != null)
        {
            if (dice2.GetComponent<Rigidbody>().linearVelocity == Vector3.zero && other.tag == "dice2")
            {
                dice2.diceFaceNum = int.Parse(other.name);
            }
        }
        if (dice3 != null)
        {
            if (dice3.GetComponent<Rigidbody>().linearVelocity == Vector3.zero && other.tag == "dice3")
            {
                dice3.diceFaceNum = int.Parse(other.name);
            }
        }
    }
}
