using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : MonoBehaviour
{
    public UnityEvent<int> OnSwipeEvent;
    //public delegate void OnSwipeInput(int i);

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    [SerializeField] private float _deadZone = 80;

    public static bool _isSwiping;
    private bool _isMobile;

    public float border = 0;
    
    private void Start()
    {
        if (OnSwipeEvent == null)
            OnSwipeEvent = new UnityEvent<int>();
        _isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if(!_isMobile)
        {
            if(Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _tapPosition= Input.mousePosition;
            }
            else if(Input.GetMouseButtonUp(0)) {
                ResetSwipe();
            }
        }
        else
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began) {
                _isSwiping= true;
                _tapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                    Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
        if(Input.mousePosition.x < border)
        {
            CheckSwipe();
        }
    }

    private void CheckSwipe()
    {
        _swipeDelta = Vector2.zero;

        if(_isSwiping)
        {
            if (!_isMobile && Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;
            else if (Input.touchCount > 0)
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if(_swipeDelta.magnitude > _deadZone)
        {
            if(OnSwipeEvent != null)
            {
                if (Mathf.Abs(_swipeDelta.y) > Mathf.Abs(_swipeDelta.x))
                {
                    OnSwipeEvent.Invoke(_swipeDelta.y > 0 ? 1 : -1);
                }   
            }
            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        _isSwiping =  false;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }
}
