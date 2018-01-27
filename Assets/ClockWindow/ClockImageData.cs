using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ClockSetting")]
public class ClockImageData : ScriptableObject
{
    public Texture[] Images = new Texture[10];
    public Texture colon;
}

