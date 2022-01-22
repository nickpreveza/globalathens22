using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{

    public override void Interact()
    {
        if (!isInteractable)
        {
            return;
        }
        if (hasBeenInteracted)
        {
            if (isConsumable)
            {
                Consumable();
                return;
            }
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

    private void SuccessfulInteraction()
    {
        // Interactable action goes here.
        this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.white;
    }

    public void Consumable()
    {

        this.gameObject.SetActive(false);
    }
}
