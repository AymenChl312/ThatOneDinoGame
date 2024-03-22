using UnityEngine;

public class Chekpoint : MonoBehaviour
{
    private Transform playerSpawn;
    public Animator animator;
    public AudioClip sound;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Audio_Manager.instance.PlayClipAt(sound, transform.position);
            playerSpawn.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
