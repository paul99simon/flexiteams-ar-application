using Assets.Scripts.Application;
using Assets.Scripts.UI.ResourceUI;
using FlexiTeams.DataClasses.Resource.Wrapper;
using UnityEngine;
using UnityEngine.UI;



public class ResourceButton : Button
{
    public ResourceId Id { get; set; }
    public VR_AR_Application application { get; set; }

    public void OnClick()
    {
        _ = new ResourceUI(application.ResourcePool[Id], new Vector3(-2.5f, 1.6f, -1), Quaternion.Euler(new Vector3(0,-90, 0 )), new Vector2(1000, 760), application.Settings);
    }
}