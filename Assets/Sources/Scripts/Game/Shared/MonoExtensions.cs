using System;
using UnityEngine;

namespace Game.Shared
{
    public static class MonoExtensions
    {
        public static bool IsInterface(this MonoBehaviour monoBehaviour, Type interfaceType)
        {
            return interfaceType.IsInstanceOfType(monoBehaviour);
        }
    }
}