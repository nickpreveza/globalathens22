using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    private int _currentUniverse = 0;
    public bool canTeleport = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canTeleport) {
            switch (_currentUniverse) {
                case 0:
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 200);
                    _currentUniverse = 1;
                    break;
                case 1:
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
                    _currentUniverse = 0;
                    break;
            }
        }
    }
}
