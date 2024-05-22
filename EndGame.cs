using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public GameObject endGamePanel;  
    public GameObject zombieObject;  

    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);  
        }

        if (zombieObject != null)
        {
            capsuleCollider = zombieObject.GetComponent<CapsuleCollider>();
        }
    }

    private void Update()
    {
        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if (capsuleCollider != null && !capsuleCollider.enabled)
        {
           
            if (endGamePanel != null)
            {
                endGamePanel.SetActive(true);
                Time.timeScale = 0f;
                if(Input.GetKeyDown(KeyCode.M))
                {
                    ConfirmExit();
                }
                
                
            }
        }
    }
    public void ConfirmExit()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("main menu");
        Time.timeScale = 1;
    }
}
