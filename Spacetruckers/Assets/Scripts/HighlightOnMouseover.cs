using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnMouseover : MonoBehaviour
{
    Outline outline;

    public Color color = Color.white;
    public float width = 2;

    // Start is called before the first frame update
    void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineWidth = width;
        outline.OutlineColor = color;
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

}
