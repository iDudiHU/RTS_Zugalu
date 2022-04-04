using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DisplayName : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _displayedName;
    [SerializeField]
    private NameSO _nameSO;
    void Start()
    {
        _displayedName.text = _nameSO.PlayerInput;
    }

}
