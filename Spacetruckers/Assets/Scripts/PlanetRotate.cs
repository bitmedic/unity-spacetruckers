using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{

    public float rotationSpeed = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 2);

        transform.RotateAround(transform.position, transform.up, rotationSpeed * Time.deltaTime);
    }
}
