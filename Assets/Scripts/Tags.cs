using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags
{
    public const string Enemy = "Enemy";
    public const string Laser = "Laser";
    public const string Player = "Player";
}

public class Parameters
{
    public const string Die = "Die";
    public const string Trigger = "Trigger";
    public const string AnimationCount = "AnimationCount";
}

public class PowerupType
{
    public const string Speed = "Speed";
    public const string Power = "Power";

    public static readonly string[] AllPowerUps = { Speed, Power };
}