using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float range = 3;
    [SerializeField] private float attackCooldown = 2;
    
    [Header ("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance = 1;
    
    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private PlayerLife playerLife;
    private EnemyPatrol enemyPatrol;


    private void Awake() 
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight()) 
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }   

        if (enemyPatrol != null)
        {
            if (cooldownTimer < 1)
                enemyPatrol.enabled = false;
            else
                enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + 
                                             transform.right * range * transform.localScale.x * colliderDistance,
                                             new Vector3(boxCollider.bounds.size.x * range, 
                                                         boxCollider.bounds.size.y, 
                                                         boxCollider.bounds.size.z), 
                                             0, Vector2.left, 0, playerLayer);
        if (hit.collider == null)
        {
            return false;
        }

        if (hit.collider.gameObject.name == "Player")
        {
            playerLife = hit.transform.GetComponent<PlayerLife>();
        }
        
        return hit.collider.gameObject.name == "Player";
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + 
                            transform.right * range * transform.localScale.x * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, 
                                        boxCollider.bounds.size.y, 
                                        boxCollider.bounds.size.z)); 
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            if (playerLife != null)
                playerLife.Die();
        }
    }
}
