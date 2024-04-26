using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public BoxCollider2D ladderCollider;
    public Rigidbody2D rb;


    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.gravityScale =0;
            playerMovement.isClimbing = true;
            ladderCollider.isTrigger = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(CurrentSceneManager.instance.temporange == true)
            {
                rb.gravityScale = 2.5f;
            }
            else
            {
                rb.gravityScale = 1f;
            }
            playerMovement.isClimbing = false;
            ladderCollider.isTrigger = false;
        }
    }

}
