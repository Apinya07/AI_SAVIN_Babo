using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patel", story: "Playone Move [random] [Position] Platfrom", category: "Action", id: "78a4cbf1064600f2f5e6a76232233031")]
public partial class PatelAction : Action
{
    [SerializeReference] public BlackboardVariable<Patal> Random;
    [SerializeReference] public BlackboardVariable<Vector3> Position;
    protected override Status OnStart()
    {
        Position.Value = Random.Value.GetRandomPositionWithinRange();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}



