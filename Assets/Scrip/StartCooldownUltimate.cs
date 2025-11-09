using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StartCooldownUltimate")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StartCooldownUltimate", message: "Start Cooldown Ultimate", category: "Events", id: "e3234a41eed59ab7eb564c5d121ab4a7")]
public sealed partial class StartCooldownUltimate : EventChannel { }

