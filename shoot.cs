using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleShootingGun : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float shootingCooldown = 0.2f;
    public int magazineCapacity = 5;
    public int storedAmmo = 50; // Total ammo available
    public ParticleSystem collisionParticles;
    public AudioSource audios;
    private float nextShotTime;
    public float reloadingTime = 3f;
    private bool canShoot = true;
    public TextMeshProUGUI bulletCount;
    public Image ammo;
    float lerpSpeed;
    public Transform shootingPoint;
    public int bulletDamage = 10;
    public float raycastRange = 100f;

    // Reference to the pointer (UI element)
    public Image pointer;

    void Update()
    {
        if (Time.timeScale != 0)
        {
            lerpSpeed = 3f * Time.deltaTime;
            ammo.fillAmount = Mathf.Lerp(ammo.fillAmount, magazineCapacity / 10f, lerpSpeed);

            if (Input.GetMouseButton(0) && Time.time >= nextShotTime && magazineCapacity > 0 && canShoot)
            {
                canShoot = true;
                Shoot();
                nextShotTime = Time.time + shootingCooldown;
                audios.Play();
            }

            if ((magazineCapacity <= 0 && canShoot) || (Input.GetKeyDown(KeyCode.R) && magazineCapacity < 5 && storedAmmo > 0))
            {
                canShoot = false;
                StartCoroutine(Reloading());
            }

            if (magazineCapacity <= 0 && storedAmmo <= 0)
            {
                bulletCount.text = "No ammo";
            }
            else
            {
                bulletCount.text = canShoot ? $"{magazineCapacity}/{storedAmmo}" : "Reloading..";
            }

            // Update the pointer's position and rotation every frame
            UpdatePointer();
        }
    }

    void Shoot()
    {
        // Cast a ray from the shooting point in the forward direction
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, raycastRange))
        {
            // If the ray hits an object
            if (hit.collider != null)
            {
                // Check if the object is a Zombie
                if (hit.collider.CompareTag("Zombie"))
                {
                    Zombie zombie = hit.collider.GetComponent<Zombie>();
                    if (zombie != null)
                    {
                        zombie.TakeDamage(bulletDamage);
                    }
                }

                // Instantiate particle effects at the point of impact
                if (collisionParticles != null)
                {
                    ParticleSystem newParticles = Instantiate(collisionParticles, hit.point, Quaternion.identity);
                    Destroy(newParticles.gameObject, newParticles.main.duration);
                }

                // Continue to update the pointer here if you prefer,
                // but it's also updating every frame in Update()
            }
        }

        magazineCapacity--;
    }

    void UpdatePointer()
    {
        // Cast a ray from the shooting point in the forward direction
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, raycastRange))
        {
            // Convert the world position of the hit point to a screen position
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);

            // Update the position of the pointer UI element
            pointer.transform.position = screenPosition;

            // Calculate the direction from the shooting point to the hit point
            Vector3 direction = hit.point - shootingPoint.position;

            // Calculate the angle to rotate the pointer to face the hit point
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pointer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    IEnumerator Reloading()
    {
        // If there's no ammo in the magazine and there's stored ammo available, reload automatically
        if (magazineCapacity <= 0 && storedAmmo > 0)
        {
            yield return new WaitForSeconds(reloadingTime);
            int ammoToReload = Mathf.Min(10, storedAmmo); // Reload maximum 10 bullets or less if there's less ammo available
            magazineCapacity = ammoToReload;
            storedAmmo -= ammoToReload;
        }
        else // Otherwise, wait for manual reload
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R) && magazineCapacity < 5 && storedAmmo > 0);
           int ammoToReload = Mathf.Min(5 - magazineCapacity, storedAmmo); // Reload up to 10 bullets or less if there's less ammo available
            magazineCapacity += ammoToReload;
            storedAmmo -= ammoToReload;
        }

        canShoot = true;
    }
}
