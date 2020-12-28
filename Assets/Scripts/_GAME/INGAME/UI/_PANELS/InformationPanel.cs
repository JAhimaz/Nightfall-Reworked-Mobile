using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InformationPanel : MonoBehaviour
{   
    [Header("Main Game")]
    public GameObject mainGame;
    public GameObject settlementGO;
    public Settlement settlement;
    [Header("Settlers")]
    public GameObject settlerPanel;
    public GameObject settlerContent;
    [Header("Scavengers")]
    public GameObject scavengerPanel;
    public GameObject scavengerContent;

    public GameObject scavengingPanel;
    public GameObject scavengingSettlerContent;
    public GameObject scavengingScavengerContent;
    [Header("Actions")]
    public GameObject actionPanel;

    //Settlers
    void Update(){
        if(settlement == null){
            settlementGO = GameObject.FindGameObjectWithTag("SettlementTag");
            settlement = settlementGO.GetComponent<Settlement>();
        }
    }

    public void onSettlerInfoOpen(){
        settlerContent.GetComponent<PopulateGridSettlers>().Populate();
        settlerPanel.SetActive(true);
        mainGame.SetActive(false);
    }

    public void onSettlerInfoClose(){
        settlerContent.GetComponent<PopulateGridSettlers>().Depopulate();
        settlerPanel.SetActive(false);
        mainGame.SetActive(true);
    }

    //Scavengers

    public void onScavengerInfoOpen(){
        scavengerContent.GetComponent<PopulateGridScavengers>().Populate();
        scavengerPanel.SetActive(true);
        mainGame.SetActive(false);
    }

    
    public void onScavengerInfoClose(){
        scavengerContent.GetComponent<PopulateGridScavengers>().Depopulate();
        scavengerPanel.SetActive(false);
        mainGame.SetActive(true);        
    }

    //Action Panel

    public void ActionPanelOpen(){
        actionPanel.SetActive(true);
        mainGame.SetActive(false);
    }

    public void ActionPanelClose(){
        actionPanel.SetActive(false);
        mainGame.SetActive(true);   
    }

    //Scavenging Panel
    public void ScavengingPanelOpen(){
        actionPanel.SetActive(false);
        scavengingScavengerContent.GetComponent<PopulateGridScavengers>().Populate();
        scavengingSettlerContent.GetComponent<PopulateGridSettlers>().Populate();
        scavengingPanel.SetActive(true);
    }

    public void ScavengingPanelClose(){
        actionPanel.SetActive(true);
        scavengingScavengerContent.GetComponent<PopulateGridScavengers>().Depopulate();
        scavengingSettlerContent.GetComponent<PopulateGridSettlers>().Depopulate();
        scavengingPanel.SetActive(false);
        if(!settlement.isScavenging){
            settlement.moveScavengersToSettlers();
        }
    }

    //Main Menu

    public void ReturnToMainMenu(){
        SceneManager.LoadScene(0);
    }

}
