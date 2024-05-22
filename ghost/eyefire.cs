using System.Collections;
using UnityEngine;

public class irondome : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletpos;
    public Transform player;
    public float bulletspeed = 10f;
    public float fireRate = 1f;
    public float shootingradius;
    private float timer = 0;

    private void Update()
    {
        if (player != null)
        {
            float distanceToTarget = Vector3.Distance(bulletpos.position, player.position);

            if (distanceToTarget <= shootingradius)
            {
                timer += Time.deltaTime;

                if (timer >= 1 / fireRate)
                {
                    timer = 0;
                    ShootMissile();
                }
            }
        }
    }

    private void ShootMissile()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletpos.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        
        Vector3 direction = player.position - bulletpos.position;

       
        bulletRigidbody.velocity = direction.normalized * 1f; 
        StartCoroutine(IncreaseSpeedOverTime(bulletRigidbody, direction.normalized * bulletspeed, 0.5f));

        
        bullet.transform.rotation = Quaternion.LookRotation(direction);

       
        SphereCollider collider = bullet.AddComponent<SphereCollider>();
        collider.isTrigger = true;

        
        bullet.AddComponent<BulletCollision>();

       
        Destroy(bullet, 2f);
    }

    
    private IEnumerator IncreaseSpeedOverTime(Rigidbody rb, Vector3 targetSpeed, float duration)
    {
        float timer = 0f;
        Vector3 initialSpeed = rb.velocity;

        while (timer < duration)
        {
            rb.velocity = Vector3.Lerp(initialSpeed, targetSpeed, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = targetSpeed;
    }
    

}
