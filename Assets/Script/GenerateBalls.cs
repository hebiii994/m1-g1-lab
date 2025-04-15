using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBalls : MonoBehaviour
{
    public GameObject spherePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateSphere()
    {
        if (spherePrefab != null) // Good practice to check if it's assigned
        {
            Instantiate(spherePrefab);
        }
        else
        {
            Debug.LogError("Sphere Prefab is not assigned in the Inspector!");
        }
    }
}
