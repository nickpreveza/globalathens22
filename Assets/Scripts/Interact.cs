using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interact : MonoBehaviour {
    public GameObject crosshairIdle;
    public GameObject crosshairActive;

    private float _maxDistance = 2f;
    // Update is called once per frame
    private void Update() {
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E)) {
            TryInteract();
        }
    }

    private void CheckForInteractable() {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, _maxDistance)) {
            if (hitInfo.transform.gameObject.GetComponent<Interactable>() != null) {
                // Hit an interactable.
                crosshairActive.SetActive(true);
                Debug.Log("Interactable");
            }
        }
        else {
            // Nothing interactable found.
            crosshairActive.SetActive(false);
            Debug.Log("Not Interactable");
        }
    }

    private void TryInteract() {
        var ray = new Ray();
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
    }
}
