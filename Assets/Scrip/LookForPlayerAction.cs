using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Look for player", story: "Character [Look] for Player", category: "Action", id: "43b44c444fdb3dd8ecde6c66ec864997")]
public partial class LookForPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<Cheses> Look;
    [SerializeReference] public BlackboardVariable<bool> Checkplayer;
    protected override Status OnStart()
    {
        Checkplayer.Value = Look.Value.EnableToChase;
        return Status.Running;
    }


}


