using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOnClient : MonoSingleton<DataOnClient>
{
    public PlayerData playerData;
    public PlayerData playerDataUri;

    public int InternalId;
    public RoomInstance room = new RoomInstance();
    public RoomInstance newRoom = new RoomInstance();
    public RoomInstance RoomMatched = new RoomInstance();

    #region Host
    public bool IsHost = false;
    public void SetPropertyHost()
    {
        IsHost = true;
    }
    #endregion

    #region Player Prefab
    [Header("PlayerPrefab")]
    [SerializeField]    // objects
    public GameObject[] PlayerPrefabs;
    [HideInInspector]
    public GameObject[] PlayerGameObjects;

    public GameObject[] SpawnPrefabs;
    [HideInInspector]
    public GameObject[] SpawnGameObjects;
    [HideInInspector]
    public int[] SpawnCounts = new int[] { 100, 100, 100, 100, 100, 100 };

    public void InitPrefab()
    {
        PlayerGameObjects = new GameObject[1000];
        SpawnGameObjects = new GameObject[1000];
    }

    public int indexPlayerPrefab;
    public void SetPlayer(CreatePlayerMessage om)
    {
        GameObject player = Instantiate(PlayerPrefabs[om.IndexPrefab], om.Position, Quaternion.identity);
        player.GetComponent<PlayerId>().Id = om.Id;
        player.GetComponent<PlayerId>().displayName = om.DisplayName;
        PlayerGameObjects[om.Id] = player;
        if (om.Id == InternalId)
        {
            Camera.main.transform.position = player.GetComponent<PlayerId>().CameraPosition.transform.position;
            Camera.main.transform.parent = player.GetComponent<PlayerId>().CameraPosition.transform;
        }
    }
    public void SetPlayerRoomMatched(CreatePlayerMessage om)
    {
        // cùng team
        GameObject player;
        if (room.PlayerExistInRoomByPlayerId(om.Id))
        {
            player = Instantiate(PlayerPrefabs[0], om.Position, Quaternion.identity);
        }
        else
        {
            player = Instantiate(PlayerPrefabs[1], om.Position, Quaternion.identity);
        }

        player.GetComponent<PlayerId>().Id = om.Id;
        player.GetComponent<PlayerId>().displayName = om.DisplayName;
        PlayerGameObjects[om.Id] = player; 
        if (om.Id == InternalId)
        {
            Camera.main.transform.position = player.GetComponent<PlayerId>().CameraPosition.transform.position;
            Camera.main.transform.parent = player.GetComponent<PlayerId>().CameraPosition.transform;
        }
    }
    public GameObject GetPlayerLocal()
    {
        return GetPlayerWithId(InternalId);
    }
    public GameObject GetPlayerWithId(int id)
    {
        return PlayerGameObjects[id];
    }

    public void SetSpawn(CreateObjectMessage om)
    {
        GameObject spawn = Instantiate(SpawnPrefabs[om.IndexPrefab], om.Position, Quaternion.identity);
        spawn.tag = "Spawn";
        spawn.AddComponent<ObjectId>();
        spawn.GetComponent<ObjectId>().Id = om.Id;
        SpawnGameObjects[om.Id] = spawn;
    }

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this);
        InitPrefab();
    }
}
