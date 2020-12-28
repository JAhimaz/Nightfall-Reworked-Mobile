using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateValues : MonoBehaviour{
    // UI Elements
    public GameObject settlementGO;
    public Environment environment;
    private Settlement settlement;

    [Header("Game Information")]
    public TMP_Text settlementName;
    public TMP_Text dayText;
    public TMP_Text settlerCount;
    public TMP_Text scavengerCount;
    public TMP_Text water, food, metal, wood;

    void Update(){
        if(settlement == null){
            settlementGO = GameObject.FindGameObjectWithTag("SettlementTag");
            settlement = settlementGO.GetComponent<Settlement>();
            settlementName.text = settlement.settlementName;
        }

        if(settlement.settlers.Count <= 2){
            settlerCount.text = "Settlers: <color=red>" + settlement.settlers.Count.ToString() + "</color>";
        }else if(settlement.settlers.Count <= 7){
            settlerCount.text = "Settlers: <color=orange>" + settlement.settlers.Count.ToString() + "</color>";
        }else{
            settlerCount.text = "Settlers: <color=green>" + settlement.settlers.Count.ToString() + "</color>";
        }
        scavengerCount.text = "Scavengers: <color=orange>" + settlement.scavengers.Count.ToString() + "</color>";
        dayText.text = "Day " + environment.day.ToString("000");
        water.text = settlement.water.ToString();
        food.text = settlement.food.ToString();
        metal.text = settlement.metal.ToString();
        wood.text = settlement.wood.ToString();
    }
}
