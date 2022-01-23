using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip Ambience1;
    public AudioClip Ambience2;
    public AudioClip Waves;
    public AudioClip TrinketOn;
    public AudioClip Transition;
    public AudioClip ChestOpen;
    public AudioClip ButtonPressed1;
    public AudioClip ButtonPressed2;
    public AudioClip ButtonPressed3;
    public AudioClip ButtonPressed4;
    public AudioClip ButtonUnpressed;
    private AudioSource _ambienceSource;
    private AudioSource _coastWavesSource;

    public void Start() {
        _ambienceSource = gameObject.AddComponent<AudioSource>(); // ambience
        _ambienceSource.loop = true;
        _ambienceSource.clip = Ambience1;
        _ambienceSource.Play();

        _coastWavesSource = gameObject.AddComponent<AudioSource>(); // sea
        _coastWavesSource.loop = true;
        _coastWavesSource.clip = Waves;
        _coastWavesSource.Play();
    }

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

    public void PlayChestOpen() {
        GameObject newSound = new GameObject();
        newSound.AddComponent<AudioSource>();
        newSound.GetComponent<AudioSource>().PlayOneShot(ChestOpen);
    }

    public void PlayButtonPressed() {
        GameObject newSound = new GameObject();
        newSound.AddComponent<AudioSource>();
        var random = new System.Random().Next(0, 4);
        switch (random) {
            case 0:
                newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed1, 0.7f);
                break;
            case 1:
                newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed2, 0.7f);
                break;
            case 2:
                newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed3, 0.7f);
                break;
            case 3:
                newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed4, 0.7f);
                break;
        }
    }

    public void PlayButtonUnpressed() {
        GameObject newSound = new GameObject();
        newSound.AddComponent<AudioSource>();
        newSound.GetComponent<AudioSource>().PlayOneShot(ButtonUnpressed, 0.7f);
    }

    public void PlayButtonPressedSolved() {
        GameObject newSound = new GameObject();
        newSound.AddComponent<AudioSource>();
        newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed1, 0.7f);
        newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed2, 0.7f);
        newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed3, 0.7f);
        newSound.GetComponent<AudioSource>().PlayOneShot(ButtonPressed4, 0.7f);
    }

    public void ChangeAmbienceTo1() {
        _ambienceSource.clip = Ambience1;
        _ambienceSource.Play();
    }

    public void ChangeAmbienceTo2() {
        _ambienceSource.clip = Ambience2;
        _ambienceSource.Play();
    }
}
