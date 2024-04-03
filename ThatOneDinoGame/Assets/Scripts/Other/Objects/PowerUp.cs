using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public BoxCollider2D bananailes;

    public bool active = false;

    public static PowerUp instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PowerUp dans la scene.");
            return;
        }
        instance = this;
    }

    private void OnTriggerEnter2D()
    {
        if (bananailes.CompareTag("Bananailes"))
        {
            active = true;
            Debug.LogWarning("true active");
            Destroy(bananailes.gameObject);
            PlayerMovement.instance.GetComponent<SpriteRenderer>().color = Color.yellow;
            PlayerMovement.instance.doubleJumpPowerUp = true;
        }
    }

    private void powerUpActive()
    {
        if (active==false)
        {
            PlayerMovement.instance.GetComponent<SpriteRenderer>().color = Color.white;
            PlayerMovement.instance.doubleJumpPowerUp = false;
            Debug.LogWarning("false active");
        }
    }
    
}
