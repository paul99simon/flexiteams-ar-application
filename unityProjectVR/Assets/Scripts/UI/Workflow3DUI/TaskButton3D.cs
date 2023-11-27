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
            var temp = new TaskUI1(application.TaskPool[ID], new Vector3(1.7f, 1.6f, -1), Quaternion.Euler(new Vector3(0,45,0)), new Vector2(1000, 760), application.Settings);
        }
    }
}
