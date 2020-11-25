using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Life/Behavior/Pursuit")]
public class PursuitBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(LifeAgent agent, List<Transform> context, Life life)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;

        foreach (Transform item in filteredContext)
        {
            float distance = Vector2.Distance(item.position, agent.transform.position);
            float distancePercent = distance / life.neighborRadius;
            float inverseDistancePercent = 1 - distancePercent;
            float weight = inverseDistancePercent / filteredContext.Count;


            Vector2 direction = (item.position - agent.transform.position) * weight;

            move += direction;
        }

        return move;
    }
}
