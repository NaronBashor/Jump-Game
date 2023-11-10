using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
        public List<GameObject> characters = new List<GameObject>();

        [SerializeField] private int characterIndex = 0;
        [SerializeField] private Transform spawnPosition;

        public int CharacterIndex { get => characterIndex; set => characterIndex = value; }

        private void Awake()
        {
                GameObject playerChosen = Instantiate(characters[characterIndex] , spawnPosition.position , Quaternion.identity);
                GameObject player = GameObject.Find("Player");
                playerChosen.GetComponentInParent<Transform>().SetParent(player.transform);
                playerChosen.name = "PlayerCharacter";
        }
}
