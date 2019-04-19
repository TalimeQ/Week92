using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDirection.Game { 
public class Spawner : MonoBehaviour
{
        [SerializeField][Tooltip("Defines objectypes to spawn")]
        List<GameObject> objectToSpawn;

        List<GameObject> pooledObjects = new List<GameObject>();
        List<Destination> destinations = new List<Destination>();
        List<string> npcDestinations = new List<string>();
        int currentLevel = 0;
        ISpawnSignalizer spawnSignalizer;
    
        public ISpawnSignalizer SpawnSignalizer { set { spawnSignalizer = value; } }
        // Start is called before the first frame update
    
        public void StartSpawning(int amount, float minTimeDelay, float maxTimeDelay)
        {
            StartCoroutine(spawnWaves(amount, minTimeDelay, maxTimeDelay));
        }
    
        public void InitSpawner(List<Destination> destinationsToSet)
        {
                destinations = destinationsToSet;
                this.currentLevel = 1;
        }



        public void SetNewSpawnParams(List<string> roundDestinations)
        {
                npcDestinations = roundDestinations;
        }

        IEnumerator spawnWaves(int amount, float minTimeDelay, float maxTimeDelay)
        {
            for(int i = 0; i < amount; i++)
            {
                bool objectPooled = false;
                foreach(GameObject pooledObj in pooledObjects)
                {
                    if (pooledObj.activeInHierarchy == false)
                    {
                        SpawnObject(pooledObj);
                        objectPooled = true;
                        break;
                    }
                }
                if(!objectPooled)
                {
                    GameObject temp = Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Count)]);
                    pooledObjects.Add(temp);
                    SpawnObject(temp);
                }
            
                yield return new WaitForSeconds(Random.Range(minTimeDelay, maxTimeDelay));
            }
            currentLevel++;
            StopCoroutine("spawnWaves");
            spawnSignalizer.OnSpawningFinished();
        }
    
        public void Deinit()
        {
            
            foreach (GameObject obj in pooledObjects)
            {
                obj.SetActive(false);
            }
            StopAllCoroutines();
        }

        private void SpawnObject(GameObject spawnedObject)
        {
            Destination spawnPoint = destinations[Random.Range(0, destinations.Count)];
            spawnedObject.transform.position = spawnPoint.transform.position;
            if (spawnedObject.transform.position.x > 0) spawnedObject.GetComponent<SpriteRenderer>().flipX = true;
            Vector2 movementVec = GetFacingDirection(spawnedObject);
            SetNpcParams(spawnedObject, movementVec, spawnPoint);

        }

        private static Vector2 GetFacingDirection(GameObject spawnedObject)
        {
            Vector2 movementVec = new Vector2(-1 * spawnedObject.transform.position.x, -1  * spawnedObject.transform.position.y);
            movementVec.Normalize();
            return movementVec;
        }

        private void SetNpcParams(GameObject spawnedObject, Vector2 movementVec, Destination spawnPoint)
        {
            NPC npcComponent = spawnedObject.GetComponent<NPC>();
            npcComponent.SetDiriection(movementVec);
            npcComponent.WasSpawned = true;
            if(spawnedObject.transform.position.x == 0)
            {
                npcComponent.MovementSpeed = npcComponent.BaseMovementSpeed / 2;
            }
            else
            {
                npcComponent.MovementSpeed = npcComponent.BaseMovementSpeed;   
            }

            do
            {
                npcComponent.Destination = npcDestinations[Random.Range(0, npcDestinations.Count)];
            }
            while (spawnPoint.containsDestination(npcComponent.Destination));
            npcComponent.UpdateText();
            npcComponent.CurrentDifficulty += 0.1f;
            spawnedObject.SetActive(true);
        }
    }
}