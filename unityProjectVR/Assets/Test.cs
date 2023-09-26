using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject importManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        var importManager = importManagerObject.GetComponent<ImportManager>();
        var pool = importManager.ResourcePool;

        int i = 2;

        pool.List.ForEach(resource => {
            ResourceUI.Create(resource, new Vector3(-2, i++, 0), Quaternion.identity, new UISettings());
            });
    }
}
