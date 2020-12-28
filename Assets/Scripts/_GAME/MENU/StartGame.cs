using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour{

    public TMP_InputField settlementNameInput;
    public TMP_Text errorText;
    public Button startButton;
    public GameObject levelLoader;


    void Start(){
        errorText.text = "";
    }

    public void OnStartGame(){

        errorText.text = "";

        settlementNameInput.characterLimit = 24;
        string settlementName = settlementNameInput.text;

        if(string.IsNullOrEmpty(settlementName) || settlementName.Length >= settlementNameInput.characterLimit){
            if(settlementName.Length >= settlementNameInput.characterLimit){
                errorText.text = "Error: Maximum Characters is 24 (" + settlementName.Length + ")";
            }
            if(string.IsNullOrEmpty(settlementName)){
                errorText.text = "Error: Settlement Name Cannot Be Empty";
            }
        }else{
            PlayerPrefs.SetString("SettlementName", settlementNameInput.text);
            levelLoader.GetComponent<LevelLoader>().LoadLevel(1);
        }
    }

}
