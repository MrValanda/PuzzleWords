using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Levels
{
    [Serializable]
    public class LevelModel
    {
        public LevelModel(List<string> words)
        {
            Words = words;
        }

        [field: SerializeField] public List<string> Words { get; private set; }
        [JsonIgnore] public string Separator { get; private set; } = "|";
    }
}