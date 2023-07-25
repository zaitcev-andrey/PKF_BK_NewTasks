using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellButtonOnClick : MonoBehaviour
{
    private static InterfaceManager manager;

    private void Start()
    {
        manager = GameObject.FindObjectOfType<InterfaceManager>();
    }

    public static void SelectGameObjectOnClick(int index)
    {
        manager.AllObjectsInInterface[index].IsChange = true;
        manager.AllObjectsInInterface[index].IsSelectObject = true;
    }
}
