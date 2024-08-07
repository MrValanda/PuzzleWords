using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Clusters
{
    public class ClusterModel
    {
        public Cluster<char> Cluster { get; }
        private readonly RectTransform _rectTransform;

        public ClusterModel(Cluster<char> cluster, RectTransform rectTransform)
        {
            Cluster = cluster;
            _rectTransform = rectTransform;
        }

        public void UpdatePosition(Vector3 position)
        {
            _rectTransform.position = position;
        }

        public void MoveCluster(Vector2 direction)
        {
            _rectTransform.anchoredPosition += direction;
        }

        public void SetParent(Transform parent)
        {
            _rectTransform.SetParent(parent);
        }
    }
}