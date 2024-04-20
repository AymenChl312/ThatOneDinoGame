using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public int coinsPickedUpInThisSceneCount;

    public Vector3 respawnPoint;

    //PowerUp
    public bool active = false;
    public bool doubleJumpItem;

    public int levelToUnlock;

    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la scene.");
            return;
        }
        instance = this;

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void powerUpActive()
    {
        if (active==false)
        {
            PlayerMovement.instance.GetComponent<SpriteRenderer>().color = Color.white;
            PlayerMovement.instance.doubleJumpPowerUp = false;
        }
        if (doubleJumpItem == true)
        {
            active = true;
            PlayerMovement.instance.GetComponent<SpriteRenderer>().color = Color.yellow;
            PlayerMovement.instance.doubleJumpPowerUp = true;
        }
    }
}
