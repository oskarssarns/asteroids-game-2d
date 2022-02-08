using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool WrapWidth = true;
    public bool WrapHeight = true;

    private Renderer _renderer;
    private Transform _transform;
    private Camera _camera;
    private Vector2 _viewportPosition;
    private bool _isWrappingWidth;
    private bool _isWrappingHeight;
    private Vector2 _newPosition;

    void Start() 
    {
        _renderer = GetComponent<Renderer>();
        _transform = transform;
        _camera = Camera.main;
        _viewportPosition = Vector2.zero;
        _isWrappingWidth = false;
        _isWrappingHeight = false;
        _newPosition = _transform.position;
    }

    void LateUpdate()
    {
        Wrap();
    }

    private void Wrap()
    {
        bool isVisible = IsBeingRendered();

        if (isVisible)
        {
            _isWrappingWidth = false;
            _isWrappingHeight = false;
        }

        _newPosition = _transform.position;
        _viewportPosition = _camera.WorldToViewportPoint(_newPosition);

        if (WrapWidth)
        {
            if (!_isWrappingWidth)
            {
                if (_viewportPosition.x > 1)
                {
                    _newPosition.x = _camera.ViewportToWorldPoint(Vector2.zero).x;
                    _isWrappingWidth = true;
                }
                else if (_viewportPosition.x < 0)
                {
                    _newPosition.x = _camera.ViewportToWorldPoint(Vector2.one).x;
                    _isWrappingWidth = true;
                }
            }
        }

        if (WrapHeight)
        {
            if (!_isWrappingHeight)
            {
                if (_viewportPosition.y > 1)
                {
                    _newPosition.y = _camera.ViewportToWorldPoint(Vector2.zero).y;
                    _isWrappingHeight = true;
                }
                else if (_viewportPosition.y < 0)
                {
                    _newPosition.y = _camera.ViewportToWorldPoint(Vector2.one).y;
                    _isWrappingHeight = true;
                }
            }
        }

        _transform.position = _newPosition;
    }

    private bool IsBeingRendered()
    {
        return _renderer.isVisible;
    }
}
