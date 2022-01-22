using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] FirstPersonMovement controller;
    public bool canTeleport = true;
    public GameObject vehicleParent;
    public Material skybox0;
    public Material skybox1;
    void Awake()
    {
        controller = GetComponent<FirstPersonMovement>();
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
        switch (UniverseController.Instance.currentUniverse)
        {
            case 0:
                if (controller.inVechile && vehicleParent != null)
                {

                    vehicleParent.transform.position = new Vector3(vehicleParent.transform.position.x, vehicleParent.transform.position.y, vehicleParent.transform.position.z - UniverseController.Instance.universeDistance);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - UniverseController.Instance.universeDistance);
                }

                RenderSettings.skybox = skybox1;
                UniverseController.Instance.currentUniverse = 1;
                break;
            case 1:

                if (controller.inVechile && vehicleParent != null)
                {
                    GameObject targetGameObject = this.transform.parent.transform.parent.gameObject;
                    targetGameObject.transform.position = new Vector3(vehicleParent.transform.position.x, vehicleParent.transform.position.y, vehicleParent.transform.position.z + UniverseController.Instance.universeDistance);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + UniverseController.Instance.universeDistance);
                }
                RenderSettings.skybox = skybox0;
                UniverseController.Instance.currentUniverse = 0;
                break;
        }
    }
}
