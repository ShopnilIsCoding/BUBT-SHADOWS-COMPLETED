using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public float rotationSpeed = 3f;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    bool canjump;
    private Rigidbody rb;
    public AudioSource audioSource;
    public Animator animator;
    public float health = 100;
    public float startingHealth;
    public bool picked = false;
    public Text diedtext1;
    public Text diedtext2;
    public GameObject diePannel;
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            print("Player Dead");
        }
        else
        {
            print("Player Hit");
        }
    }
    public void AddHealth(int amount)
    {
        health += amount;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource.Pause();
        health = startingHealth;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float mouseX = Input.GetAxis("Mouse X");
            float currentMoveSpeed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && verticalInput > 0)
            {
                currentMoveSpeed *= 3;
                animator.speed = 2.0f;
                audioSource.pitch = 2.0f;
            }
            else
            {
                animator.speed = 1.0f;
                audioSource.pitch = 1.0f;
            }

            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * currentMoveSpeed * Time.deltaTime;
            transform.Translate(movement);
            transform.Rotate(Vector3.up * mouseX * rotationSpeed);

            if (movement.magnitude != 0f)
            {
                audioSource.UnPause();
                animator.SetBool("walking", true);
            }
            else
            {
                audioSource.Pause();
                animator.SetBool("walking", false);
            }

            if (Input.GetButtonDown("Jump") && canjump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        if(health<=0)
        {
            PlayerDied();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canjump = true;
        }
        else
        {
            canjump = false;
        }
    }
    public void PlayerDied()
    {
        diedtext1.text = "It Was Not Expected From You!";
        diedtext2.text = "YOU DIED";
        diePannel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.M))
        {
            ConfirmExit();
        }

    }
    public void ConfirmExit()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("main menu");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
