using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] healthBars;
    public PlayerController playerController;

    void Start()
    {
       
        UpdateHealthBars();
    }

    
    void Update()
    {
        
        UpdateHealthBars();
    }

    void UpdateHealthBars()
    {
        
        float normalizedHealth = Mathf.InverseLerp(playerController.minbright, playerController.mxbright, playerController.torch.intensity);//0.00-1.00 value

       
        int filledBars = Mathf.CeilToInt(normalizedHealth * (healthBars.Length - 1));//0.5*10=5bars

        
        for (int i = 0; i < healthBars.Length; i++)
        {
            healthBars[i].fillAmount = i < filledBars ? 1f : 0f;//each bar 
        }
    }
}
