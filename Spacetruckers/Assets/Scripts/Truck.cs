using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private GameObject targetPlanet;

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
            // check if target is a planet and call checkContracts on that planet
            // TODO: Wie???????
            if (targetPlanet != null) {
                Debug.Log("Arrived at planet " + targetPlanet.name);
                var pcc = targetPlanet.GetComponent<PlanetCargoController>();
                if (pcc != null && pcc.CanFulfillContractHere(this))
                {
                    Debug.Log("Truck can complete contract for " + this.loadedCargo.cargoName);
                    pcc.DeliverContract(this);
                }
            }

            Destroy(target.gameObject);
            target = null;
            targetPlanet = null;
            this.GetComponentInChildren<TruckAudioController>().setTruckMoving(false);
        }
    }

    public void LoadCargo(CargoSO cargo)
    {
        loadedCargo = cargo;
        SetCargoColor();
    }

    public void UnloadCargo()
    {
        loadedCargo = null;
        SetCargoColor();
    }

    public CargoSO GetLoadedCargo()
    {
        return this.loadedCargo;
    }

    private void SetCargoColor()
    {
        MeshRenderer mr = cargoBox.GetComponent<MeshRenderer>();
        if (loadedCargo != null)
        { 
            mr.material.color = loadedCargo.cargoColor;
        }
        else
        {
            mr.material.color = Color.grey;
        }
    }

    public void SetTarget(Transform target, GameObject planet)
    {
        this.target = target;
        this.targetPlanet = planet;
    }
}
