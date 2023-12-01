using Assets.Scripts.Application;
using Assets.Scripts.UI.DataUI;
using FlexiTeams.DataClasses.Data.Wrapper;
using UnityEngine;
using UnityEngine.UI;

public class DataButton : Button
{
    public VR_AR_Application application;

    public void OnClick()
    {
        var position = application.GetInFrontOfCameraPosition(1.5f, 1.6f);
        var rotation = application.GetCameraOrientation();

        //_ = new DataUI(application.DataPool[Id], position, Quaternion.Euler(rotation), new Vector2(1000, 760), application.Settings);
    }
}