using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool baielectrik;
    private bool canDash = true;
    public bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    public static PlayerDash instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerDash dans la scene.");
            return;
        }
        instance = this;
    }
    

    void Update()
    {
        if (baielectrik == false)
        {
            return;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Q) && canDash)
            {
                StartCoroutine(Dash());
            }
        }
        
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = PlayerMovement.instance.rb.gravityScale;
        PlayerMovement.instance.rb.gravityScale = 0f;
        PlayerMovement.instance.rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        PlayerMovement.instance.rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash= true;
    }
}
