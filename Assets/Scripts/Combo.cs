using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Combo
{
    public string[] sequence;
    public float score;
    [System.NonSerialized]
    public string comboHash;
}
