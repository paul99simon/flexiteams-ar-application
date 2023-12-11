using FlexiTeams.DataClasses.Task.Wrapper;
using UnityEngine;
using Assets.Scripts.UI.TaskUI;

namespace Assets.Scripts.UI.Workflow3DUI
{
    public class TaskButton3D : Button3D
    {
        public TaskId ID;

        public void onClicK()
        {
            var position = application.GetInFrontOfCameraPosition(1.5f, 1.6f);
            var rotation = application.GetCameraOrientation();
            
            _ = new TaskUI1(application.TaskPool[ID], position, Quaternion.Euler(rotation), new Vector2(1000, 760), application.Settings);
        }
    }
}