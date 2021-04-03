using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract
{
    public GameObject targetPlanet;
    public EnumCargo contractCargoType;
    public CargoSO contractCargo;
    public float contractValue;
    public float contractDuration;

    public Contract(GameObject targetPlanet, EnumCargo contractCargoType, float contractValue, float contractDuration)
    {
        this.targetPlanet = targetPlanet;
        this.contractCargoType = contractCargoType;
        this.contractValue = contractValue;
        this.contractDuration = contractDuration;
    }

    public Contract(GameObject targetPlanet, CargoSO contractCargo)
    {
        this.targetPlanet = targetPlanet;
        this.contractCargo = contractCargo;

    }
}
