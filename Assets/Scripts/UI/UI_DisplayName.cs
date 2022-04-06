using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Class that handles setting the inputted name
public class UI_DisplayName : MonoBehaviour
{
    [Tooltip("Ui text mesh pro to be changed to the name")]
    [SerializeField]
    private TMP_Text _displayedName;
    [Tooltip("Given name at Main Menu")]
    [SerializeField]
    private NameSO _nameSO;
    void Start()
    {
        _displayedName.text = _nameSO.PlayerInput;
    }

}
