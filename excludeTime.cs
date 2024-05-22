using UnityEngine;

public class TimeScaleExcluder : MonoBehaviour
{
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
      
        if (!IsExcludedFromTimeScale())
        {
            Time.timeScale = originalTimeScale;
        }
    }

    bool IsExcludedFromTimeScale()
    {
        
        return gameObject.CompareTag("ExcludedFromTimeScale");
    }
}
