using System.Collections.Generic;
using System.IO;
using System.Linq;
using Source.Modules.GameLogicModule.Scripts.Clusters;
using Source.Modules.GameLogicModule.Scripts.Words;
using UnityEngine;
using Zenject;

namespace Source.Modules.GameLogicModule.Scripts.Levels
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private ClusterSpawner _clusterSpawner;
        [SerializeField] private WordsSpawner _wordsSpawner;
        [SerializeField] private WordsHandler _wordsHandler;
        [SerializeField] private Canvas _canvas;

        private LevelData _levelData;

        public override void InstallBindings()
        {
            List<LevelModel> levelData = new List<LevelModel>
            {
                new LevelModel(new List<string>()
                {
                    "КЛ|АС|ТЕР",
                    "КЛАС|ТЕР",
                    "КЛ|АСТ|ЕР",
                }),
            };
        
            string path = Path.Combine(Application.persistentDataPath, "data.json");
            
            _levelData = new ();
            List<LevelModel> levelModels = new LevelModelReader(levelData).ReadData(path);
            _levelData.SetWords(GenerateWords(levelModels[0]));
            Container.Bind<ClusterSpawner>().FromInstance(_clusterSpawner).AsSingle();
            Container.Bind<WordsSpawner>().FromInstance(_wordsSpawner).AsSingle();
            Container.Bind<WordsHandler>().FromInstance(_wordsHandler).AsSingle();
            Container.Bind<LevelData>().FromInstance(_levelData).AsSingle();
            Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
        }

        public override void Start()
        {
            _clusterSpawner.SpawnClusters(_levelData.Words);
            List<WordController> wordControllers = _wordsSpawner.SpawnWords(_levelData.Words).Select(x => x.WordController).ToList();
            _wordsHandler.Initialize(wordControllers);
        }

        private List<Word> GenerateWords(LevelModel levelModel)
        {
            List<Word> result = new();
            foreach (var levelModelWord in levelModel.Words)
            {
                List<Cluster<char>> clusters = GetWord(levelModelWord, levelModel.Separator);
                result.Add(new Word(clusters));
            }
         

            return result;
        }
        
        public List<Cluster<char>> GetWord(string word,string separator)
        {
            List<Cluster<char>> tempWordClusters = new List<Cluster<char>>();
            foreach (var clusterData in word.Split(separator))
            {
                Cluster<char> cluster = new Cluster<char>();
                foreach (var value in clusterData)
                {
                    cluster.AddItem(value);
                }
                tempWordClusters.Add(cluster);
            }
            
            return tempWordClusters;
        }
    }
}