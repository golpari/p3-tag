//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LightHandler : PossessionController
//{
//    protected void Run(GameObject currObject)
//    {
//        Debug.Log("run");
//    }
//}

using UnityEngine;

public class LightHandler : MonoBehaviour, IPossessionAction
{
    private bool isDark = false;
    private bool isActive = false;

    public void EnableAction()
    {
        isActive = true;
    }

    public void DisableAction()
    {
        isActive = false;
    }

    public void ToggleLighting()
    {
        if (!isActive) return;
        isDark = !isDark;
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
    }
}

