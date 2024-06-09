using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public GameManager.Colour colour = GameManager.Colour.blue;
    void Awake()
    {
        ColourSelf();
    }

    public bool SameColour(GameManager.Colour colour)
    {
        return this.colour == colour;
    }

    public void Collect() {
        // FX
        Destroy(gameObject);
    }

    public void ColourSelf()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (colour == GameManager.Colour.blue)
        {
            spriteRenderer.color = Color.blue;
        }
        else if (colour == GameManager.Colour.red)
        {
            spriteRenderer.color = Color.red;
        }
        else if (colour == GameManager.Colour.yellow)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (colour == GameManager.Colour.cyan)
        {
            spriteRenderer.color = Color.cyan;
        }
        else if (colour == GameManager.Colour.green)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.magenta;
        }
    }
}
