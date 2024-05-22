using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour
{
    [Header("MENUS")]
    [Tooltip("The Menu for when the MAIN menu buttons")]
    public GameObject mainMenu;
    [Tooltip("THe first list of buttons")]
    public GameObject firstMenu;
    [Tooltip("The Menu for when the PLAY button is clicked")]
    public GameObject playMenu;
    [Tooltip("The Menu for when the EXIT button is clicked")]
    public GameObject exitMenu;
    [Tooltip("Optional 4th Menu")]
    public GameObject extrasMenu;

    public enum Theme { custom1, custom2, custom3 };
    [Header("THEME SETTINGS")]
    public Theme theme;
    private int themeIndex;

    [Header("PANELS")]
    [Tooltip("The UI Panel parenting all sub menus")]
    public GameObject mainCanvas;
    [Tooltip("The UI Panel that holds the CONTROLS window tab")]
    public GameObject PanelControls;
    [Tooltip("The UI Panel that holds the VIDEO window tab")]
    public GameObject PanelVideo;
    [Tooltip("The UI Panel that holds the GAME window tab")]
    public GameObject PanelGame;
    [Tooltip("The UI Panel that holds the KEY BINDINGS window tab")]
    public GameObject PanelKeyBindings;
    [Tooltip("The UI Sub-Panel under KEY BINDINGS for MOVEMENT")]
    public GameObject PanelMovement;
    [Tooltip("The UI Sub-Panel under KEY BINDINGS for COMBAT")]
    public GameObject PanelCombat;
    [Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
    public GameObject PanelGeneral;


    // highlights in settings screen
    [Header("SETTINGS SCREEN")]
    [Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
    public GameObject lineGame;
    [Tooltip("Highlight Image for when VIDEO Tab is selected in Settings")]
    public GameObject lineVideo;
    [Tooltip("Highlight Image for when CONTROLS Tab is selected in Settings")]
    public GameObject lineControls;
    [Tooltip("Highlight Image for when KEY BINDINGS Tab is selected in Settings")]
    public GameObject lineKeyBindings;
    [Tooltip("Highlight Image for when MOVEMENT Sub-Tab is selected in KEY BINDINGS")]
    public GameObject lineMovement;
    [Tooltip("Highlight Image for when COMBAT Sub-Tab is selected in KEY BINDINGS")]
    public GameObject lineCombat;
    [Tooltip("Highlight Image for when GENERAL Sub-Tab is selected in KEY BINDINGS")]
    public GameObject lineGeneral;

    [Header("LOADING SCREEN")]
    [Tooltip("If this is true, the loaded scene won't load until receiving user input")]
    public bool waitForInput = true;
    public GameObject loadingMenu;
    [Tooltip("The loading bar Slider UI element in the Loading Screen")]
    public Slider loadingBar;
    public TMP_Text loadPromptText;
    public KeyCode userPromptKey;

    [Header("SFX")]
    [Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
    public AudioSource hoverSound;
    [Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
    public AudioSource sliderSound;
    [Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
    public AudioSource swooshSound;
    void Start()
    {
        //playMenu.SetActive(false);
        //exitMenu.SetActive(false);
        //if (extrasMenu) extrasMenu.SetActive(false);
        //firstMenu.SetActive(true);
        //mainMenu.SetActive(true);
        //mainCanvas.SetActive(true);
    }
    public void PlayCampaign()
    {
        exitMenu.SetActive(false);
        if (extrasMenu) extrasMenu.SetActive(false);
        playMenu.SetActive(true);
    }
    public void DisablePlayCampaign()
    {
        playMenu.SetActive(false);
    }
    public void AreYouSure()
    {
        exitMenu.SetActive(true);
        if (extrasMenu) extrasMenu.SetActive(false);
        DisablePlayCampaign();
    }
    public void ReturnMenu()
    {
        playMenu.SetActive(false);
        if (extrasMenu) extrasMenu.SetActive(false);
        exitMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
				Application.Quit();
#endif
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlaySFXHover()
    {
        sliderSound.Play();
    }

    public void PlaySwoosh()
    {
        swooshSound.Play();
    }
    public void LoadScene(string scene)
    {
        if (scene != "")
        {
            StartCoroutine(LoadAsynchronously(scene));
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    { // scene name is just the name of the current scene being loaded
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        mainCanvas.SetActive(false);
        loadingMenu.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .95f);
            loadingBar.value = progress;

            if (operation.progress >= 0.9f && waitForInput)
            {
                loadPromptText.text = "Press " + userPromptKey.ToString().ToUpper() + " to continue";
                loadingBar.value = 1;

                if (Input.GetKeyDown(userPromptKey))
                {
                    operation.allowSceneActivation = true;
                }
            }
            else if (operation.progress >= 0.9f && !waitForInput)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
