using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Achievement : ScriptableObject
{
    public string title;
    public string description;
    public abstract bool IsCompleted();
}
