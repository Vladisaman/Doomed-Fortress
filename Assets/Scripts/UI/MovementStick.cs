using UnityEngine;
using UnityEngine.EventSystems;

public class MovementStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBackground;
    private RectTransform joystickKnob;
    private Vector2 inputVector;

    private void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
        joystickKnob = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = touch.position;
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                    Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition);
                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.CompareTag("Wall"))
                        {
                            return;
                        }
                    }

                    transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
                }
            }
        } 
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 touchPosition = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Wall"))
                    {
                        return;
                    }
                }

                transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
                Debug.Log("Touched at: " + worldPosition);
            }
        }
    }
    

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystickKnob.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBackground.sizeDelta.x);
            pos.y = (pos.y / joystickBackground.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickKnob.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.sizeDelta.x / 2), inputVector.y * (joystickBackground.sizeDelta.y / 2));
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }
}