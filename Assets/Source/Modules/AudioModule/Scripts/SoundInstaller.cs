using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Source.Modules.GameLogicModule.Scripts
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializeField] private AudioClip _setClusterInCellSound;
        [SerializeField] private AudioSource _audioSourcePrefab;
       
        public override void InstallBindings()
        {
            Dictionary<SoundType, AudioClip> audioClips = new Dictionary<SoundType, AudioClip>()
            {
                { SoundType.SetClusterInCellSound, _setClusterInCellSound }
            };
            
            SoundPlayer soundPlayer = new(audioClips, _audioSourcePrefab);
            Container.Bind<SoundPlayer>().FromInstance(soundPlayer).AsSingle();
        }
    }
}