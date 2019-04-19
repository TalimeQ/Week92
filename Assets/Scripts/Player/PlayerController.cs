using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneDirection.Game;

namespace OneDirection.Player
{ 
  


    public enum ECurrentDirection
    {
        ECurrentDirNorth,
        ECurrentDirSouth,
        ECurrentDirWest,
        ECurrentDirEast
    }
    public class PlayerController : MonoBehaviour
    {

        // Holds list of sprites which will be representing direction of the sign, could be done with animations yet this is kinda simpler/faster
        [SerializeField]
        private List<Sprite> directionalRepresentation;

        private SpriteRenderer playerSpriteRenderer;
        private ECurrentDirection pointingAt = ECurrentDirection.ECurrentDirSouth;
        private AudioSource audioSource;
        // Assign this
        IDirectionListener directionTrigger;
        public IDirectionListener DirectionTrigger { get { return directionTrigger; } set { directionTrigger = value; } }
       


        // Start is called before the first frame update
        void Start()
        {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {
            ProcessPlayerInput();
        }

        private void ProcessPlayerInput()
        {
            if (Input.GetButtonDown("Up"))
            {
                SwitchState(ECurrentDirection.ECurrentDirNorth);
            }
            else if (Input.GetButtonDown("Down"))
            {
                SwitchState(ECurrentDirection.ECurrentDirSouth);
            }
            else if (Input.GetButtonDown("Right"))
            {
                SwitchState(ECurrentDirection.ECurrentDirEast);
            }
            else if (Input.GetButtonDown("Left"))
            {
                SwitchState(ECurrentDirection.ECurrentDirWest);
            }
        }

        private void SwitchState(ECurrentDirection newState)
        {
            pointingAt = newState;
            audioSource.Play();
            switch(pointingAt)
            {
                case ECurrentDirection.ECurrentDirNorth:
                    playerSpriteRenderer.sprite = directionalRepresentation[0];
                    directionTrigger.OnDirectionChanged(Vector2.up);
                    break;
                case ECurrentDirection.ECurrentDirSouth:
                    playerSpriteRenderer.sprite = directionalRepresentation[1];
                    directionTrigger.OnDirectionChanged(Vector2.down);
                    break;
                case ECurrentDirection.ECurrentDirEast:
                    playerSpriteRenderer.sprite = directionalRepresentation[2];
                    directionTrigger.OnDirectionChanged(Vector2.right);
                    break;
                case ECurrentDirection.ECurrentDirWest:
                    playerSpriteRenderer.sprite = directionalRepresentation[3];
                    directionTrigger.OnDirectionChanged(Vector2.left);
                    break;

            }
           
        }
    }
}