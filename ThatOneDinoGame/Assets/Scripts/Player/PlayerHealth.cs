using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;

    public bool isInvincible = false;
    public Animator animator;

    public float invincibilityTimeAfterHit = 1f;
    private Vector3 PlayerSpawn;
    private Animator fadeSystem;

    public float jumpDeath;
    public AudioClip hitSound;

    public static PlayerHealth instance;

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scene.");
            return;
        }
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if(isInvincible == false) 
        {
            Audio_Manager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
               
            }
            else
            {
                PlayerMovement.instance.rb.AddForce(new Vector2(0f, jumpDeath));
                StartCoroutine(HandleInvincibilityDelay());
                isInvincible = true;
                PlayerMovement.instance.enabled = false;
            }
        }
            
    }

    public void Die()
    {
        PlayerMovement.instance.rb.AddForce(new Vector2(0f, jumpDeath));
        PlayerMovement.instance.playerCollider.enabled = false;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerSpawn = CurrentSceneManager.instance.respawnPoint;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        StartCoroutine(ReplacePlayer());
        StartCoroutine(HandleInvincibilityDelay());
        isInvincible = true;
        PlayerMovement.instance.enabled = false;
        //PowerUp
        CurrentSceneManager.instance.active = false;
        CurrentSceneManager.instance.doubleJumpItem = false;
        CurrentSceneManager.instance.baielectrik = false;
        if (CurrentSceneManager.instance.temporange == true)
        {
            PlayerEffects.instance.deadPlayerOff(300);
        }  
        CurrentSceneManager.instance.powerUpActive();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
    }



    public IEnumerator HandleInvincibilityDelay()
    {
        animator.SetBool("isInvincible", true);
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        animator.SetBool("isInvincible", false);
        isInvincible = false;
        PlayerMovement.instance.enabled = true;

    }

    public IEnumerator ReplacePlayer()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        transform.position = PlayerSpawn;
        currentHealth = maxHealth;
        PlayerMovement.instance.playerCollider.enabled = true;
    }

}
