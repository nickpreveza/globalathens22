using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCode : MonoBehaviour {

    [Range(1,4)]
    public int slot1;
    [Range(1, 4)]
    public int slot2;
    [Range(1, 4)]
    public int slot3;
    [Range(1, 4)]
    public int slot4;
    [Range(1, 4)]
    public int slot5;
    [Range(1, 4)]
    public int slot6;
    [Range(1, 4)]
    public int slot7;
    [Range(1, 4)]
    public int slot8;
    public GameObject Glyph1, Glyph2, Glyph3, Glyph4;
    // Start is called before the first frame update
    private bool isPuzzleReady = false;
    public Collider triggerCollider;
    public Interactable interactableToTriggerSolution;
    public Animator door1;
    public Animator door2;
    int[] solution;
    int currentPointer = 0;
    private void Start()
    {
        solution = new int[8];

        solution[0] = slot1;
        solution[1] = slot2;
        solution[2] = slot3;
        solution[3] = slot4;
        solution[4] = slot5;
        solution[5] = slot6;
        solution[6] = slot7;
        solution[7] = slot8;
    }
    private void CheckIfPuzzleReady() {
        if (Glyph1.activeSelf && Glyph2.activeSelf && Glyph3.activeSelf && Glyph4.activeSelf) {
            if (isPuzzleReady != true)
            {
                isPuzzleReady = true;
                UIManager.Instance.OpenPopup("DOORS UNLOCKED");
                door1.SetTrigger("Open");
                door2.SetTrigger("Open");
            }
           
        }

        if (isPuzzleReady) {
            triggerCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.name) {
            case "Glyph1Unplugged":
                Destroy(other.gameObject);
                GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().PlayButtonPressed();
                Glyph1.SetActive(true);
                GameManager.Instance.playerController.interact.ExitInspectMode();
                //GameObject.FindWithTag("Player").GetComponent<Interact>().ExitInspectMode();
                CheckIfPuzzleReady();
                break;
            case "Glyph2Unplugged":
                Destroy(other.gameObject);
                GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().PlayButtonPressed();
                Glyph2.SetActive(true);
                GameManager.Instance.playerController.interact.ExitInspectMode();
                CheckIfPuzzleReady();
                break;
            case "Glyph3Unplugged":
                Destroy(other.gameObject);
                GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().PlayButtonPressed();
                Glyph3.SetActive(true);
                GameManager.Instance.playerController.interact.ExitInspectMode();
                CheckIfPuzzleReady();
                break;
            case "Glyph4Unplugged":
                Destroy(other.gameObject);
                GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().PlayButtonPressed();
                Glyph4.SetActive(true);
                GameManager.Instance.playerController.interact.ExitInspectMode();
                CheckIfPuzzleReady();
                break;
        }
    }

    public void OnGlyphPressed(string glyphName) {
        switch (glyphName) {
            case "Glyph1Plugged":
                AddToSequence(1);
                break;
            case "Glyph2Plugged":
                AddToSequence(2);
                break;
            case "Glyph3Plugged":
                AddToSequence(3);
                break;
            case "Glyph4Plugged":
                AddToSequence(4);
                break;
        }
    }



    private void AddToSequence(int number) {
        if (number == solution[currentPointer]) {
            currentPointer++;
        }
        else currentPointer = 0;

        Debug.Log("Puzzle1 progress: " + currentPointer);

        if (currentPointer > solution.Length - 1) {
            Debug.Log("Solved!");
            StartCoroutine(OnSolved());
        }
    }

    private IEnumerator OnSolved() {
        // Stop interactions.
        Glyph1.GetComponent<ButtonInteractable>().isInteractable = false;
        Glyph2.GetComponent<ButtonInteractable>().isInteractable = false;
        Glyph3.GetComponent<ButtonInteractable>().isInteractable = false;
        Glyph4.GetComponent<ButtonInteractable>().isInteractable = false;
        Glyph1.tag = "Untagged";
        Glyph2.tag = "Untagged";
        Glyph3.tag = "Untagged";
        Glyph4.tag = "Untagged";
        // Wait 1 sec (takes 0.5 to finish last button animation).
        yield return new WaitForSeconds(1);
        // Glyphs lock in.
        Glyph1.transform.position = new Vector3(Glyph1.transform.position.x, Glyph1.transform.position.y - 0.15f, Glyph1.transform.position.z);
        Glyph2.transform.position = new Vector3(Glyph2.transform.position.x, Glyph2.transform.position.y - 0.15f, Glyph2.transform.position.z);
        Glyph3.transform.position = new Vector3(Glyph3.transform.position.x, Glyph3.transform.position.y - 0.15f, Glyph3.transform.position.z);
        Glyph4.transform.position = new Vector3(Glyph4.transform.position.x, Glyph4.transform.position.y - 0.15f, Glyph4.transform.position.z);
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().PlayButtonPressedSolved();
        // Open door.
        interactableToTriggerSolution.TriggerPuzzleSolution();
        UIManager.Instance.OpenPopup("Puzzle Solved");
        UIManager.Instance.EndingSequence();
    }
}
