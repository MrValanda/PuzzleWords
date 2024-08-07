using UnityEngine;
using UnityEngine.UI;

namespace Source.Modules.GameLogicModule.Scripts.Words.WordCells
{
    public class WordCellView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void Active()
        {
            _image.color = Color.green;
        }

        public void DeActive()
        {
            _image.color = Color.white;
        }
    }
}