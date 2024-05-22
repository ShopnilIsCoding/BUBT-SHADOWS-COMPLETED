using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeAndLift : MonoBehaviour
{
    public GameObject lift;
    public GameObject movingIndicator; // GameObject to indicate lift is moving
    public float ground = 0f;
    public float f1 = 4f;
    public float f2 = 8f;
    public float f3 = 12f; // Add the height for floor 3
    public float f4 = 16f; // Add the height for floor 4

    public float moveSpeed = 1.0f;
    public Button groundbtn;
    public Button f1btn;
    public Button f2btn;
    public Button f3btn; // Add button for floor 3
    public Button f4btn; // Add button for floor 4
    public Animator liftAnimator;

    private bool liftMoving = false;

    public void groundFloor()
    {
        if (!liftMoving)
        {
            StartCoroutine(MoveToPosition(ground));
            SetButtonColors(groundbtn);
            DisableAllButtons();
            liftMoving = true;

            liftAnimator.enabled = false;
            SetMovingIndicator(true);
        }
    }

    public void Floor1()
    {
        if (!liftMoving)
        {
            StartCoroutine(MoveToPosition(f1));
            SetButtonColors(f1btn);
            DisableAllButtons();
            liftMoving = true;

            liftAnimator.enabled = false;
            SetMovingIndicator(true);
        }
    }

    public void Floor2()
    {
        if (!liftMoving)
        {
            StartCoroutine(MoveToPosition(f2));
            SetButtonColors(f2btn);
            DisableAllButtons();
            liftMoving = true;

            liftAnimator.enabled = false;
            SetMovingIndicator(true);
        }
    }

    public void Floor3() // Add method for floor 3
    {
        if (!liftMoving)
        {
            StartCoroutine(MoveToPosition(f3));
            SetButtonColors(f3btn);
            DisableAllButtons();
            liftMoving = true;

            liftAnimator.enabled = false;
            SetMovingIndicator(true);
        }
    }

    public void Floor4() // Add method for floor 4
    {
        if (!liftMoving)
        {
            StartCoroutine(MoveToPosition(f4));
            SetButtonColors(f4btn);
            DisableAllButtons();
            liftMoving = true;

            liftAnimator.enabled = false;
            SetMovingIndicator(true);
        }
    }

    IEnumerator MoveToPosition(float targetY)
    {
        Vector3 startPosition = lift.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

        float elapsedTime = 0f;

        while (elapsedTime < moveSpeed)
        {
            lift.transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lift.transform.position = targetPosition;

        EnableAllButtons();
        liftMoving = false;

        liftAnimator.enabled = true;
        SetMovingIndicator(false);
    }

    void SetButtonColors(Button selectedButton)
    {
        groundbtn.image.color = (selectedButton == groundbtn) ? Color.green : Color.red;
        f1btn.image.color = (selectedButton == f1btn) ? Color.green : Color.red;
        f2btn.image.color = (selectedButton == f2btn) ? Color.green : Color.red;
        f3btn.image.color = (selectedButton == f3btn) ? Color.green : Color.red; // Update for f3
        f4btn.image.color = (selectedButton == f4btn) ? Color.green : Color.red; // Update for f4
    }

    void SetMovingIndicator(bool isActive)
    {
        if (movingIndicator != null)
            movingIndicator.SetActive(isActive);
    }

    void DisableAllButtons()
    {
        groundbtn.interactable = false;
        f1btn.interactable = false;
        f2btn.interactable = false;
        f3btn.interactable = false; // Disable button for floor 3
        f4btn.interactable = false; // Disable button for floor 4
    }

    void EnableAllButtons()
    {
        groundbtn.interactable = true;
        f1btn.interactable = true;
        f2btn.interactable = true;
        f3btn.interactable = true; // Enable button for floor 3
        f4btn.interactable = true; // Enable button for floor 4
    }
}
