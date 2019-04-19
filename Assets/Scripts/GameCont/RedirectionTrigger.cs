using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneDirection.Player;

namespace OneDirection.Game {
public class RedirectionTrigger : MonoBehaviour , IDirectionListener
    {
        [SerializeField]
        Vector2 currentDirection;
        [SerializeField]
        PlayerController ownedController;

      


        void Start()
        {
            if (ownedController)
                ownedController.DirectionTrigger = this;


        }


        public void OnDirectionChanged(Vector2 newDirection)
        {
            currentDirection = newDirection;
            print(newDirection);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag  == "NPC")
            {
                print("Collided!");
                NPC collidingNPC = collision.gameObject.GetComponent<NPC>();
                collidingNPC.ChangeDirection(currentDirection);
            }
            else
            {
                print("RedirectionTrigger :: Unknown entity collided");
            }
        }
    }
}