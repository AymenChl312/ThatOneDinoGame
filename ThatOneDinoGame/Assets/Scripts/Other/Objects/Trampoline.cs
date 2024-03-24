using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Animator animator;
    public AudioClip sound;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Audio_Manager.instance.PlayClipAt(sound, transform.position);
            animator.SetTrigger("Trampoline");
        }
    }
}
