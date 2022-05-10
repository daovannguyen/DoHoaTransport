using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSpawnObject : MonoSingleton<SelectSpawnObject>
{
    [SerializeField]
    public Material hightlightMaterial;
    Material defaultMaterial;
    Transform selection;
    public GameObject selectObject;
    public MoveObjectType controlType;

    private void Awake()
    {
        controlType = MoveObjectType.OBJECT;
    }
    void ChooseObject()
    {
        if (controlType == MoveObjectType.CAMERA)
        {
            selectObject = Camera.main.gameObject;
        }
        else if (controlType == MoveObjectType.OBJECT)
        {
            if (selection != null)
            {
                try
                {
                    selection.GetComponent<Renderer>().material = defaultMaterial;
                }
                catch { }
                selection = null;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // thua
                if (hit.transform.tag == "Spawn")
                {
                    selection = hit.transform;
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    defaultMaterial = selectionRenderer.material;
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = hightlightMaterial;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        selectObject = selection.gameObject;
                    }
                }
            }
        }
    }
    void ChooseControlType()
    {
        if (Input.GetKeyDown(KeyCode.C))
            controlType = MoveObjectType.CAMERA;
        else if (Input.GetKeyDown(KeyCode.O))
            controlType = MoveObjectType.OBJECT;
    }

    private void Update()
    {
        ChooseControlType();
        ChooseObject();
    }

}
