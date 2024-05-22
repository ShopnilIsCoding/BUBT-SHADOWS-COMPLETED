using UnityEngine;
using UnityEngine.UI;

public class FuseBox : MonoBehaviour
{
    public GameObject[] plugs; 
    public plugParent[] plugParents; 
    public GameObject[] specialGameObjects;
    public Text task;

    private bool[] inserted; 

    private void Start()
    {
        
        inserted = new bool[plugs.Length];
        for (int i = 0; i < plugs.Length; i++)
        {
            inserted[i] = false;
            plugs[i].SetActive(inserted[i]);
        }

        
        foreach (var obj in specialGameObjects)
        {
            obj.SetActive(false);
        }
    }

    public void Update()
    {
        
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

        
        bool allInserted = true;
        foreach (var status in inserted)
        {
            if (!status)
            {
                allInserted = false;
                break;
            }
        }

        
        foreach (var obj in specialGameObjects)
        {
            obj.SetActive(allInserted);
        }
        if(allInserted)
        {
            task.text = "Find the Ray Gun";
        }
    }
}
