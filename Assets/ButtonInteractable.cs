using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Interactable
{
    private static readonly int ButtonPressed = Animator.StringToHash("ButtonPressed");
    private bool _playAnimation = false;
    private Vector3 startPosition;
    private Vector3 pressedPosition;
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
        startPosition = transform.position;
        pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        StartCoroutine(ButtonAnimation());
        GetComponentInParent<Puzzle1>().OnGlyphPressed(gameObject.name);
    }

    private IEnumerator ButtonAnimation() {
        isInteractable = false;
        transform.position = pressedPosition;
        yield return new WaitForSeconds(0.5f);
        transform.position = startPosition;
        ResetState();
        isInteractable = true;
    }
}