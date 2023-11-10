using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
        [SerializeField] public List<AudioClip> levelMusicList = new List<AudioClip>();
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource ambientNoise;

        private void Start()
        {
                ambientNoise.Play();
                int randomNumber = Random.Range(0 , levelMusicList.Count);
                audioSource.clip = levelMusicList[randomNumber];
                audioSource.Play();
        }
}
