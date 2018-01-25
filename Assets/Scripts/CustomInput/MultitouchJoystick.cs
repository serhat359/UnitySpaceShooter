using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MultitouchJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IAxisInputProvider
{
    bool isDragging = false;
    Vector3 firstPosition;
    Transform joystick;

    int fingerId;
    public float maxDistance = 100f;
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    // The order: Horizontal, Vertical
    float[] axisValues = new float[2];

    // Use this for initialization
    void Start()
    {
        firstPosition = transform.position;
        joystick = transform.GetChild(0);

        CustomInput.RegisterAxis(verticalAxis, this);
        CustomInput.RegisterAxis(horizontalAxis, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown(Vector3 finger)
    {
        transform.position = finger;
    }

    void OnMouseUp(Vector3 finger)
    {
        transform.position = firstPosition;
        joystick.localPosition = Vector3.zero;

        axisValues[0] = 0f;
        axisValues[1] = 0f;
    }

    void OnMouseDrag(Vector3 finger)
    {
        Vector3 distance = finger - transform.position;
        distance = distance.normalized * Mathf.Min(distance.magnitude, maxDistance);

        axisValues[0] = distance.x / maxDistance;
        axisValues[1] = distance.y / maxDistance;

        //Debug.Log (distance / maxDistance);
        joystick.position = distance + transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDragging)
        {
            OnMouseDown(eventData.position);
            isDragging = true;
            fingerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (fingerId == eventData.pointerId)
        {
            isDragging = false;
            OnMouseUp(eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            if (fingerId == eventData.pointerId)
            {
                OnMouseDrag(eventData.position);
            }
        }
    }

    public float GetAxis(string axisName)
    {
        if (axisName == horizontalAxis)
        {
            return axisValues[0];
        }
        else if (axisName == verticalAxis)
        {
            return axisValues[1];
        }
        else
        {
            throw new Exception("This axis is not handled");
        }
    }
}
