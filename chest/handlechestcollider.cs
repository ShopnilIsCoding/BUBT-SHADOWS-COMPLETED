using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handlechestcollider : MonoBehaviour
{
    public GameObject cratePanel;
    public GameObject weaponWheel;
    void Start()
    {
        cratePanel.SetActive(false);
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            cratePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            weaponWheel.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            cratePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            weaponWheel.SetActive(true);
        }
    }
}
