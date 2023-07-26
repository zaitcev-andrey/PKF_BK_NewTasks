using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellColor : MonoBehaviour
{
    private static InterfaceManager _manager;

    private void Start()
    {
        _manager = GameObject.FindObjectOfType<InterfaceManager>();
    }

    public static void ChangeColorForObjectOnClick(int index)
    {
        _manager.OpenColorPickerPanelForObject(index);       
    }
}
