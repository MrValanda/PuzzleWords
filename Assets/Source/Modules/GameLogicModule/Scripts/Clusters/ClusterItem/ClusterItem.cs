using System;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Clusters.ClusterItem
{
    [Serializable]
    public class ClusterItem<T>
    {
        public ClusterItem(T value)
        {
            Value = value;
        }

        [field: SerializeField]
        public T Value { get; private set; }
    }
}