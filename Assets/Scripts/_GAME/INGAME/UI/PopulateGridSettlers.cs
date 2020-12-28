using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGridSettlers : MonoBehaviour{
    
    public GameObject settlerViewPrefab;
    public GameObject settlementGO;
    public Settlement settlement;

    public List<GameObject> createdObjs = new List<GameObject>();

    public void Populate(){
        if(settlementGO == null){
            settlementGO = GameObject.FindGameObjectWithTag("SettlementTag");
            settlement = settlementGO.GetComponent<Settlement>();
        }

        GameObject newObj;
        
        for (int i = 0; i < settlement.settlers.Count; i++){
            newObj = (GameObject) Instantiate(settlerViewPrefab, transform);
            Settler settler = settlement.settlers[i].GetComponent<Settler>();
            string gender = "";
            if(settler.gender == 'M'){
                gender = "MALE";
            }else if(settler.gender == 'F'){
                gender = "FEMALE";
            }
            newObj.GetComponent<SettlerDetails>().InitialiseValues(settler.id, settler.firstName, settler.lastName, settler.age, settler.health, gender, settler.isScavenging, settler.isBuilding);
            createdObjs.Add(newObj);
        }
    }

    public void Depopulate(){
        for(int i = 0; i < createdObjs.Count; i++){
            Destroy(createdObjs[i]);
        }
    }

        public void Repopulate(){
        Depopulate();
        Populate();
    }
}
