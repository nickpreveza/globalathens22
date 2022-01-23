using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    public bool isInspectable = false;
    public bool highlighted;
    public bool hasBeenInteracted;
    public bool isConsumable;
    public virtual void Interact()
    {
        hasBeenInteracted = true;
    }

    public virtual void ResetState()
    {
        hasBeenInteracted = false;
    }

    public virtual void TriggerPuzzleSolution()
    {

    }

}
