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
    private Transform inspectableObjectStartTransform;
    Vector3 cameraToWorld;
    private GameObject _inspectingObject;
    private Transform _inspectingObjectStartParent;
    private Vector3 _inspectingObjectStartPosition;
    private Quaternion _inspectingObjectStartRotation;
    private bool isInspecting = false;
    public RectTransform mainCanvasRect;
    public RectTransform crosshairRect;
    private Interactable selectedInteractable;
    private GameObject _newParent;

    private void Start() {
        _newParent = new GameObject();
        _newParent.name = "RotateParent";
        _newParent.transform.parent = inspectPosition.transform;
        _newParent.transform.position = inspectPosition.transform.position;
    }

    private void Update() {
        if (_inspectingObject == null) _hasInteractableInCrosshair = CheckForInteractable(); // If not in inspect mode, check for interactables.
        else _hasInteractableInCrosshair = false;

        if (!_hasInteractableInCrosshair) crosshairActive.SetActive(false);

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

                cameraToWorld = Camera.main.WorldToViewportPoint(_selectedInteractable.transform.position);
                Vector2 WorldObject_ScreenPosition = new Vector2(
                                               ((cameraToWorld.x * mainCanvasRect.sizeDelta.x) - (mainCanvasRect.sizeDelta.x * 0.5f)),
                                               ((cameraToWorld.y * mainCanvasRect.sizeDelta.y) - (mainCanvasRect.sizeDelta.y * 0.5f)));

                //Making sure it's forward (markRect is my UI Element's RectTransform)
                if (cameraToWorld.z > 0) crosshairRect.anchoredPosition = WorldObject_ScreenPosition;
                return true;
            }
        }
        else {
            // Nothing interactable found.
            _selectedInteractable = null;
            crosshairActive.SetActive(false);
        }

        return false;
    }

    private void TryInteract() {
        if (_selectedInteractable.isInspectable) EnterInspectMode(); // First try for inspect.
        else if (_selectedInteractable.isInteractable) _selectedInteractable.Interact(); // If not inspectable, interact.
    }

    // Pick up.
    private void EnterInspectMode() {
        GetComponent<FirstPersonMovement>().canRun = false;
        GetComponent<FirstPersonMovement>().speed /= 2;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        _inspectingObject = _selectedInteractable.gameObject;

        _inspectingObjectStartParent = _inspectingObject.transform.parent;
        _inspectingObjectStartPosition = _inspectingObject.transform.position;
        _inspectingObjectStartRotation = _inspectingObject.transform.rotation;

        _inspectingObject.transform.parent = inspectPosition.transform;
        _inspectingObject.transform.position = inspectPosition.transform.position;
        _inspectingObject.transform.rotation = inspectPosition.transform.rotation;
    }

    // Holding.
    private void OnInspectMode() {
        var rotationSpeed = 100f;
        // Left click down.
        if (Input.GetMouseButtonDown(0)) {
            GetComponent<FirstPersonMovement>().canWalk = false;
            GetComponentInChildren<FirstPersonLook>().sensitivity = 0;
            crosshairIdle.SetActive(false);
            crosshairActive.SetActive(false);
            _newParent.transform.rotation = transform.rotation;
        }
        // Left click hold.
        else if (Input.GetMouseButton(0)) {
            _inspectingObject.transform.parent = _newParent.transform;
            var xAngle = (Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime);
            var yAngle = (-Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime); // this is correct
            var zAngle = 0f;
            _inspectingObject.transform.parent.Rotate(xAngle,yAngle, zAngle, Space.Self);
        }
        // Left click up.
        else if (Input.GetMouseButtonUp(0)) {
            GetComponent<FirstPersonMovement>().canWalk = true;
            GetComponentInChildren<FirstPersonLook>().sensitivity = 2;
            crosshairIdle.SetActive(true);
            _inspectingObject.transform.parent = inspectPosition.transform;
        }
    }

    // Put down.
    private void ExitInspectMode() {
        GetComponent<FirstPersonMovement>().speed *= 2;
        GetComponent<FirstPersonMovement>().canRun = true;
        _inspectingObject.transform.parent = _inspectingObjectStartParent;
        _inspectingObject.transform.position = _inspectingObjectStartPosition;
        _inspectingObject.transform.rotation = _inspectingObjectStartRotation;
        _inspectingObject = null;
    }
}
