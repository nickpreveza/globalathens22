using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    public bool isInspectable = false;
    public bool hasBeenInteracted;

    public virtual void Interact()
    {
        hasBeenInteracted = true;
    }

    public virtual void ResetState()
    {
        hasBeenInteracted = false;
    }


}
