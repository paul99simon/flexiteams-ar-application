using Assets.Scripts.Application;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Workflow3DUI
{
    public class Layer3D
    {
        public VR_AR_Application Application;
        public UISettings Settings;
        public string Id;
        public Layout3D Layout;
        public readonly List<Workflow3D> Workflows = new();

        public float yPosition = 0;
        public float yIncrement = 0;

        public Layer3D(Layout3D layout, VR_AR_Application application, string id)
        {
            this.Layout = layout;
            this.Application = application;
            this.Id = id;
            this.Settings = Application.Settings;

            layout.Layers.Add(this);
        }

        public float GetHeight()
        {
            float height = 0;

            Workflows.ForEach(workflow =>
            {
                height += workflow.GetHeight();
            });

            height += (Workflows.Count-1) * Settings.Layout3D.Layer.Spacing;

            return height;
        }

        public void Add(Workflow3D workflow)
        {
            Workflows.Add(workflow);
        }

        public void Remove(Workflow3D workflow)
        {
            Workflows.Remove(workflow);
            if( Workflows.Count == 0 ) Delete();
        }

        public void CalculateLayout()
        {
            Workflows.ForEach(workflow =>  workflow.CalculateLayout());
        }

        public void Delete()
        {
            Layout.Remove(this);
        }
    }
}
