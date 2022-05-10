using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddCollider : MonoBehaviour
{
    public GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        AddMeshColliderForModel(model);
    }

    // đệ quy 
    void AddMeshColliderForModel(GameObject model)
    {
        int counts = model.transform.childCount;
        if (counts == 0)
        {
            model.AddComponent<MeshCollider>();
        }
        else
        {
            for (int i=0; i<counts; i++)
            {
                AddMeshColliderForModel(model.transform.GetChild(i).gameObject);
            }
        }    
    }

    // Update is called once per frame
    void Update()
    {

    }
}
