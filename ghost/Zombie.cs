using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    private Animator animator;
    private NavMeshAgent navAgent;
    public List<GameObject> rewardPrefabs;
    public float rewardSpacing = 0.5f;
    public Slider healthSlider;
    public Inventory inventory;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        currentHP = maxHP;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }

        if (currentHP <= 0)
        {
            animator.SetTrigger("DIE2");
            inventory.soul++;


            for (int i = 0; i < rewardPrefabs.Count; i++)
            {
                Vector3 rewardPosition = transform.position;
                rewardPosition.y = 1.5f;
                rewardPosition.x += i * rewardSpacing;
                Instantiate(rewardPrefabs[i], rewardPosition, Quaternion.identity);
            }

            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 7f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
