using UnityEngine;

public class CellButtonOnClick : MonoBehaviour
{
    private static InterfaceManager _manager;

    private void Start()
    {
        _manager = GameObject.FindObjectOfType<InterfaceManager>();
    }

    public static void SelectGameObjectOnClick(int index)
    {
        _manager.AllObjectsInInterface[index].IsChange = true;
        _manager.AllObjectsInInterface[index].IsSelectObject = true;
    }
}
