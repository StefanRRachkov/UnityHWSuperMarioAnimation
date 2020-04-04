using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject contains;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (contains != null)
        {
            if (other.gameObject.CompareTag("Player") && !contains.GetComponent<Animator>().GetBool("onSpawn"))
            {
                Debug.Log("Player Hit the box and spawned its contains.");
            
                contains.GetComponent<Animator>().SetBool("onSpawn", true);
            }
        }
    }
}
