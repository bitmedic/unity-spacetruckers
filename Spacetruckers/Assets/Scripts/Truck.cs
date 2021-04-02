using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{

    public Transform target;
    public float truckSpeed = 5;
    public float turnSpeed = 10;


    internal EnumCargo loadedCargo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            // stay on current y level
            Vector3 targetPosition = target.position;

            transform.forward = Vector3.Lerp(transform.forward, targetPosition - transform.position, turnSpeed * Time.deltaTime);

            transform.position += transform.forward * truckSpeed * Time.deltaTime;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Destroy(target.gameObject);
            target = null;
        }
    }
}
