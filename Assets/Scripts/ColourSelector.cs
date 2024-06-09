using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColourSelector : MonoBehaviour
{
    private void OnEnable() {
        GetComponent<TMP_Dropdown>().value = GameManager.GetGameManager().numberOfColours - 1;
    }
    public void GetColourCount(int val) {
        GameManager.GetGameManager().numberOfColours = val + 1;
    }
    
}
