
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderController : MonoBehaviour
{
    public Slider healthSlider; // Reference to the health slider UI
    public playerController target; // Reference to the target with health
    public Text healthText;
    public Text healthimage;
    public Image fill;

    private void Update()
    {
        if (target != null)
        {
            // Update the slider's value based on the target's health
            healthSlider.value = target.health / 100;
            healthText.text = target.health.ToString("F0");
            if (healthSlider.value >= 0.67)
            {
                healthimage.color = Color.green;
                fill.color = Color.green;
                

            }
            else if (healthSlider.value >= 0.33 && healthSlider.value < 0.67)
            {
                healthimage.color = Color.yellow;
                fill.color = Color.yellow;
               
            }
            else if (healthSlider.value >= 0 && healthSlider.value < 0.33)
            {
                healthimage.color = Color.red;
                fill.color = Color.red;
                
            }
        }
    }
}
