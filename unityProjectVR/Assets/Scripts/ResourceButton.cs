using FlexiTeams.DataClasses.Resource;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResourceButton  : Button
{
    public Resource resource { get; set; }

    public ResourceButton() : base()
    {

    }
}