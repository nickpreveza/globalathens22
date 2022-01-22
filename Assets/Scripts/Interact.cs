using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interact : MonoBehaviour {
    public GameObject crosshairIdle;
    public GameObject crosshairActive;

    private float _maxDistance = 2f;
    private Interactable selectedInteractable;
    // Update is called once per frame
    private void Update() {
        bool hasInteractableInCrosshair = CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E) && hasInteractableInCrosshair) {
            TryInteract();
        }
    }

    private bool  CheckForInteractable() {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, _maxDistance)) {
            if (hitInfo.transform.gameObject.CompareTag("Interactable"))
            {
                // Hit an interactable.
                crosshairActive.SetActive(true);

                if (selectedInteractable == null || selectedInteractable.gameObject != hitInfo.transform.gameObject)
                {
                    selectedInteractable = hitInfo.transform.gameObject.GetComponent<Interactable>();
                }


                return true;
            }
        }
        else {
            // Nothing interactable found.
            selectedInteractable = null;
            crosshairActive.SetActive(false);
            return false;
        }

        return false;
    }

    private void TryInteract() {
        if (selectedInteractable == null)
        {
            return;
        }

        selectedInteractable.Interact();
    }
}
