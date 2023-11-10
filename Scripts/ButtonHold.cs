using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
        [SerializeField] private string button;
        [SerializeField] private float moveSpeed;

        private bool held;

        public void OnPointerDown(PointerEventData eventData)
        {
                held = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
                held = false;
        }

        private void Update()
        {
                if (held)
                {
                        if (button == "Left")
                        {
                                GameObject player = GameObject.FindGameObjectWithTag("Player");
                                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * moveSpeed , 0));
                        }
                        else if (button == "Right")
                        {
                                GameObject player = GameObject.FindGameObjectWithTag("Player");
                                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * moveSpeed , 0));
                        }
                }
        }
}
