using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectCharactor : MonoSingleton<SelectCharactor>
{
    public List<GameObject> playerPrefabs;
    [HideInInspector]
    public int indexPlayer;
    [HideInInspector]
    public GameObject playerSelect;

    public Button Left_Btn;
    public Button Right_Btn;

    float rotationSpeed;
    public Vector3 charactorPosition;
    public bool isQuayCungChieu;
    // Start is called before the first frame update

    void Init()
    {
        rotationSpeed = 100f;
        playerPrefabs = new List<GameObject>();
        charactorPosition = Vector3.zero;
    }

    void CreateAllPrefabs()
    {
        foreach (var i in DataOnClient.Instance.PlayerPrefabs)
        {
            GameObject a = Instantiate(i, charactorPosition, Quaternion.identity);
            a.GetComponent<Rigidbody>().useGravity = false;
            playerPrefabs.Add(a);
        }
    }

    private void Awake()
    {
        Init();
        CreateAllPrefabs();
    }
    void Start()
    {
        DisplayCharactor(0);
        Left_Btn.onClick.AddListener(OnClickLeftButton);
        Right_Btn.onClick.AddListener(OnClickRightButton);
    }

    void DisplayCharactor(int index)
    {
        foreach (var i in playerPrefabs)
        {
            i.SetActive(false);
        }
        playerPrefabs[index].SetActive(true);
        indexPlayer = index;
        playerSelect = playerPrefabs[index].gameObject;
    }
    void ChangeCharactor(int change)
    {
        indexPlayer += change;
        if (indexPlayer < 0)
            indexPlayer = playerPrefabs.Count - 1;
        else if (indexPlayer >= playerPrefabs.Count)
            indexPlayer = 0;
        DisplayCharactor(indexPlayer);
        DataOnClient.Instance.indexPlayerPrefab = indexPlayer;
    }
    void OnClickLeftButton()
    {
        ChangeCharactor(-1);
    }
    void OnClickRightButton()
    {
        ChangeCharactor(+1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            isQuayCungChieu = !isQuayCungChieu;
        }
        if (isQuayCungChieu)
        {
            float rotation = rotationSpeed * Time.deltaTime;
            playerSelect.transform.Rotate(0, rotation, 0);
        }
        else
        {
            float rotation = -rotationSpeed * Time.deltaTime;
            playerSelect.transform.Rotate(0, rotation, 0);
        }
    }
}