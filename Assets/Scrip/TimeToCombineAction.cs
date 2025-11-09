using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TimeToCombine", story: "AI [Wait] Time For CombinePose", category: "Action", id: "daec30ec627154b52aa556908d491d23")]
public partial class TimeToCombineAction : Action
{
    [SerializeReference] public BlackboardVariable<IncreaseTimeSinceCombine> Wait;

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

