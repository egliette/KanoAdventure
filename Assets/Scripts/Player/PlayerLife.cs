using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private AudioSource deathSound;

    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject[] BGMs = GameObject.FindGameObjectsWithTag("BGM");
        if (BGMs.Length >= 2)
        {
            Destroy(BGMs[1]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Pea") ||
            (collision.gameObject.CompareTag("Enemy") && 
             !collision.gameObject.GetComponent<EnemyDeath>().isDead))
        {
            Die();
        }    
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            deathSound.Play();
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
        }
    }

    private void RestartLevel() 
    {
        GameObject[] BGMs = GameObject.FindGameObjectsWithTag("BGM");
        DontDestroyOnLoad(BGMs[0]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
