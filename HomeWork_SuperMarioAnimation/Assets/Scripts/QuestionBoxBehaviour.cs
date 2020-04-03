using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxBehaviour : MonoBehaviour
{
    private bool bLoaded = true;
    
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bLoaded = false;
            Debug.Log("Star is unloaded!");
        }
    }
}
