using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(LifeAgent agent, List<Transform> context, Life life);
}
