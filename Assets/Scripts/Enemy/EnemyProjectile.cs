using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private float resetTime = 2;
    [SerializeField] private AudioSource explodeSound;

    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;
    private float direction;
    private float lifeTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void Explode()
    {
        explodeSound.Play();
        hit = true;
        coll.enabled = false;
        anim.SetTrigger("explode");
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;

        if (lifeTime > resetTime)
        {            
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.name == "Terrain" ||
            collision.gameObject.CompareTag("PowerBall"))
        {
            {
                Explode();
            }
        }
    }

    public void SetDirection(float _direction)
    {   
        lifeTime = 0f;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        coll.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y,
                                           transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
