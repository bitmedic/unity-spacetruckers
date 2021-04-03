using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCargoController : MonoBehaviour
{
    public CargoSO producedGoods = null;

    private Contract currentContract;

    ContractSign sign;

    void Start()
    {
        sign = GetComponentInChildren<ContractSign>(true);

        SetContract(null);
    }

    public void SetContract(Contract newContract)
    {
        currentContract = newContract;
        if (currentContract == null)
        {
            sign.gameObject.SetActive(false);
        }
        else
        {
            sign.gameObject.SetActive(true);
            sign.UpdateContract(newContract);
        }
    }
}
