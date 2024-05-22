using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider hit)
    {
        if(hit.CompareTag("Zombie"))
        {
            Zombie zombie = hit.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
}
