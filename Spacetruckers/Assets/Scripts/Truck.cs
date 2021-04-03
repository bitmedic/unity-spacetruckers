using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{

    public Transform target;
    public float truckSpeed = 5;
    public float turnSpeed = 10;

    [SerializeField]
    private CargoSO loadedCargo;

    private GameObject cargoBox;

    // Start is called before the first frame update
    void Start()
    {
        cargoBox = transform.Find("Cargo")?.gameObject;
        SetCargoColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            // stay on current y level
            Vector3 toTarget = target.position - transform.position;

            transform.forward = Vector3.Lerp(transform.forward, toTarget, turnSpeed * Time.deltaTime);

            transform.position += transform.forward * truckSpeed * Time.deltaTime;

            Debug.DrawRay(transform.position, transform.forward * truckSpeed, Color.red);
            Debug.DrawRay(transform.position, toTarget.normalized * turnSpeed, Color.green);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Destroy(target.gameObject);
            target = null;
            this.GetComponentInChildren<TruckAudioController>().setTruckMoving(false);
        }
    }

    public void LoadCargo(CargoSO cargo)
    {
        loadedCargo = cargo;
        SetCargoColor();
    }

    private void SetCargoColor()
    {
        MeshRenderer mr = cargoBox.GetComponent<MeshRenderer>();
        if (loadedCargo != null)
        { 
            mr.material.color = loadedCargo.cargoColor;
        }
    }
}
