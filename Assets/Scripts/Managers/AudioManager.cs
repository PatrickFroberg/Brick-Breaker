using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public AudioClip BounceSound;
        public AudioClip StartGameSound;
        public AudioClip BtnSound;
        public AudioClip ErrorSound;
        public AudioClip GameOverSound;

        private AudioSource _audioSource;


        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _audioSource.volume = 0.8f;
            
            DontDestroyOnLoad(gameObject);
        }

        public void PlayBounceSound()
        {
            _audioSource.PlayOneShot(BounceSound);
        }
        public void PlayStartGameSound()
        {
            _audioSource.PlayOneShot(StartGameSound);
        }
        public void PlayBtnSound()
        {
            _audioSource.PlayOneShot(BtnSound);
        }
        public void PlayErrorSound()
        {
            _audioSource.PlayOneShot(ErrorSound);
        }
        public void PlayGameOverSound()
        {
            _audioSource.PlayOneShot(GameOverSound);
        }
    }
}
