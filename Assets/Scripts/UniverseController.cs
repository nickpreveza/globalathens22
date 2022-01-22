using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseController : MonoBehaviour
{
    public static UniverseController Instance;
    [SerializeField] GameObject universe1;
    [SerializeField] GameObject universe2;
    public float universeDistance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        universeDistance = universe1.transform.position.z - universe2.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
