using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] powerBalls;
    [SerializeField] private AudioSource spellSound;

    private float cooldownTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();  
        cooldownTimer = attackCooldown + Time.deltaTime;      
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && cooldownTimer > attackCooldown &&
            playerMovement.canAttack())
        {
            Attack();
        }

        if (cooldownTimer < attackCooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
    }

    private void Attack()
    {   
        spellSound.Play();
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        powerBalls[FindPowerBall()].transform.position = firePoint.position;
        powerBalls[FindPowerBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindPowerBall()
    {
        for (int i = 0; i < powerBalls.Length; i++)
        {
            if (!powerBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
