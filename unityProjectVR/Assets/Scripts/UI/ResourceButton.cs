using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResourceButton  : Button
{
    public ResourceId Id { get; set; }

    public ResourceButton() : base()
    {

    }
}