using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public bool dead = false;

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
        Time.timeScale = slowTime;
        DialogueManager.instance.textSpeed /= 2;
        StartCoroutine(TemporangeOff(speedGiven, speedDuration));

    }

    private IEnumerator TemporangeOff(int speedGiven, float speedDuration)
    {
        yield return new WaitForSeconds(speedDuration);
        if (dead == false)
        {
            PlayerMovement.instance.moveSpeed -= speedGiven;
            DialogueManager.instance.textSpeed *= 2;
        }
        Time.timeScale = 1;
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
        Time.timeScale = 1;
    }
}
