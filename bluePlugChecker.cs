using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bluePlugChecker : MonoBehaviour
{
    public playerController playerController;
    public Text text;
    public FuseBox fusebox;
    public GameObject TextObject;
   
    void Start()
    {
        fusebox = GetComponent<FuseBox>();
        fusebox.enabled = false;
        TextObject.SetActive(false);
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TextObject.SetActive(true);
            if(playerController.picked)
            {
                text.text = "PRESS P TO PLUG/UNPLUG";
                fusebox.enabled = true;

            }
            else
            {
                text.text = "Collect the Blue Plug First";
                fusebox.enabled = false;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            TextObject.SetActive(false);
        }
    }
}
