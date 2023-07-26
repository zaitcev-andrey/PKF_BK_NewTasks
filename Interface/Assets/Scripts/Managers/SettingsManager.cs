using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    #region private Members
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Slider _horizontalSensitivitySlider;
    [SerializeField] private Slider _verticalSensitivitySlider;
    [SerializeField] private Slider _zoomSensitivitySlider;

    private CameraRotation _cameraRotation;
    #endregion

    #region private Methods
    private void Start()
    {
        _cameraRotation = FindObjectOfType<CameraRotation>();
        _horizontalSensitivitySlider.value = _cameraRotation.RotationHorizontalSensitivity;
        _verticalSensitivitySlider.value = _cameraRotation.RotationVerticalSensitivity;
        _zoomSensitivitySlider.value = _cameraRotation.ZoomSensitivity;
    }

    private void Update()
    {
        _cameraRotation.RotationHorizontalSensitivity = _horizontalSensitivitySlider.value;
        _cameraRotation.RotationVerticalSensitivity = _verticalSensitivitySlider.value;
        _cameraRotation.ZoomSensitivity = _zoomSensitivitySlider.value;
    }
    #endregion

    #region public Methods
    public void BackOnCLick()
    {
        _pauseMenuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }
    #endregion
}
