using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteractable : Interactable
{
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
        this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.white;
    }

    public override void ResetState()
    {
        base.ResetState();
        this.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
    }
}
