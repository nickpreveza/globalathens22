using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool devMode;
    public bool GodMode;
    [SerializeField] Transform lighthouseSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FirstPersonMovement.Instance.Respawn(false, lighthouseSpawn);
        }
    }
}
