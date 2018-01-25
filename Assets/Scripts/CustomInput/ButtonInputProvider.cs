public interface IButtonInputProvider
{
    bool GetButton(string buttonName);
    bool GetButtonDown(string buttonName);
    bool GetButtonUp(string buttonName);
}
