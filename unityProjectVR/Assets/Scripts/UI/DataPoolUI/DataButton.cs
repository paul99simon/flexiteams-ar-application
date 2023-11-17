using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataButton  : Button
{
    public DataId Id { get; set; }

    public DataButton() : base()
    {

    }
}