using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI popupText;
    public GameObject popupObject;
    public Animator popupAnimator;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        popupObject.SetActive(false);
    }

    public void OpenPopup(string _text)
    {
        popupText.text = _text;
        popupObject.SetActive(true);
        Invoke("ClosePopup", 2);
        StartCoroutine(ClosePopup());
    }

    public IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(2f);
        popupAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.4f);
        popupObject.SetActive(false);
    }
}
