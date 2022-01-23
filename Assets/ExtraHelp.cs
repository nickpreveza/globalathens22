using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHelp : MonoBehaviour
{

    bool hasHelpedWithZoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasHelpedWithZoom)
        {
            UIManager.Instance.SendHelp("ZOOM WITH RIGHT MOUSE BUTTON");
        }
    }
}
