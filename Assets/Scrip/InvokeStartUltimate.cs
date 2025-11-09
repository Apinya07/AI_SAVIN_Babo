using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/InvokeStartUltimate")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "InvokeStartUltimate", message: "Annie OnStart Invoke Untimate", category: "Events", id: "294cea9347379e1a7f55ef3300aab17e")]
public sealed partial class InvokeStartUltimate : EventChannel { }

