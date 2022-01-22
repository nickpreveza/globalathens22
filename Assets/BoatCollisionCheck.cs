using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollisionCheck : MonoBehaviour
{
    [SerializeField] FirstPersonMovement controller;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            controller.touchedGround = true;
        }
    }
}
