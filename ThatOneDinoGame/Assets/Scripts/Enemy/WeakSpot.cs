using TMPro;
using UnityEditor;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public Animator animator;
    public AudioClip sound;

    public GameObject monster;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("Death", true);
            Audio_Manager.instance.PlayClipAt(sound ,transform.position);
            monster.GetComponent<EnemyPatrol>().enabled = false;
            monster.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
