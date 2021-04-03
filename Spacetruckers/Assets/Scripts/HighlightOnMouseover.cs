using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnMouseover : MonoBehaviour
{
    Outline outline;

    public Color color = Color.white;
    public float width = 2;

    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineWidth = width;
        outline.OutlineColor = color;
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.enabled = false;
    }

    public void SetSelected(bool selected)
    {
        this.isSelected = selected;

        outline.enabled = selected;        
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        if (!this.isSelected)
        {
            outline.enabled = false;
        }
    }


}
