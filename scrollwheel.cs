using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollwheel : MonoBehaviour
{
    public int selectedweapon = 0;
    public bool isSwordActive = false;
    public bool isRayGunActive = false;
    public GameObject raygunImage;
    public Text task;

    void Start()
    {
        selectweapon();
       
    }

    void Update()
    {
        int previousselected = selectedweapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectedweapon = (selectedweapon + 1) % transform.childCount;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedweapon = (selectedweapon - 1 + transform.childCount) % transform.childCount;
        }

        if (previousselected != selectedweapon)
        {
            selectweapon();
        }
        if (isRayGunActive)
        {
            raygunImage.SetActive(true);
            task.text = "Go B1 Stairs Side To Rescue";
        }
    }

    void selectweapon()
    {
        int startSelection = selectedweapon;
        bool foundValidWeapon = false;

        while (!foundValidWeapon)
        {
            if ((selectedweapon == 1 && !isSwordActive) || (selectedweapon == 2 && !isRayGunActive))
            {
                selectedweapon = (selectedweapon + 1) % transform.childCount;
                if (selectedweapon == startSelection)
                {
                    // If we have looped all the way around and didn't find a valid weapon, stop to avoid infinite loop
                    return;
                }
            }
            else
            {
                foundValidWeapon = true;
            }
        }

        // Update the weapon activation
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == selectedweapon);
        }
    }
    public void SaveWeaponStates()
    {
        FindObjectOfType<ScriptDb>().SaveWeaponStates(isSwordActive, isRayGunActive);
    }

}
