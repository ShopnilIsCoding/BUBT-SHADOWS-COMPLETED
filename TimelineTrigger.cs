using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector; // Assign the PlayableDirector in the Inspector
    public MonoBehaviour[] scriptsToDisable; // Assign the scripts you want to disable in the Inspector
    public GameObject[] uiElementsToDisable; // Assign the UI elements you want to disable in the Inspector
    public GameObject objectToDestroy; // Assign the GameObject to destroy when the Timeline ends
    public GameObject pressXUIPrompt; // Assign the UI element that prompts the player to press "X"
    public scrollwheel scrollwheel;
   // public GameObject aquired;
    public GameObject task;

    private bool playerInTrigger = false; // Flag to check if the player is in the trigger area

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.X))
        {
            // Start the timeline and other actions
            StartTimeline();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            playerInTrigger = true;

            // Show the UI prompt
            if (pressXUIPrompt != null)
            {
                pressXUIPrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            playerInTrigger = false;

            // Hide the UI prompt
            if (pressXUIPrompt != null)
            {
                pressXUIPrompt.SetActive(false);
            }
        }
    }

    void StartTimeline()
    {
        // Hide the UI prompt
        if (pressXUIPrompt != null)
        {
            pressXUIPrompt.SetActive(false);
        }

        // Play the timeline
        if (playableDirector != null)
        {
            playableDirector.Play();
        }

        // Disable the specified scripts
        foreach (var script in scriptsToDisable)
        {
            script.enabled = false;
        }

        // Disable the specified UI elements
        foreach (var uiElement in uiElementsToDisable)
        {
            uiElement.SetActive(false);
        }

        // Subscribe to the timeline's stopped event to re-enable the components and perform other actions
        if (playableDirector != null)
        {
            playableDirector.stopped += OnPlayableDirectorStopped;
        }

        // Reset the flag
        playerInTrigger = false;
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        //pressXUIPrompt.SetActive(false);
       //aquired.SetActive(true);
        scrollwheel.isSwordActive = true;
        task.SetActive(true);
        // Re-enable the specified scripts
        foreach (var script in scriptsToDisable)
        {
            script.enabled = true;
        }

        // Re-enable the specified UI elements
        foreach (var uiElement in uiElementsToDisable)
        {
            uiElement.SetActive(true);
        }

        // Destroy the specified GameObject
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }

        // Set the Timeline GameObject to inactive
        if (playableDirector != null)
        {
            playableDirector.gameObject.SetActive(false);
        }

        // Unsubscribe from the stopped event to avoid memory leaks
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}
