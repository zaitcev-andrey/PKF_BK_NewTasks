using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    #region private Members
    [SerializeField] private Transform _sourceTarget;

    private float _maxLimit = 90; // ограничение вращения по Y
    private float _minLimit = 10;
    private float _zoomMax; // макс. увеличение
    private float _zoomMin; // мин. увеличение  
    private Vector3 _offset;
    private float _x, _y;
    private Transform _target;
    #endregion

    #region public Members
    public float rotationSensitivity = 300f; // чувствительность мышки
    public float zoomSensitivity = 250f; // чувствительность при увеличении, колесиком мышки
    #endregion

    #region private Methods
    private void Start()
    {
        _target = _sourceTarget;
        if (_maxLimit > 90)
            _maxLimit = 90;
        _zoomMax = _target.localScale.x;
        _zoomMin = _zoomMax / 5;

        _offset = new Vector3(0, 0, -_zoomMax / 2);
        transform.position = _target.position + _offset;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) _offset.z += zoomSensitivity * Time.deltaTime;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) _offset.z -= zoomSensitivity * Time.deltaTime;
        _offset.z = Mathf.Clamp(_offset.z, -_zoomMax, -_zoomMin);

        _x = transform.localEulerAngles.y - Input.GetAxis("Horizontal") * rotationSensitivity * Time.deltaTime;
        _y += Input.GetAxis("Vertical") * rotationSensitivity * Time.deltaTime;
        _y = Mathf.Clamp(_y, _minLimit, _maxLimit);
        transform.localEulerAngles = new Vector3(_y, _x, 0);
        transform.position = _target.position + transform.localRotation * _offset;
    }
    #endregion

    #region public Methods
    public void ChangeTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    public void SetDefaultTarget()
    {
        _target = _sourceTarget;
    }
    #endregion
}
