using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDirection.Game { 
    public class Destination : MonoBehaviour
    {
        [SerializeField]
        DestinationText destinationText;
        


        List<string> townProperties = new List<string>();
        IDirectionEnterListener directionEnterListener;
        AudioSource audioSource;

        public IDirectionEnterListener DirectionEnterListener { set { directionEnterListener = value; } }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "NPC")
            {
                NPC npcComp = collision.gameObject.GetComponent<NPC>();
                if (!npcComp.WasSpawned)
                {
                    CheckEnteringNpc(npcComp);
                    collision.gameObject.SetActive(false);
                }

               
            }
            else
            {
                print("RedirectionTrigger :: Unknown entity collided");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "NPC")
            {
   
                collision.gameObject.GetComponent<NPC>().WasSpawned = false;
            }
        }

        private void CheckEnteringNpc(NPC enteringNpc)
        {
            if(townProperties.Contains(enteringNpc.Destination))
            {
                int score = 1;
                audioSource.Play();
                directionEnterListener.OnScored(score);
            }
            else
            {
                directionEnterListener.OnFailed();
            }
        }

        public void ClearDestinations()
        {
            townProperties.Clear();
        }

        public void AddDestionations(string newDestination)
        {
           
            townProperties.Add(newDestination);
            string textToSet = "";
            foreach(string str in townProperties)
            {
                textToSet += str + "\n";
            }
            destinationText.SetText(textToSet);
        }

        public bool containsDestination(string Destination)
        {
            return townProperties.Contains(Destination);
        }
    }
}