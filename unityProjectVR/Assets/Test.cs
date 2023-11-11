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

        //_ = new ResourceUI(pool.List[0], new Vector3(-3, 1.5f, 1), Quaternion.Euler(new Vector3(0,-90,0)), new Vector2(1000, 760));
        
        pool.List.ForEach(resource => {
            _ = new ResourceUI(resource, new Vector3(-2, i++, 0), Quaternion.identity, new Vector2(1000, 760));
            });
    }
}