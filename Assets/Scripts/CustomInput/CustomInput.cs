using System.Collections.Generic;
using UnityEngine;

public class CustomInput : MonoBehaviour
{
    static CustomInput instance;

    Dictionary<string, List<IAxisInputProvider>> axisProviders = new Dictionary<string, List<IAxisInputProvider>>();
    Dictionary<string, List<IButtonInputProvider>> buttonProviders = new Dictionary<string, List<IButtonInputProvider>>();

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void RegisterAxis(string axisName, IAxisInputProvider provider)
    {
        List<IAxisInputProvider> providerList;
        if (instance.axisProviders.TryGetValue(axisName, out providerList))
        {
            providerList.Add(provider);
        }
        else
        {
            providerList = new List<IAxisInputProvider>
            {
                provider
            };
            instance.axisProviders[axisName] = providerList;
        }
    }

    public static void RegisterButton(string buttonName, IButtonInputProvider provider)
    {
        List<IButtonInputProvider> providerList;
        if (instance.buttonProviders.TryGetValue(buttonName, out providerList))
        {
            providerList.Add(provider);
        }
        else
        {
            providerList = new List<IButtonInputProvider>
            {
                provider
            };
            instance.buttonProviders[buttonName] = providerList;
        }
    }

    public static float GetAxis(string axisName)
    {
        List<IAxisInputProvider> providerList;
        if (instance.axisProviders.TryGetValue(axisName, out providerList))
        {
            foreach (var provider in providerList)
            {
                float axisValue = provider.GetAxis(axisName);

                if (axisValue != 0)
                    return axisValue;
            }
        }

        return Input.GetAxis(axisName);
    }

    public static float GetAxisRaw(string axisName)
    {
        float axisValue = GetAxis(axisName);

        if (axisValue > 0)
            return 1f;
        else if (axisValue < 0)
            return -1f;
        else
            return 0f;
    }

    public static bool GetButton(string buttonName)
    {
        List<IButtonInputProvider> providerList;
        if (instance.buttonProviders.TryGetValue(buttonName, out providerList))
        {
            foreach (var provider in providerList)
            {
                bool buttonValue = provider.GetButton(buttonName);

                if (buttonValue)
                    return buttonValue;
            }
        }

        return Input.GetButton(buttonName);
    }

    public static bool GetButtonDown(string buttonName)
    {
        List<IButtonInputProvider> providerList;
        if (instance.buttonProviders.TryGetValue(buttonName, out providerList))
        {
            foreach (var provider in providerList)
            {
                bool buttonValue = provider.GetButtonDown(buttonName);

                if (buttonValue)
                    return buttonValue;
            }
        }

        return Input.GetButtonDown(buttonName);
    }

    public static bool GetButtonUp(string buttonName)
    {
        List<IButtonInputProvider> providerList;
        if (instance.buttonProviders.TryGetValue(buttonName, out providerList))
        {
            foreach (var provider in providerList)
            {
                bool buttonValue = provider.GetButtonUp(buttonName);

                if (buttonValue)
                    return buttonValue;
            }
        }

        return Input.GetButtonUp(buttonName);
    }
}
