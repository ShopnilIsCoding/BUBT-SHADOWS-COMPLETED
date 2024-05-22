using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Inventory2 : MonoBehaviour
{
    private bool isOpen;
    public Animator animator;
    private bool canToggle = true;
    public KeyCode inventoryKeyCode;
    public int soul;
    public Text soulCount;
    public playerController playerController;
    public BatteryManager batteryManager;

    // Ammo related
    public SimpleShootingGun shootingGun; // Reference to the SimpleShootingGun script
    public int ammoToAdd = 10; // Amount of ammo to add when pressing the key

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKeyCode) && canToggle)
        {
            if (!isOpen)
            {
                isOpen = true;
                animator.SetTrigger("open");
            }
            else
            {
                isOpen = false;
                animator.SetTrigger("close");
            }
            StartCoroutine(ToggleDelay());
        }

        soulCount.text = "" + soul;

        // Check for adding ammo when inventory is open and the key '2' is pressed
        if (isOpen && Input.GetKeyDown(KeyCode.Alpha2) && soul > 0)
        {
            AddAmmo();
            soul--;
        }
        if (isOpen && Input.GetKeyDown(KeyCode.Alpha1) && soul >= 3)
        {
            playerController.AddHealth(50);
            soul = soul - 3;
        }
        if (isOpen && Input.GetKeyDown(KeyCode.Alpha3) && soul >= 1)
        {
            batteryManager.batteryCount++;
            soul--;
        }
    }

    IEnumerator ToggleDelay()
    {
        canToggle = false;
        yield return new WaitForSeconds(0.5f);
        canToggle = true;
    }

    void AddAmmo()
    {
        shootingGun.storedAmmo += ammoToAdd;
    }
}
