using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MovetoAIannie", story: "[Character] [Moveto] [AIannie] for Comebine", category: "Action", id: "ba113feab41f9fa8e9338c6f73d61d65")]
public partial class MovetoAIannieAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Character;
    [SerializeReference] public BlackboardVariable<Vector3> Moveto;
    [SerializeReference] public BlackboardVariable<GameObject> AIannie;

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

