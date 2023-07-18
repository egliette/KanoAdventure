using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Behaviour[] components;

    private Animator anim;

    public bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PowerBall") && !isDead)
        {   
            isDead = true;
            collision.gameObject.GetComponent<Projectile>().Explode();
            anim.SetTrigger("death");
            foreach (Behaviour component in components)
                component.enabled = false;
            Destroy(gameObject, .5f);
        }
    }
}
