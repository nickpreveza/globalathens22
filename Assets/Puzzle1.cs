using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour {
    public GameObject Glyph1, Glyph2, Glyph3, Glyph4;
    // Start is called before the first frame update
    private bool isPuzzleReady = false;
    public Collider triggerCollider;
    private void Update() {

    }

    private void CheckIfPuzzleReady() {
        if (Glyph1.activeSelf && Glyph2.activeSelf && Glyph3.activeSelf && Glyph4.activeSelf) {
            isPuzzleReady = true;
        }

        if (isPuzzleReady) {
            triggerCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.name) {
            case "Glyph1Unplugged":
                Destroy(other.gameObject);
                Glyph1.SetActive(true);
                GameObject.FindWithTag("Player").GetComponent<Interact>().ExitInspectMode();
                CheckIfPuzzleReady();
                break;
            case "Glyph2Unplugged":
                Destroy(other.gameObject);
                Glyph2.SetActive(true);
                GameObject.FindWithTag("Player").GetComponent<Interact>().ExitInspectMode();
                CheckIfPuzzleReady();
                break;
            case "Glyph3Unplugged":
                Destroy(other.gameObject);
                Glyph3.SetActive(true);
                GameObject.FindWithTag("Player").GetComponent<Interact>().ExitInspectMode();
                CheckIfPuzzleReady();
                break;
            case "Glyph4Unplugged":
                Destroy(other.gameObject);
                Glyph4.SetActive(true);
                GameObject.FindWithTag("Player").GetComponent<Interact>().ExitInspectMode();
                CheckIfPuzzleReady();
                break;
        }
    }
}
