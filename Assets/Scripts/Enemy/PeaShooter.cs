using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float range = 3;
    [SerializeField] private float attackCooldown = 1;
    
    [Header ("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] peas;

    [Header ("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance = 1;
    
    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private EnemyPatrol enemyPatrol;

    [Header ("Audio")]
    [SerializeField] private AudioSource shootingSound;


    private void Awake() 
    {
        anim = GetComponent<Animator>();
        shootingSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight()) 
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("shooting");
            }
        }   
    }

    private void Shooting()
    {
        cooldownTimer = 0;
        shootingSound.Play();
        peas[FindPea()].transform.position = firePoint.position;
        peas[FindPea()].GetComponent<PeaProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindPea()
    {
        for (int i = 0; i < peas.Length; i++)
        {
            if (!peas[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
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
        return hit.collider.gameObject.name == "Player";
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + 
                            transform.right * range * transform.localScale.x * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, 
                                        boxCollider.bounds.size.y, 
                                        boxCollider.bounds.size.z)); 
    }
}
