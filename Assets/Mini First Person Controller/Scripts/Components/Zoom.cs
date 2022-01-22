using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 30;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;

    private const float CustomZoomSpeed = 2f;
    private const float CustomUnzoomSpeed = 5f;

    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
    }

    void Update()
    {
        // OLD
        // Update the currentZoom and the camera's fieldOfView.
        //currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        //currentZoom = Mathf.Clamp01(currentZoom);
        //camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);

        // Custom
        CustomZoom();
    }

    void CustomZoom() {
        if (Input.GetMouseButton(1)) {
            if (camera.fieldOfView > maxZoomFOV) {
                camera.fieldOfView -= CustomZoomSpeed;
            }
            else camera.fieldOfView = maxZoomFOV;
        }
        else {
            if (camera.fieldOfView < defaultFOV) camera.fieldOfView += CustomUnzoomSpeed;
            else camera.fieldOfView = defaultFOV;
        }
    }
}
