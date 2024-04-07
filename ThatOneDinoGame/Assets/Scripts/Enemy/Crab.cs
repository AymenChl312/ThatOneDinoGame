using UnityEngine;

public class CrabTest : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D collision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("PlayerOn", true);
        }
    }

    private void EndAnim()
    {
        animator.SetBool("PlayerOn", false);
        collision.enabled = false;
    }

    private void Kill()
    {
        collision.enabled = true;
    }
}
