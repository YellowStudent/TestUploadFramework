using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPackingData {
    
}
public class GamePackingData
{
    public bool isHit;
}
public class CharacterPackingData
{
    public CharacterPackingData()
    {
        gunRotate = new Vector2();
    }
    public Direction direction;
    public Vector2 gunRotate;
}
public class NetPackingData
{
    public Direction direction = Direction.None;
}
public enum Direction
{
    Left,
    Right,
    None
}

