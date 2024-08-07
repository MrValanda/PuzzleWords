using Source.Modules.GameLogicModule.Scripts.Clusters.ClusterItem;

namespace Source.Modules.GameLogicModule.Scripts.Words.WordCells
{
    public class WordCellModel
    {
        public ClusterItem<char> WorldCellItem { get; private set; }
        public int CellIndex{ get; private set; }

        public void SetCellIndex(int index)
        {
            CellIndex = index;
        }
        
        public void UpdateCellItem(ClusterItem<char> wordCellItem)
        {
            WorldCellItem = wordCellItem;
        }
    }
}