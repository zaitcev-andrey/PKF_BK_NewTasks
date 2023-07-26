using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    #region private Members
    [SerializeField] private Transform _sourceTarget;

    private float _maxLimit = 90; // ограничение вращения по оси X
    private float _minLimit = 10;
    private float _zoomMax;
    private float _zoomMin;
    private Vector3 _offset;
    private float _x, _y;
    private Transform _target;
    #endregion

    #region public Members
    public float RotationHorizontalSensitivity { get; set; } = 300f;
    public float RotationVerticalSensitivity { get; set; } = 300f;
    public float ZoomSensitivity { get; set; } = 250f;
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
        UpdateRotationAndPosition();
    }

    private void UpdateRotationAndPosition()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) _offset.z += ZoomSensitivity * Time.deltaTime;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) _offset.z -= ZoomSensitivity * Time.deltaTime;
        _offset.z = Mathf.Clamp(_offset.z, -_zoomMax, -_zoomMin);

        _x = transform.localEulerAngles.y - Input.GetAxis("Horizontal") * RotationHorizontalSensitivity * Time.deltaTime;
        _y += Input.GetAxis("Vertical") * RotationVerticalSensitivity * Time.deltaTime;
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
