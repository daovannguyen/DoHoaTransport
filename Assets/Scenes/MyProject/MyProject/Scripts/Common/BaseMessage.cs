using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMessage <T> // T l� message, H l� netmessage
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
