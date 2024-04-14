using UnityEngine;

public class StopCam : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<CameraFollow>().enabled = false;
        }
    }
}
