using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public bool canRotate=true;
    public float speed=10;
    private Quaternion _startRotation;
    private void Start() {
        _startRotation = transform.rotation;
    }

    private void OnEnable() {
        transform.rotation = _startRotation;
    }

    private void Update () {
        if(canRotate) transform.Rotate(speed*Vector3.forward*Time.deltaTime);
    }
}