using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D coll;

    [SerializeField] private float speed = 10f;
    [SerializeField] private AudioSource explodeSound;

    private bool hit;
    private float direction;
    private float lifeTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
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

        if (lifeTime > 3)
        {
            Explode();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {     
        if (collision.gameObject.name == "Terrain")
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
