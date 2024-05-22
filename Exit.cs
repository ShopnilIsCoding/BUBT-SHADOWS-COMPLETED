using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitConfirmationUI : MonoBehaviour
{
    public GameObject exitConfirmationPanel;

    void Start()
    {
        // Hide the exit confirmation panel at the start
        exitConfirmationPanel.SetActive(false);
        
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            exitConfirmationPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
           
        }
    }

    public void ConfirmExit()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("main menu");
        Time.timeScale = 1;
    }

    public void CancelExit()
    {
        exitConfirmationPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
       
    }
}
