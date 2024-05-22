using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timelinechecker : MonoBehaviour
{
    public scrollwheel wheel;
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wheel.isSwordActive)
        {
            Destroy(gameObject);
            Destroy(player);
        }
    }
}
