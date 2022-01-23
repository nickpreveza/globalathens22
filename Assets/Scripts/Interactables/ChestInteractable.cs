using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    [SerializeField] Animator chestAnimator;

    private void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        if (!isInteractable)
        {
            return;
        }
        if (hasBeenInteracted)
        {
            if (oneOffInteraction)
            {
                OneOffInteract();
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
    }

    private void SuccessfulInteraction()
    {
        isInteractable = false;
        chestAnimator.SetTrigger("Open");
        UIManager.Instance.OpenPopup("Trinket Unlocked");
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().PlayChestOpen();
        GameManager.Instance.playerController.GetComponent<Teleport>().SetTeleportState(true);
    }

    public override void OneOffInteract()
    {
        hasBeenInteracted = true;
        isInteractable = false;

        SuccessfulInteraction();

        if (shouldDestoryOnEnd)
        {
            this.gameObject.SetActive(false);
        }

    }
}
