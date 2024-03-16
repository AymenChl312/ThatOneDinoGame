using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public int damageOnCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
