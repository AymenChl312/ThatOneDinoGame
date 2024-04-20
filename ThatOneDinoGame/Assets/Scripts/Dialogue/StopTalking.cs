
using UnityEngine;

public class StopTalking : MonoBehaviour
{
    
    public void stopTalking()
    {
        DialogueManager.instance.isTalking=false;
    }
}
