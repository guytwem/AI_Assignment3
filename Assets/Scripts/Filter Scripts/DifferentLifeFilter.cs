using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Life/Filter/Different Life")]
public class DifferentLifeFilter : ContextFilter
{
    public override List<Transform> Filter(LifeAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            LifeAgent itemAgent = item.GetComponent<LifeAgent>();

            if (itemAgent != null)
            {
                if (itemAgent.AgentLife != agent.AgentLife)
                {
                    filtered.Add(item);
                }
            }
        }
        return filtered;
    }
}
