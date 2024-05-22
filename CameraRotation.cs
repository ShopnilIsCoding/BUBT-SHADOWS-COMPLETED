using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 3f;
    public float smoothFactor = 5f;
    float rotationx = 0;
    public float normalLookUp = 0f;
    public float normalLookLow = -20f;
    public float extendedLookUp = 45f; 
    public float extendedLookLow = -45f; 

    void Update()
    {
       
        bool isMiddleMouseButtonDown = Input.GetMouseButton(2); 

        float currentLookUp = isMiddleMouseButtonDown ? extendedLookUp : normalLookUp;
        float currentLookLow = isMiddleMouseButtonDown ? extendedLookLow : normalLookLow;


        float mouseY = Input.GetAxis("Mouse Y");

        rotationx += mouseY * rotationSpeed;
        rotationx = Mathf.Clamp(rotationx, currentLookLow, currentLookUp);

        Quaternion targetRotation = Quaternion.Euler(-rotationx, 0, 0);
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            smoothFactor * Time.deltaTime
        );

        //if (!isMiddleMouseButtonDown)
        {
           // rotationx = Mathf.MoveTowards(rotationx, 0, smoothFactor * Time.deltaTime);
        }
    }
}
