using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Life/Behavior/Composite")]
public class CompositeBehavior : LifeBehavior
{
    [System.Serializable]
    public struct BehaviorGroup
    {
        public LifeBehavior behaviors;
        public float weights;
    }

    public BehaviorGroup[] behaviors;

    public override Vector2 CalculateMove(LifeAgent agent, List<Transform> context, Life life)
    {
        Vector2 move = Vector2.zero;

        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].behaviors.CalculateMove(agent, context, life) * behaviors[i].weights;

            if (partialMove != Vector2.zero)
            {
                //make sure the number we get for moving the agent isnt larger than the weight we gave it
                if (partialMove.sqrMagnitude > behaviors[i].weights * behaviors[i].weights)
                {
                    partialMove.Normalize();
                    partialMove *= behaviors[i].weights;
                }
                //bring all the behaviors together as one
                move += partialMove;
            }
        }

        return move;
    }
}
