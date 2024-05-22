using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor1 : MonoBehaviour
{
    public Animator animator;
   

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger the animations
            animator.SetTrigger("open");
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger the animations
            animator.SetTrigger("close");
           
        }
    }
}
