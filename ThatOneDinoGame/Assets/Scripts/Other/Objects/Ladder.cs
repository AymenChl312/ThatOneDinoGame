using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public BoxCollider2D ladderCollider;


    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isClimbing = true;
            ladderCollider.isTrigger = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.isClimbing = false;
            ladderCollider.isTrigger = false;
        }
    }

}
