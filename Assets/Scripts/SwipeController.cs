using System;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float xClamp;

    [Space] 
    [SerializeField] private float turnSpeed = 500;

    private Transform _targetCorner;
    
    private float _firstValue;
    private float _currentValue;
    
    private float _screenWidth;

    private bool _touchable = true;

    private bool _turnLeft;

    private const float Sensitivity = 80f;
    private const float DegreeClampBound = 2;
    
    private float _sumValue;

    public bool isReachXBound;

    private void Awake()
    {
        ActionController.Instance.AddMethodFail(DisableSwipe);
        ActionController.Instance.AddMethodFinish(DisableSwipe);
    }

    private void Start()
    {
        _screenWidth = Screen.width;
    }

    private void FixedUpdate()
    {
        var posX = transform.position.x;
        var value = GetTouchedPos();
        
        isReachXBound = (posX > xClamp && !_turnLeft) || (posX < -xClamp && _turnLeft);
        
        if (!_touchable || isReachXBound) return;

        RotateAround(value);

    }

    private float GetTouchedPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstValue = Input.mousePosition.x;
            
            _sumValue = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            _currentValue = Input.mousePosition.x;

            _sumValue = _currentValue - _firstValue;

            _turnLeft = _sumValue < 0;
            
            _sumValue /= _screenWidth;

            _sumValue *= Sensitivity;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _sumValue = 0;
        }

        _firstValue = _currentValue;
        
        return _sumValue;
    }

    private void RotateAround(float value)
    {
        var clampedNum = Mathf.Clamp(value, -DegreeClampBound, DegreeClampBound);
        
        GetCorner();

        if (_targetCorner == null)
            return;


        transform.RotateAround(_targetCorner.position, Vector3.back, clampedNum * Time.deltaTime * turnSpeed);
    }

    private void GetCorner()
    {
        _targetCorner = _turnLeft
            ? CornersController.Instance.touchedLeftCorner
            : CornersController.Instance.touchedRightCorner;
    }

    private void DisableSwipe()
    {
        _touchable = false;
    }

}