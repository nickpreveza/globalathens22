using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    [SerializeField] Animator doorAnimator;
    bool isOpen;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        if (!isInteractable)
        {
            return;
        }
        if (hasBeenInteracted)
        {
            ResetState();
            return;
        }
        base.Interact();
        SuccessfulInteraction();
    }

    public override void ResetState()
    {
        base.ResetState();
        this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
    }

    private void SuccessfulInteraction() {
        // Interactable action goes here.
        //gameObject.SetActive(false);
        doorAnimator.SetTrigger("Open");
    }

    public override void TriggerPuzzleSolution()
    {
        base.TriggerPuzzleSolution();
        SuccessfulInteraction();
    }
}