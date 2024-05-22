using UnityEngine;
using UnityEngine.UI;

public class plugParent : MonoBehaviour
{
    public bool triggered = false;
    public Text plug;
    public void Start()
    {
        triggered = false;
        //plug.gameObject.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("stick"))
        {
            triggered=true;
           // plug.gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("stick"))
        {
            triggered = false;
           // plug.gameObject.SetActive(false);
        }
    }

    
}
