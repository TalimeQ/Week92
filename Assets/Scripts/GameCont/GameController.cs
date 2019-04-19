using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDirection.Game
{
   
    [System.Serializable]
    struct NpcParams
    {
       
        public int npcAmount;
        public float minTimeDelay;
        public float maxTimeDelay;

        public static NpcParams operator+ (NpcParams a, NpcParams b)
        {
            NpcParams npcParams = new NpcParams();
            npcParams.npcAmount = a.npcAmount + b.npcAmount;
            npcParams.minTimeDelay = a.minTimeDelay + b.minTimeDelay;
            if (npcParams.minTimeDelay < 0.1) npcParams.minTimeDelay = 0.1f;
            if (npcParams.maxTimeDelay < 0.2) npcParams.maxTimeDelay = 0.2f;
            npcParams.maxTimeDelay = a.maxTimeDelay + b.maxTimeDelay;
            return npcParams;
        }
    }
    
    enum GameState
    {
        EMainMenu,
        EFinished,
        EPlaying
    }

    public class GameController : MonoBehaviour , ISpawnSignalizer , IDirectionEnterListener
    {
        GameState currentGameState = GameState.EMainMenu;

        [SerializeField]
        List<GameObject> gameUi;
        [SerializeField]
        [Tooltip("Contains list of string which helps players locate to what destination NPC's want to go")]
        List<string> globalDestinationProperties;
        [SerializeField]
        List<Destination> Destinations;
        [SerializeField]
        InGameUi ingameUi;
        [SerializeField]
        PauseMenu pausedMenu;

        [SerializeField]
        NpcParams startingParams;
        [SerializeField]
        NpcParams currentParams;
        [SerializeField]
        NpcParams paramsPerLevel;
        
        


       
        HashSet<string> currentlyUsedProperties = new HashSet<string>();
        Spawner spawner;
        int score = 0;
        int lives = 10;
        int currentLevel = 1;
        int destinationsAmount = 1;
        bool isPaused = false;

        void Start()
        {
            SetupGame();
            ChangeState(GameState.EMainMenu);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(!isPaused)
                { 
                    isPaused = true;
                    pausedMenu.gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                }
                else
                {
                    Unpause();
                }
            }
            
       
        }

        public void Unpause()
        {
            isPaused = false;
            Time.timeScale = 1.0f;
        }

        private void SetupGame()
        {
            spawner = GetComponent<Spawner>();
            spawner.InitSpawner(Destinations);
            foreach (Destination dest in Destinations)
            {
                dest.DirectionEnterListener = this;
            }
            spawner.SpawnSignalizer = this;
        }

        void ChangeState(GameState newState)
        {
            switch(newState)
            {
                case GameState.EMainMenu:
                    ChangeUi(0);
                    break;
                case GameState.EPlaying:
                    GameInit();
                    ChangeUi(1);
                    spawner.StartSpawning(currentParams.npcAmount,currentParams.minTimeDelay,currentParams.maxTimeDelay);
                    break;
                case GameState.EFinished:
                    ChangeUi(2);
                    spawner.Deinit();
                    break;
            }
        }

        

        void ChangeUi(int index)
        {
            foreach(GameObject ui in gameUi)
            {
                ui.SetActive(false);
            }
            gameUi[index].SetActive(true);
        }

        private void NextRound()
        {
            spawner.StartSpawning(currentParams.npcAmount, currentParams.minTimeDelay, currentParams.maxTimeDelay);
        }

        private void AddDestinations(int maxDesinations)
        { 
            int startingAmount = currentlyUsedProperties.Count;
           
            while (currentlyUsedProperties.Count < maxDesinations)
            {
                string propertyToAdd = globalDestinationProperties[Random.Range(0, globalDestinationProperties.Count)];
                currentlyUsedProperties.Add(propertyToAdd);
            }
            List<string> newDirections = new List<string>();
            foreach (string str in currentlyUsedProperties)
            {
                newDirections.Add(str);    
            }
            int j = 0;
            foreach(Destination dest in Destinations)
            {
                dest.AddDestionations(newDirections[startingAmount + j]);
                j++;
            }
            spawner.SetNewSpawnParams(newDirections);
        }

        private void GameInit()
        {
            score = 0;
            destinationsAmount = 8;
            currentLevel = 1;
            ingameUi.UpdateScore(score);
            lives = 10;
            ingameUi.UpdateLives(lives);
            currentParams = startingParams;


            // Clean up so we can rerandomize properties for next game
            currentlyUsedProperties.Clear();
            while (currentlyUsedProperties.Count < 4)
            {
                string propertyToAdd = globalDestinationProperties[Random.Range(0, globalDestinationProperties.Count)];
                currentlyUsedProperties.Add(propertyToAdd);
            }
            List<string> spawnerDestinations = new List<string>();
            int i = 0; 
            foreach(string str in currentlyUsedProperties)
            {
                Destinations[i].ClearDestinations();
                Destinations[i].AddDestionations(str);
                spawnerDestinations.Add(str);
                i++;
            }
            spawner.SetNewSpawnParams(spawnerDestinations);

        }

        public void StartGame()
        {
            ChangeState(GameState.EPlaying);
        }

        public void OnSpawningFinished()
        {
            currentParams  += paramsPerLevel;
            currentLevel++;
            if(currentLevel % 5 == 0)
            {
                if (destinationsAmount <= globalDestinationProperties.Count)
                AddDestinations(destinationsAmount);
               
                destinationsAmount+= 4;
            }
            Invoke("NextRound", 4.0f);
            
  
        }

        public void OnScored(int scoreGiven)
        {
            score += scoreGiven;
            print(" " + scoreGiven);
            ingameUi.UpdateScore(score);
        }

        public void OnFailed()
        {
            
            lives--;
            ingameUi.UpdateLives(lives);
            if(lives <= 0)
            {
                ChangeState(GameState.EFinished);
         
            }
        }

        public void Retry()
        {
            ChangeState(GameState.EFinished);
            ChangeState(GameState.EPlaying);
            Unpause();
        }
    }
    
}