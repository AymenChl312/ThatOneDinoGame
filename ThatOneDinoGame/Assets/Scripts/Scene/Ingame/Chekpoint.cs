using UnityEngine;

public class Chekpoint : MonoBehaviour
{
    private Transform playerSpawn;
    public Animator animator;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Confetti");
            playerSpawn.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
