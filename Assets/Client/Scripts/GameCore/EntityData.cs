using System;
using UnityEngine;

[Serializable]
public class EntityData
{
    [field: SerializeField] public float Health {get; set;}
    [field: SerializeField] public float Damage {get; set;}

    public bool isDied {get; set;}
}
