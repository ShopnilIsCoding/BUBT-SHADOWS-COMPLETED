using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        GameManager.Instance.isNewGame = true;
        SceneManager.LoadScene("SampleScene"); 
    }

    public void OnLoadGameClicked()
    {
        GameManager.Instance.isNewGame = false;
        SceneManager.LoadScene("SampleScene"); 
    }
}
