using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tag
{
    public GameObject textObj = new GameObject("TextMeshPro 3D Text");
    private TextMeshPro textMeshPro;

    public Tag(string number, Vector3 position)
    {
        this.textObj.transform.position = position;
        this.textMeshPro = textObj.AddComponent<TextMeshPro>();
        this.textMeshPro.text = number;
        this.textMeshPro.fontSize = 0.5f;
        this.textMeshPro.alignment = TextAlignmentOptions.Center;
        this.textMeshPro.color = Color.black;
    }
}
