using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostLists : MonoBehaviour
{
        SpriteRenderer sprite;

        [SerializeField] public List<Sprite> sprites = new List<Sprite>();

        [SerializeField] private int currentLevel;

        private void Start()
        {
                sprite = GetComponent<SpriteRenderer>();
                sprite.sprite = sprites[currentLevel -1];
        }
}
