using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class TargetContact : MonoBehaviour
{
    [Header("Detect Targets")] public bool touchingTarget;
    const string k_Target = "target"; // Tag on target object.

    /// <summary>
    /// Check for collision with a target.
    /// </summary>
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag(k_Target))
        {
            touchingTarget = true;
        }
    }

    /// <summary>
    /// Check for end of ground collision and reset flag appropriately.
    /// </summary>
    void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag(k_Target))
        {
            touchingTarget = false;
        }
    }
}
