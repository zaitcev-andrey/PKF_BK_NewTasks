using UnityEngine;
using UnityEngine.UI;

public class ColorPickerManager : MonoBehaviour
{
    #region private Members
    [SerializeField] private GameObject _colorPickerPanel;
    [SerializeField] private ColorPicker _colorPicker;
    [SerializeField] private RectTransform _panelForListOfElementsRT;

    private RectTransform _rt;
    private float _defaultPositionY;
    #endregion

    #region private Methods
    private void Start()
    {
        _rt = _colorPickerPanel.GetComponent<RectTransform>();
        _defaultPositionY = _rt.anchoredPosition.y;
    }

    private void Update()
    {
        if (!_colorPicker.GetReadyStatus() && _colorPickerPanel.activeSelf)
        {
            _colorPickerPanel.SetActive(false);
        }
    }
    #endregion

    #region public Methods
    public void TurnOnColorPicker(Image buttonImageOnInterface, GameObject objectOnScene, float positionYOnRectTransform)
    {
        _colorPicker.SetButtonImageColorAndObjectColor(buttonImageOnInterface, objectOnScene);
        _colorPicker.SetReady();

        float newPositionY = _defaultPositionY + positionYOnRectTransform + _panelForListOfElementsRT.anchoredPosition.y;
        _rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x, newPositionY);

        _colorPickerPanel.SetActive(true);
    }
    #endregion
}
