using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollected : MonoBehaviour
{
    public bool isCollected = false;

    private void DestroyItem()
    {
        Destroy(gameObject, .5f);
    }
}
