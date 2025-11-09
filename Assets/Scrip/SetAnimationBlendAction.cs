using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetAnimationBlend", story: "Set [float] to blendcontrol by lerping", category: "Action", id: "680fb9e3c3ffdeeda7c53197a6b61024")]
public partial class SetAnimationBlendAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Float;
    [SerializeReference] public BlackboardVariable<AnimatorController> blendControl;
   
    protected override Status OnStart()
    {
        blendControl.Value.SetTargetValue(Float.Value);
        return Status.Success;
    }


}


