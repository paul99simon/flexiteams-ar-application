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
        var position = application.GetInFrontOfCameraPosition(1.5f, 1.6f);
        var rotation = application.GetCameraOrientation();

        _ = new ResourceUI(application.ResourcePool[Id], position, Quaternion.Euler(rotation), new Vector2(1000, 760), application.Settings);
    }
}