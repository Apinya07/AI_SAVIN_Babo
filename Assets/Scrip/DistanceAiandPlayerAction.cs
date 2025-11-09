using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DistanceAiandPlayer", story: "Distantce between [Ai] [DistanceToPlayer] [Player]", category: "Action", id: "5154ffb2a036d7cc92856080d402859d")]
public partial class DistanceAiandPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Ai;
    [SerializeReference] public BlackboardVariable<float> DistanceToPlayer;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
        if (Ai.Value == null || Player.Value == null)
        {
            Debug.LogWarning("AI or Player reference is missing");
            return Status.Failure;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Ai.Value == null || Player.Value == null)
            return Status.Failure;

        float distance = Vector3.Distance(Ai.Value.transform.position, Player.Value.transform.position);
        DistanceToPlayer.Value = distance;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


