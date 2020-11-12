using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{

    public GameObject prey;
    public float health = 100;
    public Predator predatorClass;

    [SerializeField] private GameObject[] preyWaypointArray;
    private int currentWaypoint = 0;
    [SerializeField] private float minEvadeDistance = 3f;

    

    public void Start()
    {
        NextState();
    }

    private State preyState;

    private enum State { Wander, Evade }

    private IEnumerator WanderState()
    {
        Debug.Log("Prey Wandering");
        while (preyState == State.Wander)
        {
            Wander();
            yield return null;
            if (Vector2.Distance(prey.transform.position, predatorClass.predator.transform.position) < minEvadeDistance)
            {
                preyState = State.Evade;
            }
        }
        NextState();
    }

    private IEnumerator EvadeState()
    {
        Debug.Log("Prey Evading");
        while (preyState == State.Evade)
        {
            Evade();
            yield return null;
            if (Vector2.Distance(prey.transform.position, predatorClass.predator.transform.position) > minEvadeDistance)
            {
                preyState = State.Wander;
            }
        }
        NextState();
    }

    private void Wander()
    {
        float distance = Vector2.Distance(prey.transform.position, preyWaypointArray[currentWaypoint].transform.position);
        if (distance < minDistanceToWaypoint)
        {
            currentWaypoint++;
        }
        if (currentWaypoint >= preyWaypointArray.Length)
        {
            currentWaypoint = 0;
        }

        Move(prey, preyWaypointArray[currentWaypoint].transform.position);
    }

    private void Evade()
    {
        Vector2 runAway = prey.transform.position - predatorClass.predator.transform.position;

        if (Vector2.Distance(prey.transform.position, predatorClass.predator.transform.position) <= minEvadeDistance)
        {
            Move(prey, runAway);
            
        }
        else
        {
            Wander();
        }
    }

    private void NextState()
    {
        string methodName = preyState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    public void Die()
    {
        Destroy(prey);
    }
    //flock
    //wander
    //evade
    //hide
}
