using UnityEngine;
using UnityEngine.UI;

public class FloorIndicator : MonoBehaviour
{
    public GameObject lift;
    public Text floorText1;
    public Text floorText2;

    private void Update()
    {
        int currentFloor = GetCurrentFloor();

        string floorString = GetCurrentFloorString(currentFloor);

        floorText1.text = floorString;
        floorText2.text = floorString;
    }

    private int GetCurrentFloor()
    {
        float yPosition = lift.transform.position.y;

        if (yPosition >= 0f && yPosition < 4f)
        {
            return 0;
        }
        else if (yPosition >= 4f && yPosition < 8f)
        {
            return 1;
        }
        else if (yPosition >= 8f && yPosition < 12f)
        {
            return 2;
        }
        else if (yPosition >= 12f && yPosition < 16f)
        {
            return 3;
        }
        else if (yPosition >= 16f)
        {
            return 4;
        }
        else
        {
            return -1;
        }
    }

    private string GetCurrentFloorString(int floor)
    {
        switch (floor)
        {
            case 0:
                return "G";
            case 1:
                return "1F";
            case 2:
                return "2F";
            case 3:
                return "3F";
            case 4:
                return "4F";
            default:
                return "Unknown";
        }
    }
}
