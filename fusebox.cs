using UnityEngine;
using UnityEngine.UI;

public class FuseBox : MonoBehaviour
{
    public GameObject[] plugs;
    public plugParent[] plugParents;
    public GameObject[] specialGameObjects;
    public Text task;
    public bool allInserted;

    private bool[] inserted;

    private void Start()
    {
        inserted = new bool[plugs.Length];

        // Set the initial state of plugs based on allInserted
        for (int i = 0; i < plugs.Length; i++)
        {
            inserted[i] = allInserted;
            plugs[i].SetActive(inserted[i]);
        }

        foreach (var obj in specialGameObjects)
        {
            obj.SetActive(allInserted);
        }

        if (allInserted)
        {
            task.text = "Find the Ray Gun";
        }
    }

    public void Update()
    {
        // This part remains the same
        for (int i = 0; i < plugs.Length; i++)
        {
            if (plugParents[i] != null)
            {
                if (plugParents[i].triggered == true && Input.GetKeyDown(KeyCode.P))
                {
                    inserted[i] = !inserted[i];
                    plugs[i].SetActive(inserted[i]);
                }
            }
        }

        // Check if all plugs are inserted
        allInserted = true;
        foreach (var status in inserted)
        {
            if (!status)
            {
                allInserted = false;
                break;
            }
        }

        // Activate special objects based on allInserted
        foreach (var obj in specialGameObjects)
        {
            obj.SetActive(allInserted);
        }
        if (allInserted)
        {
            task.text = "Find the Ray Gun";
        }
    }
}
