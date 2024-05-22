using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    public int bulletdamage;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<Zombie>().TakeDamage(bulletdamage);
                Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
