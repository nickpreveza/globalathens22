using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Interact : MonoBehaviour {
    public GameObject crosshairIdle;
    public GameObject crosshairActive;
    private bool _hasInteractableInCrosshair = false;

    private const float MaxDistance = 2f;

    private Interactable _selectedInteractable;

    // Inspect:
    public GameObject inspectPosition;
    private GameObject _inspectingObject;
    private Transform _inspectingObjectStartParent;
    private Vector3 _inspectingObjectStartPosition;
    private Quaternion _inspectingObjectStartRotation;

    private void Update() {
        if (_inspectingObject == null) _hasInteractableInCrosshair = CheckForInteractable(); // If not in inspect mode, check for interactables.
        else _hasInteractableInCrosshair = false;

        if (Input.GetKeyDown(KeyCode.E)) {
            if (_inspectingObject != null) { // If we are inspecting an object,
                ExitInspectMode(); // stop inspecting.
                return;
            }

            if (_hasInteractableInCrosshair && _inspectingObject == null) TryInteract(); // If no interactable in crosshair and not in inspect mode, try interact.
        }

        if (_inspectingObject != null) OnInspectMode();
    }

    private bool CheckForInteractable() {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        if (Physics.Raycast(ray, out var hitInfo, MaxDistance)) {
            if (hitInfo.transform.gameObject.CompareTag("Interactable"))
            {
                // Hit an interactable.
                crosshairActive.SetActive(true);

                if (_selectedInteractable == null || _selectedInteractable.gameObject != hitInfo.transform.gameObject)
                {
                    _selectedInteractable = hitInfo.transform.gameObject.GetComponent<Interactable>();
                }
                return true;
            }
        }
        else {
            // Nothing interactable found.
            _selectedInteractable = null;
            crosshairActive.SetActive(false);
            return false;
        }

        return false;
    }

    private void TryInteract() {
        if (_selectedInteractable.isInspectable) {
            EnterInspectMode();
        }
        else if (_selectedInteractable.isInteractable) {
            _selectedInteractable.Interact();
        }
    }

    // Pick up.
    private void EnterInspectMode() {
        Debug.Log("Inspect started");
        _inspectingObject = _selectedInteractable.gameObject;
        _inspectingObjectStartParent = _inspectingObject.transform.parent;
        _inspectingObjectStartPosition = _inspectingObject.transform.position;
        _inspectingObjectStartRotation = _inspectingObject.transform.rotation;
        GetComponent<FirstPersonMovement>().canWalk = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        // TODO: Set camera to fixed.
        _inspectingObject.transform.parent = inspectPosition.transform;
        _inspectingObject.transform.position = inspectPosition.transform.position;
        _inspectingObject.transform.rotation = inspectPosition.transform.rotation;
    }

    private void OnInspectMode() {
        const float rotationSpeed = 100f;
        // Mouse down.
        if (Input.GetMouseButtonDown(0)) {
            crosshairIdle.SetActive(false);
            crosshairActive.SetActive(false);
            GetComponentInChildren<FirstPersonLook>().sensitivity = 0;
        }
        // Mouse hold.
        else if (Input.GetMouseButton(0)) {
            _inspectingObject.transform.Rotate((Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), (Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), 0, Space.World);
        }
        // Mouse up.
        else if (Input.GetMouseButtonUp(0)) {
            crosshairIdle.SetActive(true);
            crosshairActive.SetActive(true);
            GetComponentInChildren<FirstPersonLook>().sensitivity = 2;
        }
    }

    // Put down.
    private void ExitInspectMode() {
        Debug.Log("Inspect quit");
        GetComponent<FirstPersonMovement>().canWalk = true;
        // TODO: Set camera to fixed.
        _inspectingObject.transform.parent = _inspectingObjectStartParent;
        _inspectingObject.transform.position = _inspectingObjectStartPosition;
        _inspectingObject.transform.rotation = _inspectingObjectStartRotation;
        _inspectingObject = null;
    }
}
