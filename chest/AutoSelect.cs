using UnityEngine;
using UnityEngine.UI;

public class AutoSelectInputField : MonoBehaviour
{
    public InputField myInputField;

    void Start()
    {
        // Automatically select the input field and activate it
        if (myInputField != null)
        {
            myInputField.ActivateInputField();
        }
    }
}
