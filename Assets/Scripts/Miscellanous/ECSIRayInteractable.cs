using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECSEnvironments.Interfaces
{
    public interface ECSIRayInteractable
    {
        void OnRayEnter();
        void OnRayExit();
        void OnRayTrigger();
    }
}
