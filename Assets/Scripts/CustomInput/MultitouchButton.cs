using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultitouchButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IButtonInputProvider
{
    int fingerId;
    bool isFingerDown = false;
    bool isButtonDownSent = false;
    bool isButtonUpSent = false;
    public string buttonName;

    // Use this for initialization
    void Start()
    {
        CustomInput.RegisterButton(buttonName, this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isFingerDown)
        {
            fingerId = eventData.pointerId;
            isFingerDown = true;
            isButtonDownSent = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (fingerId == eventData.pointerId)
        {
            isFingerDown = false;
            isButtonUpSent = false;
        }
    }

    public bool GetButton(string button)
    {
        if (button == buttonName)
        {
            return isFingerDown;
        }
        else
        {
            throw new Exception("This button is not handled");
        }
    }

    public bool GetButtonDown(string button)
    {
        if (button == buttonName)
        {
            if (isFingerDown && !isButtonDownSent)
            {
                isButtonDownSent = true;
                return true;
            }

            return false;
        }
        else
        {
            throw new Exception("This button is not handled");
        }
    }

    public bool GetButtonUp(string button)
    {
        if (button == buttonName)
        {
            if (!isFingerDown && !isButtonUpSent)
            {
                isButtonUpSent = true;
                return true;
            }

            return false;
        }
        else
        {
            throw new Exception("This button is not handled");
        }
    }
}
