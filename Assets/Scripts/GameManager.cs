using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public FirstPersonMovement playerController;
    public bool devMode;
    [SerializeField] Transform lighthouseSpawn;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (devMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FirstPersonMovement.Instance.Respawn(false, lighthouseSpawn);
            }
        }
      

        if (Input.GetKeyDown(KeyCode.P) ||  Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.PauseToggle();
        }
    }
}
