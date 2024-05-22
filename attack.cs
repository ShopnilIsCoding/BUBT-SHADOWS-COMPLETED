using UnityEngine;

public class SwordAnimationController : MonoBehaviour
{
    public Animator swordAnimator;
    private bool isAttacking = false;
    public AudioSource audio1;
    private float nextShotTime;
    public float shootingCooldown = 0.2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + shootingCooldown;
            // Set attack boolean parameter to true
            if (swordAnimator != null)
            {
                swordAnimator.SetBool("attack", true);
                isAttacking = true;
                audio1.Play();
            }
        }

        if (Input.GetMouseButtonUp(0) && isAttacking)
        {
            // Set attack boolean parameter to false
            if (swordAnimator != null)
            {
                swordAnimator.SetBool("attack", false);
                isAttacking = false;
            }
        }
    }
}
