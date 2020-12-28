using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomSettlementName : MonoBehaviour{
    public TMP_InputField settlementNameInput;

    public void SetRandomName(){        
        settlementNameInput.text = Misc.settlementnames[Random.Range(0, Misc.settlementnames.Length)];
    }
}