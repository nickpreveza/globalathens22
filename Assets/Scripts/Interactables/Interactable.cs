using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    public bool isInspectable = false;
    [HideInInspector]
    public bool highlighted;
    [HideInInspector]
    public bool hasBeenInteracted;

    public bool oneOffInteraction;
    public bool shouldDestoryOnEnd;
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

    public virtual void OneOffInteract()
    {

    }
}
