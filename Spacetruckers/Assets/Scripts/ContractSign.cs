using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractSign : MonoBehaviour
{
    public CargoSO contractCargo;

    internal void UpdateContract(Contract contract)
    {
        contractCargo = contract.contractCargo;

        UpdateImages();
    }

    void UpdateImages()
    {
        if (contractCargo != null)
        {
            Image[] images = GetComponentsInChildren<Image>();
            images[0].sprite = contractCargo.cargoSprite;
            images[1].sprite = contractCargo.cargoSprite;
        }
    }
}
