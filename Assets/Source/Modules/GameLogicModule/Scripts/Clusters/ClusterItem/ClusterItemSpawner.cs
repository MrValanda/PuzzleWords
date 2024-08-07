using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Clusters.ClusterItem
{
    public class ClusterItemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private ClusterItemCompositeRoot _clusterItemCompositeRoot;

        public void SpawnClusterItems(Cluster<char> cluster)
        {
            foreach (var clusterClusterItem in cluster.ClusterItems)
            {
                ClusterItemCompositeRoot clusterItemCompositeRoot = Instantiate(_clusterItemCompositeRoot, _parent);
                clusterItemCompositeRoot.Init(clusterClusterItem);
            }
        }
    }
}