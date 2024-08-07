using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Source.Modules.GameLogicModule.Scripts.Clusters;

namespace Source.Modules.GameLogicModule.Scripts.Words
{
    public class Word : IEquatable<Word>
    {
        public Word()
        {
            WordClusters = new();
        }
        public Word(List<Cluster<char>> clusters)
        {
            WordClusters = clusters;
        }

        public List<Cluster<char>> WordClusters { get; private set; }
        public int Count => WordClusters.Sum(x => x.ClusterItems.Count);
        public void AddCluster(Cluster<char> cluster)
        {
            WordClusters.Add(cluster);
        }
        
        public bool Equals(Word other)
        {
            return other != null && ToString().Equals(other.ToString());
        }

        public override string ToString()
        {
            StringBuilder result = new();
            foreach (var wordCluster in WordClusters)
            {
                foreach (var wordClusterClusterItem in wordCluster.ClusterItems)
                {
                    result.Append(wordClusterClusterItem.Value);
                }
            }

            return result.ToString();
        }
    }
}