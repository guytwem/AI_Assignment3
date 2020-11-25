using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Life/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : LifeBehavior
{

    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private float radius = 15;
    public override Vector2 CalculateMove(LifeAgent agent, List<Transform> context, Life life)
    {
        //direction to the center
        //magnitude will = distance to the center
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;

        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
