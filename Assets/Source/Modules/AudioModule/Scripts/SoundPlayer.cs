using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Modules.GameLogicModule.Scripts
{
    public class SoundPlayer
    {
        private readonly Dictionary<SoundType, AudioClip> _audioClips;
        private readonly AudioSource _audioSourcePrefab;
        private readonly List<AudioSource> _poolAudioSource = new();
        private bool _canPlaySound = true;

        public SoundPlayer(Dictionary<SoundType, AudioClip> audioClips,AudioSource audioSourcePrefab)
        {
            _audioClips = audioClips;
            _audioSourcePrefab = audioSourcePrefab;
        }

        public void ActiveSound()
        {
            _canPlaySound = true;
        }

        public void DeActiveSound()
        {
            _canPlaySound = false;
        }

        public void PlaySound(SoundType soundType,float volume = 1f)
        {
            if(_canPlaySound == false) return;
            
            if (_audioClips.TryGetValue(soundType, out AudioClip clip))
            {
                AudioSource freeAudioSource = _poolAudioSource.FirstOrDefault(x => x.isPlaying == false);
                if (freeAudioSource == null)
                {
                    AudioSource audioSource = Object.Instantiate(_audioSourcePrefab);
                    Object.DontDestroyOnLoad(audioSource.gameObject);
                    audioSource.PlayOneShot(clip, volume);
                    _poolAudioSource.Add(audioSource);
                }
                else
                {
                    freeAudioSource.PlayOneShot(clip, volume);
                }
            }
        }
    }
}