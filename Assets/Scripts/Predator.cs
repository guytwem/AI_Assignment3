using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Life
{
    public GameObject predator;

    public Prey preyClass;

    [SerializeField] private float seekPlayerDistance = 3f;
    [SerializeField] private float minAttackDistance = 1f;

    [SerializeField] private GameObject[] predatorWaypointArray;
    private int currentWaypoint = 0;


    public void Start()
    {
        NextState();
    }

    private State predatorState;

    private enum State { Wander, Seek, Attack }



    private IEnumerator WanderState()
    {
        Debug.Log("Predator Wandering");
        while (predatorState == State.Wander)
        {
            Wander();
            yield return null;
            if (Vector2.Distance(preyClass.prey.transform.position, predator.transform.position) < seekPlayerDistance)
            {
                predatorState = State.Seek;
            }

        }
        NextState();
    }

    private IEnumerator SeekState()
    {
        Debug.Log("Predator Seeking");
        while (predatorState == State.Seek)
        {
            Seek();
            yield return null;
            if (Vector2.Distance(preyClass.prey.transform.position, predator.transform.position) < minAttackDistance)
            {
                predatorState = State.Attack;
            }
        }
        NextState();
    }

    private IEnumerator AttackState()
    {
        Debug.Log("Predator Attacking");
        while (predatorState == State.Attack)
        {
            Attack();
            yield return null;
            if (Vector2.Distance(preyClass.prey.transform.position, predator.transform.position) > minAttackDistance)
            {
                predatorState = State.Seek;
            }
        }

        NextState();
    }



    private void Wander()
    {
        float distance = Vector2.Distance(predator.transform.position, predatorWaypointArray[currentWaypoint].transform.position);
        if (distance < minDistanceToWaypoint)
        {
            currentWaypoint++;
        }

        if (currentWaypoint >= predatorWaypointArray.Length)
        {
            currentWaypoint = 0;
        }

        Move(predator, predatorWaypointArray[currentWaypoint].transform.position);
    }

    private void Seek()
    {
        if (Vector2.Distance(preyClass.prey.transform.position, predator.transform.position) <= seekPlayerDistance)
        {
            Move(predator, preyClass.prey.transform.position);
        }
        else
        {
            Wander();
        }
    }

    private void Attack()
    {
        float damage = 1 * Time.deltaTime;

        if (Vector2.Distance(preyClass.prey.transform.position, predator.transform.position) <= minAttackDistance)
        {
            Move(predator, preyClass.prey.transform.position);
            Damage();
        }

        else
        {
            Seek();
        }
    }

    private void NextState()
    {
        string methodName = predatorState.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    public void Damage()
    {
        preyClass.health = Mathf.Max(0, preyClass.health - 7 * Time.deltaTime);
        if (preyClass.health == 0)
        {
            preyClass.Die();
        }
    }
    //collision avoidance
    //pursuit
}
