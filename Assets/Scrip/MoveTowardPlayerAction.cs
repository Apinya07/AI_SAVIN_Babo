using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveTowardPlayer", story: "[MoveSpeed] [AI] quickly toward the [Player] within [AttackRange]", category: "Action", id: "f6474c2f2a84b1d102abef9fa2fc74e2")]
public partial class MoveTowardPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    [SerializeReference] public BlackboardVariable<GameObject> AI;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<float> AttackRange;

    protected override Status OnStart()
    {
        if (AI.Value == null || Player.Value == null)
            return Status.Failure;

        Vector3 direction = (Player.Value.transform.position - AI.Value.transform.position).normalized;
        float distance = Vector3.Distance(AI.Value.transform.position, Player.Value.transform.position);

        // เคลื่อนไปหาผู้เล่นถ้าอยู่ในระยะโจมตี
        if (distance <= AttackRange.Value)
        {
            AI.Value.transform.position += direction * MoveSpeed.Value * Time.deltaTime;
        }

        return Status.Success;
    }
}


