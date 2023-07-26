using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private RectTransform _rect;
    private Texture2D _colorTexture;

    private Image _buttonImageOnInterface;
    private GameObject _objectOnScene;

    private bool _isReady = false;
    private bool _isColorSave = false;
    private Color _previousImageColor;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    private void Update()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (_isReady)
        {
            if(!_isColorSave)
            {
                _isColorSave = true;
                _previousImageColor = _buttonImageOnInterface.color;
            }
            // условие отлавливает нажатия мыши только внутри панели цветов
            if (RectTransformUtility.RectangleContainsScreenPoint(_rect, Input.mousePosition))
            {
                Vector2 delta;
                // эта функция захватывает точку на всём экране
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, Input.mousePosition, null, out delta);

                // сделаем смещение дельты в левый нижний угол текстуры с выбором цвета
                float width = _rect.rect.width;
                float height = _rect.rect.height;
                delta += new Vector2(width * 0.5f, height * 0.5f);

                // ограничим значение курсора по x и y в текстуре от 0 до 1
                float x = Mathf.Clamp(delta.x / width, 0, 1);
                float y = Mathf.Clamp(delta.y / height, 0, 1);

                // получаем координаты пикселя в текстуре
                int texX = Mathf.RoundToInt(x * _colorTexture.width);
                int texY = Mathf.RoundToInt(y * _colorTexture.height);

                Color color = _colorTexture.GetPixel(texX, texY);

                _buttonImageOnInterface.color = color;
                if (Input.GetMouseButtonDown(0))
                {
                    Material mat = _objectOnScene.GetComponent<Renderer>().material;
                    color.a = mat.color.a;
                    mat.color = color;
                    SetDefaultBooleanValues();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(_buttonImageOnInterface.color != _previousImageColor)
                        _buttonImageOnInterface.color = _previousImageColor;
                    SetDefaultBooleanValues();
                }
            }
        }
    }

    private void SetDefaultBooleanValues()
    {
        _isReady = false;
        _isColorSave = false;
    }

    public void SetButtonImageColorAndObjectColor(Image buttonImageOnInterface, GameObject objectOnScene)
    {
        _buttonImageOnInterface = buttonImageOnInterface;
        _objectOnScene = objectOnScene;
        
        //_colorPickerPanel.transform.position.y = 
    }

    public bool GetReadyStatus()
    {
        return _isReady;
    }

    public void SetReady()
    {
        _isReady = true;
    }
}
