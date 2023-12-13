using Assets.Scripts.Application;
using Assets.Scripts.UI.DataUI;
using Assets.Scripts.UI.ResourceUI;
using Assets.Scripts.UI.TaskUI;
using UnityEngine;

public class Test : MonoBehaviour
{
    private VR_AR_Application application;

    // Start is called before the first frame update
    void Start()
    {
        application = GameObject.Find("Application").GetComponent<VR_AR_Application>();
        var rPool = application.ResourcePool;
        var dPool = application.DataPool;
        var tPool = application.TaskPool;
        var wNodes = application.Graph.GetWorkflowNodes();

        var layout3D = GameObject.Find("Layout 3D").GetComponent<Layout3D>();
        
        var layer = layout3D.workflowMap[wNodes[0].Id].Layer;
        var node = layout3D.workflowMap[wNodes[1].Id];

        node.ChangeLayer(layer);


        /*
        int i = 2;

        _ = new ResourceUI(pool.List[0], new Vector3(-3, 1.5f, 1), Quaternion.Euler(new Vector3(0,-90,0)), new Vector2(1000, 760));
        
        rPool.List.ForEach(resource =>
        {
            _ = new ResourceUI(resource, new Vector3(-2, i++, 0), Quaternion.identity, new Vector2(1000, 760));
        });

        int heigth = 2;
        for (int j = 0; j <= 80; j += 20)
        {
            _ = new DataUI(dPool.List[j], new Vector3(-1, heigth++, 0), Quaternion.identity, new Vector2(1000, 760));
        }

        heigth = 2;
        for (int j = 0; j <= 4; j++)
        {
            _ = new TaskUI1(tPool.List[j], new Vector3(0, heigth++, 0), Quaternion.identity, new Vector2(1000, 760));
        }
        */

    }



}