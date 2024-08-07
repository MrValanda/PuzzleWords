using TMPro;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Clusters
{
    public class ClusterItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetClusterItemValue(char value)
        {
            _text.text = value.ToString();
        }
    }
}
