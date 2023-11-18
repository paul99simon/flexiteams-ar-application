using Assets.Scripts.Application;
using FlexiTeams.DataClasses.Task.Wrapper;
using UnityEngine;

namespace Assets.Scripts.UI.Workflow3DUI
{
    public class TaskButton3D : Button3D
    {
        public TaskId ID;
        public VR_AR_Application application;

        public void onClicK()
        {
            var temp = new TaskUI1(application.TaskPool[ID], new Vector3(1.7f, 1.6f, -1), Quaternion.Euler(new Vector3(0,45,0)), new Vector2(1000, 760), application.Settings);
        }
    }
}
