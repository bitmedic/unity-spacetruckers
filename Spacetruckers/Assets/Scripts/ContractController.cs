using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractController : MonoBehaviour
{
    private float defaultContractDuration = 300f; // in Zeitschritte... Sekunden?? - Noch nicht implementiert
    private float defaultContractValue = 10000f; // in Zeitschritte... Sekunden?? - Noch nicht implementiert

    private List<GameObject> allPlanets;
    private List<EnumCargo> allCargo;

    // Start is called before the first frame update
    void Start()
    {
        this.allPlanets = new List<GameObject>();
        allPlanets.AddRange(GameObject.FindGameObjectsWithTag("Planet"));

        this.allCargo = new List<EnumCargo>();
        foreach (GameObject planet in this.allPlanets)
        {
            PlanetCargoController pcc = planet.GetComponent<PlanetCargoController>();
            if (pcc != null)
            {
                allCargo.Add(pcc.producedGoods);
            }
        }

        this.allCargo.Remove(EnumCargo._Nichts); // delete EnumCargo._Nichts as this is the placeholder for no cargo

        // debug test
        Contract temp = this.createNewRandomContract();
        Debug.Log("Contract created: " + temp.contractCargo + " to " + temp.targetPlanet);
    }

    private void Update()
    {
        // TODO randomly create contracts ever X timestept


    }

    private Contract createNewRandomContract()
    {        
        int randomPlanetIndex = (int)Mathf.Round(Random.value * (this.allPlanets.Count - 1));
        GameObject planet = this.allPlanets[randomPlanetIndex];
        
        EnumCargo cargo, targetPlanetCargo;
        do
        {
            int randomCargoIndex = (int)Mathf.Round(Random.value * this.allCargo.Count - 1);
            cargo = this.allCargo[randomCargoIndex]; // get a random produced cargo
            targetPlanetCargo = planet.GetComponent<PlanetCargoController>().producedGoods; // get the cargo form the target planet
        } while (cargo == null || cargo.Equals(targetPlanetCargo)); // do this until a cargo is found that is not produced on the target system

        return new Contract(planet, cargo, this.defaultContractValue, this.defaultContractDuration);
    }
}
