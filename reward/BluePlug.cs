using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePlug : MonoBehaviour
{
    public playerController playerController; // This should be the correct reference
    private playerController PC;
    public Text task;

    void Start()
    {
        PC = playerController;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PC.picked = true;
            task.text = "Fix The Circuit Box.";
            Destroy(gameObject, 1f);
        }
    }
}
