using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public bool dead = false;
    public Rigidbody2D rb;

    public static PlayerEffects instance;

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerEffects dans la scene.");
            return;
        }
        instance = this;
    }
    public void TemporangeOn(int speedGiven, float speedDuration, float slowTime)
    {
        PlayerMovement.instance.moveSpeed += speedGiven;
        PlayerMovement.instance.climbSpeed += speedGiven;
        Time.timeScale = slowTime;
        DialogueManager.instance.textSpeed /= 2;
        rb.mass = 0.6f;
        rb.gravityScale = 2.5f;
        StartCoroutine(TemporangeOff(speedGiven, speedDuration));

    }

    private IEnumerator TemporangeOff(int speedGiven, float speedDuration)
    {
        yield return new WaitForSeconds(speedDuration);
        if (dead == false)
        {
            PlayerMovement.instance.moveSpeed -= speedGiven;
            PlayerMovement.instance.climbSpeed -= speedGiven;
            DialogueManager.instance.textSpeed *= 2;
        }
        Time.timeScale = 1;
        rb.mass = 1;
        rb.gravityScale = 1;
        CurrentSceneManager.instance.active = false;
        CurrentSceneManager.instance.temporange = false;
        CurrentSceneManager.instance.powerUpActive();
    }

    public void deadPlayerOff(int speedGiven)
    {
        dead = true;
        DialogueManager.instance.textSpeed *= 2;
        CurrentSceneManager.instance.active = false;
        CurrentSceneManager.instance.temporange = false;
        PlayerMovement.instance.moveSpeed -= speedGiven;
        PlayerMovement.instance.climbSpeed -= speedGiven;
        Time.timeScale = 1;
        rb.mass = 1;
        rb.gravityScale = 1;
    }
}
