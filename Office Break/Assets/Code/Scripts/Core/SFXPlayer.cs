using System.Collections.Generic;
using UnityEngine;

namespace OfficeBreak.Core 
{ 
    public class SFXPlayer
    {
        private const float MIN_PITCH = 0.8f;
        private const float MAX_PITCH = 1.2f;

        private AudioSource _audioSource;
        private Dictionary<string, AudioClip[]> _clips = new Dictionary<string, AudioClip[]>();

        public SFXPlayer(AudioSource audioSource) => _audioSource = audioSource;

        public void AddClips(string key, AudioClip[] clips) => _clips.Add(key, clips);

        public void AddClip(string key, AudioClip clip)
        {
            AudioClip[] clipArray = new AudioClip[1];
            clipArray[0] = clip;
            _clips.Add(key, clipArray);
        }

        public void Play(string key)
        {
            _audioSource.clip = _clips[key][Random.Range(0, _clips[key].Length)];
            _audioSource.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
            _audioSource.Play();
        }
    }
}