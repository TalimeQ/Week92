using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    float baseMovementSpeed = 1f;
    [SerializeField]
    TextMesh npcText;
    [SerializeField]
    MeshRenderer npcTextRenderer;

    Rigidbody2D npcRigidBody;
    Vector2 movementVector = new Vector2(1.0f,0.0f);
    SpriteRenderer spriteRenderer;
    string destination;
    bool wasSpawned = false;
    float movementSpeed = 0.5f;

    // 0 - no difficulties , 1 rotating, 2 mirrored , 3 mirrored and rotationg 4 rotationg and scrambling
    float currentDifficulty = 0.0f;


    public bool WasSpawned {  get { return wasSpawned; } set { wasSpawned = value; } }
    public string Destination { get { return destination; } set { destination = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float BaseMovementSpeed { get { return baseMovementSpeed; } }
    public float CurrentDifficulty { get { return currentDifficulty; } set { currentDifficulty = value; }  }

    IEnumerator ChangeMovementDirection(Vector2 newMovementVector)
    {
        float waitTime = 2.5f;
        if (movementSpeed == baseMovementSpeed) waitTime = 1.0f;
        yield return new  WaitForSeconds(waitTime);
        movementVector = newMovementVector;
        if (newMovementVector.x < 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        npcRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        npcTextRenderer.sortingOrder = 10;
       

    }

    private void Update()
    {
        Vector3 currentRot = npcTextRenderer.gameObject.transform.rotation.eulerAngles;
        currentRot += new Vector3(0, 0, currentDifficulty);
        npcTextRenderer.gameObject.transform.rotation = Quaternion.Euler(currentRot);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveNPC();
    }

    void MoveNPC()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = (Vector2)transform.position + (movementVector * Time.deltaTime * movementSpeed);
        npcRigidBody.MovePosition(newPosition);
    }

    public void ChangeDirection(Vector2 newDirection)
    {
        StartCoroutine(ChangeMovementDirection(newDirection));

    }

    public void SetDiriection(Vector2 newDirection)
    {
        movementVector = newDirection;

    }
    public void UpdateText()
    {
        npcText.text = destination;
    }

}
