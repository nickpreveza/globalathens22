using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] FirstPersonMovement controller;
    private int _currentUniverse = 0;
    public bool canTeleport = true;
    public GameObject vehicleParent;
    void Awake()
    {
        controller = GetComponent<FirstPersonMovement>();
        vehicleParent = transform.parent.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //requires a validate method
        if (Input.GetKeyDown(KeyCode.T) && canTeleport) {
            TeleportPlayer();   
        }
    }

    void TeleportPlayer()
    {
        switch (_currentUniverse)
        {
            case 0:
                if (controller.inVechile)
                {

                    vehicleParent.transform.position = new Vector3(vehicleParent.transform.position.x, vehicleParent.transform.position.y, vehicleParent.transform.position.z - UniverseController.Instance.universeDistance);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 200);
                }
              
                _currentUniverse = 1;
                break;
            case 1:
              
                if (controller.inVechile)
                {
                    GameObject targetGameObject = this.transform.parent.transform.parent.gameObject;
                    targetGameObject.transform.position = new Vector3(vehicleParent.transform.position.x, vehicleParent.transform.position.y, vehicleParent.transform.position.z + UniverseController.Instance.universeDistance);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
                }
                _currentUniverse = 0;
                break;
        }
    }
}
