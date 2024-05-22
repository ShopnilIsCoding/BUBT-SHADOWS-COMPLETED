using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class BatteryManager : MonoBehaviour
{
    public int batteryCount = 0;
    public Text batteryCountText;

    void Start()
    {
        LoadBatteryCount();
        UpdateBatteryCountText();
    }

    public void AddBattery()
    {
        batteryCount++;
        UpdateBatteryCountText();
        SaveBatteryCount();
    }

    public bool UseBattery()
    {
        if (batteryCount > 0)
        {
            batteryCount--;
            UpdateBatteryCountText();
            SaveBatteryCount();
            return true;
        }
        return false;
    }

    void UpdateBatteryCountText()
    {
        batteryCountText.text = "" + batteryCount;
    }

    void SaveBatteryCount()
    {
        FindObjectOfType<ScriptDb>().SaveBatteryCount(batteryCount);
    }

    void LoadBatteryCount()
    {
        batteryCount = FindObjectOfType<ScriptDb>().LoadBatteryCount();
    }
}