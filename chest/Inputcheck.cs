using UnityEngine;
using UnityEngine.UI;

public class InputChecker : MonoBehaviour
{
    public InputField inputField;  // Reference to the InputField in the scene
    public GameObject crate;
    public GameObject weaponwheel;// Reference to the crate GameObject in the scene
    public scrollwheel scrollwheel;

    void Start()
    {
        
        inputField.onValueChanged.AddListener(OnInputChanged);
    }

    void OnInputChanged(string newText)
    {
       
        if(!scrollwheel.isRayGunActive)
        {
            if (newText == "SDP-2")
            {
                
                Destroy(inputField.gameObject);

               
                Animator animator = crate.GetComponent<Animator>();

                if (animator != null)
                {
                    
                    animator.SetTrigger("open");
                    weaponwheel.SetActive(true);
                    scrollwheel.isRayGunActive = true;
                    Destroy(crate, 2f);
                   
                }
                else
                {
                    Debug.LogWarning("Animator component not found on crate GameObject.");
                }
            }
        }
    }

    
    void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(OnInputChanged);
        }
    }
}
