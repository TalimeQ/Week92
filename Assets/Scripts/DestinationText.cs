using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationText : MonoBehaviour
{
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        textMesh = GetComponent<TextMesh>();
        meshRenderer.sortingOrder = 10;
        textMesh.text = "";

    }

    public void SetText(string newText)
    {
        textMesh.text = newText;
    }
     
    public void AddText(string addedText)
    {
        textMesh.text = textMesh.text + "/n" + addedText;
    }
      
}
