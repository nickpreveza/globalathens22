using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip TrinketOn;
    public AudioClip Transition;

    public void PlayTrinket() {
        StartCoroutine(PlayTrinketWithDelay());
    }

    private IEnumerator PlayTrinketWithDelay() {
        GameObject newSound = new GameObject();
        newSound.AddComponent<AudioSource>();
        yield return new WaitForSeconds(2f);
        newSound.GetComponent<AudioSource>().PlayOneShot(TrinketOn);
    }

    public void PlayTransition() {
        GameObject newSound = new GameObject();
        newSound.AddComponent<AudioSource>();
        newSound.GetComponent<AudioSource>().PlayOneShot(Transition);
    }
}
