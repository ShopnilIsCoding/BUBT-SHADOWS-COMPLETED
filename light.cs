using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Light torch;
    public float mxbright;
    public float minbright;
    public float drainrate;
    public bool drain;  // Declare the 'drain' variable at the class level
    public Material button;

    private BatteryManager batteryManager;
    private ScriptDb scriptDb;

    void Start()
    {
        // Find the BatteryManager in the scene
        batteryManager = FindObjectOfType<BatteryManager>();
        if (batteryManager == null)
        {
            Debug.LogError("BatteryManager not found in the scene.");
        }

        // Find the ScriptDb in the scene
        scriptDb = FindObjectOfType<ScriptDb>();
        if (scriptDb == null)
        {
            Debug.LogError("ScriptDb not found in the scene.");
        }

        // Load torch intensity
        if (scriptDb != null)
        {
            scriptDb.LoadTorchIntensity();
        }
    }

    void Update()
    {
        // Drain the torch intensity if draining is enabled and the torch is on
        if (drain && torch.enabled)
        {
            if (torch.intensity > minbright)
            {
                torch.intensity -= Time.deltaTime * (drainrate / 1000);
            }
        }

        // Clamp the torch intensity within the specified range
        torch.intensity = Mathf.Clamp(torch.intensity, minbright, mxbright);

        if (Input.GetKeyDown(KeyCode.E))
        {
            torch.enabled = !torch.enabled;
            Color emission = torch.enabled ? Color.green : Color.red;
            button.SetColor("_EmissionColor", emission);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (batteryManager != null && batteryManager.UseBattery())
            {
                Recharge(mxbright);
            }
        }
    }

    // Called when the player collides with the battery
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            if (batteryManager != null)
            {
                batteryManager.AddBattery();
            }


            Destroy(other.gameObject);
        }
    }

    public void Recharge(float amount)
    {
        torch.intensity = amount;
    }

    // For demonstration purposes, this method can be connected to a button click event
    
}
