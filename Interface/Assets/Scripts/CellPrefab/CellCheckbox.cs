using UnityEngine;

public class CellCheckbox : MonoBehaviour
{
    private static InterfaceManager _manager;

    private void Start()
    {
        _manager = GameObject.FindObjectOfType<InterfaceManager>();
    }

    #region public Methods
    public static void SwitchLocalCheckboxOnClick(int index)
    {
        _manager.AllObjectsInInterface[index].IsChange = true;
        _manager.AllObjectsInInterface[index].IsCheckBoxChange = true;
    }

    public static void SwitchGlobalCheckboxOnClick()
    {
        _manager.IsGlobalCheckboxesChange = true;
    }
    #endregion
}
