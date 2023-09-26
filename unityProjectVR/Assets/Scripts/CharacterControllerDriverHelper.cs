using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


//script for adjusting the character controller's postition each frame instead of input based positioning
//https://youtu.be/_Zrde_WTaiI?t=375
public class CharacterControllerDriverHelper : MonoBehaviour
{
    public XROrigin XROrigin;
    public CharacterController charController;
    public CharacterControllerDriver driver;
    
    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();
    }

    /// <summary>
    /// Updates the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
    /// based on the camera's position.
    /// </summary>
    protected virtual void UpdateCharacterController()
    {
        if (XROrigin == null || charController == null)
            return;

        var height = Mathf.Clamp(XROrigin.CameraInOriginSpaceHeight, driver.minHeight, driver.maxHeight);

        Vector3 center = XROrigin.CameraInOriginSpacePos;
        center.y = height / 2f + charController.skinWidth;

        charController.height = height;
        charController.center = center;
    }
}
