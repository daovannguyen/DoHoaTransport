using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOnServer : MonoSingleton<DataOnServer>
{
    public List<RoomInstance> rooms = new List<RoomInstance>();
    public int SpawnCount = -1;// dây là id của vật spawn


}
