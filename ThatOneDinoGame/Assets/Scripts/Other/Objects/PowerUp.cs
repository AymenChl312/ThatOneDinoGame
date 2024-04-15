using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public BoxCollider2D bananailesBox;
    public SpriteRenderer bananailesSprite;
    public AudioClip sound;

    

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
        if (bananailesBox.CompareTag("Bananailes"))
        {
            CurrentSceneManager.instance.active = true;
            Audio_Manager.instance.PlayClipAt(sound, transform.position);
            Destroy(bananailesBox.gameObject);
            PlayerMovement.instance.GetComponent<SpriteRenderer>().color = Color.yellow;
            PlayerMovement.instance.doubleJumpPowerUp = true;
        }
    }

    
    
}
