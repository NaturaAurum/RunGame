using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float JumpSpeed;
    public float MoveSpeed;
    public int MaxJumpCount;
    public float GravityValue;
}
