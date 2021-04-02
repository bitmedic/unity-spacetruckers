using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract
{
    public GameObject targetPlanet;
    public EnumCargo contractCargo;
    public float contractValue;
    public float contractDuration;

    public Contract(GameObject targetPlanet, EnumCargo contractCargo, float contractValue, float contractDuration)
    {
        this.targetPlanet = targetPlanet;
        this.contractCargo = contractCargo;
        this.contractValue = contractValue;
        this.contractDuration = contractDuration;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
