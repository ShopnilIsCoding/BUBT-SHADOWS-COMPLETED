using UnityEngine;

public class lightRotation : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float smoothFactor = 5f;
    float rotationX = 0;
    float rotationY = 0f;
    public float lookup = 2f;
    public float looklow = -2f;
    public float lookleft = 10f;
    public float lookright = -10f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX += mouseY * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, looklow, lookup);

        rotationY += mouseX * rotationSpeed;
        rotationY = Mathf.Clamp(rotationY, lookleft, lookright);

        Quaternion targetRotation = Quaternion.Euler(0, rotationY, -rotationX);
        transform.GetChild(0).localRotation = Quaternion.Slerp(
            transform.GetChild(0).localRotation,
            targetRotation,
            smoothFactor * Time.deltaTime
        );
    }
}
