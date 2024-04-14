using UnityEngine;

public class GoCam : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<CameraFollow>().enabled = true;
        }
    }
}
