using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    
    public int damageOnCollision;
    private Transform PlayerSpawn;
    private Animator fadeSystem;
    public static DeathZone instance;

    private void Awake()
    {
        PlayerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.CompareTag("Player") && PlayerHealth.instance.currentHealth>0)
        {
            StartCoroutine(ReplacePlayer(collision));
        }
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }

    public IEnumerator ReplacePlayer(Collider2D collision)
    {
        yield return new WaitForSeconds(1f);
        collision.transform.position = PlayerSpawn.position;
    }

}
