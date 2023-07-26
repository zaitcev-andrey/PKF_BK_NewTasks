using UnityEngine;

public class CellEye : MonoBehaviour
{
    private static InterfaceManager _manager;

    private void Start()
    {
        _manager = GameObject.FindObjectOfType<InterfaceManager>();
    }

    public static void SwitchLocalEyeOnClick(int index)
    {
        _manager.AllObjectsInInterface[index].IsChange = true;
        _manager.AllObjectsInInterface[index].IsEyeChange = true;
    }

    public static void SwitchGlobalEyeOnClick()
    {
        _manager.IsGlobalEyesChange = true;
    }
}
