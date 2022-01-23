using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : Interactable {
    public GameObject doorToOpen;

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
    }

    private void SuccessfulInteraction() {
        // Interactable action goes here.
        doorToOpen.SetActive(false);
        //Material newMat = ;

        this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.black;
    }
}