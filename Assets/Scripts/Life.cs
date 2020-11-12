using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Life : MonoBehaviour
{
    
    public float minDistanceToWaypoint;
    public float speed = 5.0f;
    

    

    

    public virtual void Move(GameObject gameObject, Vector2 targetPosition)
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPosition, speed * Time.deltaTime);
    }
    

    

    
}
