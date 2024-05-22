using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insideOfElevator : MonoBehaviour
{
    public GameObject menu;
    public Animator weaponAnimator;
    public GameObject swordGameObject;
    public GameObject gunGameObject;
    private SwordAnimationController swordController;
    private SimpleShootingGun shootingGun;
    private void Start()
    {
        // Get references to the SwordAnimationController and SimpleShootingGun scripts
        swordController = swordGameObject.GetComponent<SwordAnimationController>();
        shootingGun = gunGameObject.GetComponent<SimpleShootingGun>();
    }

    // Update is called once per frame

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activate the UI prompt
            menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            weaponAnimator.SetTrigger("in");

            // Turn on the scripts
            swordController.enabled = false;
            shootingGun.enabled = false;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deactivate the UI prompt
            menu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            weaponAnimator.SetTrigger("out");

            // Turn off the scripts
            swordController.enabled = true;
            shootingGun.enabled = true;
        }
    }
}
