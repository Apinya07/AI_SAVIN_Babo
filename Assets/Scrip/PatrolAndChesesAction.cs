using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatrolAndCheses", story: "[AI] Patrol random Position To [Cheses] [Player]", category: "Action", id: "22a4be45bc06d9712490ca6101fa6484")]
public partial class PatrolAndChesesAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> AI;
    [SerializeReference] public BlackboardVariable<AI_Cheses> Cheses;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

