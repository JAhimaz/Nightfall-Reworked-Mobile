using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddToSettlerBTN : MonoBehaviour{

    public GameObject _gameManager;

    void Awake(){
        _gameManager = GameObject.FindGameObjectWithTag("_Environment");

        Button button= GetComponent<Button>();
        button.onClick.AddListener(_gameManager.GetComponent<ScavengeLogic>().SendToSettlers);
    }
}
