using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int num_berries = 0;

    [SerializeField] private Text berriesCount;
    [SerializeField] private AudioSource collectSound;

    private void OnTriggerEnter2D(Collider2D collision) 
    {   
        ItemCollected item = collision.gameObject.GetComponent<ItemCollected>();
        Animator anim = collision.gameObject.GetComponent<Animator>();
        if (collision.gameObject.CompareTag("Berry") && !item.isCollected)
        {
            collectSound.Play();
            item.isCollected = true;
            num_berries++;
            anim.SetTrigger("collected");
            berriesCount.text = "Berries: " + num_berries;
        }        
    }
}
