using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;

    private bool levelCompleted = false;

    void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            finishSound.Play();
            Invoke("LevelComplete", 2f);
        }    
    }

    private void LevelComplete()
    {
        GameObject BGM = GameObject.FindGameObjectWithTag("BGM");
        Destroy(BGM);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
