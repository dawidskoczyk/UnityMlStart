using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using UnityEngine.Events;

public class TargetController : MonoBehaviour
{
    [Header("Collider Tag To Detect")]
    public string tagToDetect = "agent"; //collider tag to detect

    [Header("Target Placement")]
    public float spawnRadius; //The radius in which a target can be randomly spawned.
    public bool respawnIfTouched; //Should the target respawn to a different position when touched

    [Header("Target Fell Protection")]
    public bool respawnIfFallsOffPlatform = true; //If the target falls off the platform, reset the position.
    public float fallDistance = 5; //distance below the starting height that will trigger a respawn

    private Vector3 m_startingPos; //the starting position of the target
                                   // Start is called before the first frame update

    [System.Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }

    [Header("Trigger Callbacks")]
    public TriggerEvent onTriggerEnterEvent = new TriggerEvent();
    public TriggerEvent onTriggerStayEvent = new TriggerEvent();
    public TriggerEvent onTriggerExitEvent = new TriggerEvent();

    [System.Serializable]
    public class CollisionEvent : UnityEvent<Collision>
    {
    }

    [Header("Collision Callbacks")]
    public CollisionEvent onCollisionEnterEvent = new CollisionEvent();
    public CollisionEvent onCollisionStayEvent = new CollisionEvent();
    public CollisionEvent onCollisionExitEvent = new CollisionEvent();

    // Start is called before the first frame update
    void OnEnable()
    {
        m_startingPos = transform.position;
        if (respawnIfTouched)
        {
            MoveTargetToRandomPosition();
        }
    }

    void Update()
    {
        if (respawnIfFallsOffPlatform)
        {
            if (transform.position.y < m_startingPos.y - fallDistance)
            {
                Debug.Log($"{transform.name} Fell Off Platform");
                MoveTargetToRandomPosition();
            }
        }
    }
    /// <summary>
    /// Moves target to a random position within specified radius.
    /// </summary>
    public void MoveTargetToRandomPosition()
    {
        var newTargetPos = m_startingPos + (Random.insideUnitSphere * spawnRadius);
        newTargetPos.y = m_startingPos.y;
        transform.position = newTargetPos;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag(tagToDetect))
        {
            if (respawnIfTouched)
            {
                MoveTargetToRandomPosition();
            }
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.transform.CompareTag(tagToDetect))
        {
            onCollisionStayEvent.Invoke(col);
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.transform.CompareTag(tagToDetect))
        {
            onCollisionExitEvent.Invoke(col);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(tagToDetect))
        {
            onTriggerEnterEvent.Invoke(col);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag(tagToDetect))
        {
            onTriggerStayEvent.Invoke(col);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(tagToDetect))
        {
            onTriggerExitEvent.Invoke(col);
        }
    }
}
