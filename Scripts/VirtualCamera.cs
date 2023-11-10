using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
        [SerializeField] private CinemachineVirtualCamera cam;
        GameObject player;

        private void Start()
        {
                player = GameObject.Find("PlayerCharacter");
                cam.Follow = player.transform;
        }
}
