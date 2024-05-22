using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject particleSystemPrefab; // Assign the particle system prefab in the Inspector
    private bool hasCollided = false;
    public int damage = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;

            // Deal damage to the player
            playerController target = other.GetComponent<playerController>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Instantiate particle effect
            if (particleSystemPrefab != null)
            {
                Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            }

            // Destroy the bullet after a delay to allow the particle effect to play
            Destroy(gameObject, 1f);
        }
    }
}
