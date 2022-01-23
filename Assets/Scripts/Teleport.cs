using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] FirstPersonMovement controller;
    bool canTeleport = true;
    public GameObject vehicleParent;
    [SerializeField] Animator handsAnimator;
    [SerializeField] float chargeTime;
    [SerializeField] float cooldown;
    [SerializeField] GameObject trinket;
    [SerializeField] ParticleSystem teleportEffect;
    void Awake()
    {
        controller = GetComponent<FirstPersonMovement>();
        SetTeleportState(false);
        teleportEffect.Stop();
    }

    public void SetTeleportState(bool enabled)
    {
        canTeleport = enabled;
        trinket.SetActive(canTeleport);
       
    }
    // Update is called once per frame
    void Update()
    {
        //requires a validate method
        if (Input.GetKeyDown(KeyCode.T) && canTeleport) {
            StartCoroutine(TeleportPlayer());
        }
    }

    IEnumerator TeleportPlayer()
    {
        canTeleport = false;
        teleportEffect.Play();
        switch (UniverseController.Instance.currentUniverse)
        {
            case 0:
                handsAnimator.SetTrigger("ChangeToInu");
                yield return new WaitForSeconds(chargeTime);
                if (controller.inVechile && vehicleParent != null)
                {

                    vehicleParent.transform.position = new Vector3(vehicleParent.transform.position.x, vehicleParent.transform.position.y, vehicleParent.transform.position.z - UniverseController.Instance.universeDistance);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - UniverseController.Instance.universeDistance);
                }

                RenderSettings.skybox = UniverseController.Instance.skybox1;
                UniverseController.Instance.currentUniverse = 1;
                yield return new WaitForSeconds(cooldown);
                canTeleport = true;
                break;
            case 1:
                handsAnimator.SetTrigger("ChangeToAki");
                yield return new WaitForSeconds(chargeTime);
                if (controller.inVechile && vehicleParent != null)
                {
                    GameObject targetGameObject = this.transform.parent.transform.parent.gameObject;
                    targetGameObject.transform.position = new Vector3(vehicleParent.transform.position.x, vehicleParent.transform.position.y, vehicleParent.transform.position.z + UniverseController.Instance.universeDistance);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + UniverseController.Instance.universeDistance);
                }
                RenderSettings.skybox = UniverseController.Instance.skybox0;
                UniverseController.Instance.currentUniverse = 0;
                yield return new WaitForSeconds(cooldown);
                canTeleport = true;
                break;
        }
    }
}
