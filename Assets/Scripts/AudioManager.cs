using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [System.Serializable]
    public struct Music
    {
        public string name;
        public AudioClip audioClip;
    }
    [SerializeField] private Music[] _musics;
    private AudioSource _audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(string name)
    {
        foreach (var music in _musics)
        {
            if (music.name == name)
            {
                AudioClip clip = music.audioClip;
                _audioSource.pitch = Random.Range(0.7f, 1.3f);
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}
