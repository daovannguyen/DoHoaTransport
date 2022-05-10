using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMessage <T> // T là message, H là netmessage
{
    public virtual string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public virtual  T FromJson(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
