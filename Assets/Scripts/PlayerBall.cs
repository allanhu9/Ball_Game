using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    private bool activated = true;
    private bool aiming = false;
    private bool moving = false;
    private bool tallying = false;
    private GameManager.Colour colour;
    private Queue<Collider2D> touching = new Queue<Collider2D>();
    private GameManager gameManager;
    [SerializeField] private AimingReticle reticle;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float decceleration = 1f;
    private const float bottomBorder = -4.5f;
    private const float rightBorder = 8.39f;
    private const float leftBorder = -8.39f;
    private const float topBorder = 3.52f;

    private void Start()
    {
        velocity = new Vector3(0, 0, 0);
        gameManager = GameManager.GetGameManager();
        colour = gameManager.GetPlayerColour();
        gameManager.UpdateMoves();
        ColourSelf();
    }

    private void Update()
    {
        //Debug.Log("Before Decelerate: " + velocity.magnitude);
        checkBorders();
        decelerate();
        //Debug.Log("After Decelerate: " + velocity.magnitude);
        transform.position += velocity * Time.deltaTime;
        if (velocity.magnitude == 0 && moving)
        {
            moving = false;
            CheckMoves();
            TallyPoints();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("entered");
        touching.Enqueue(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!tallying)
            touching.Dequeue();
    }
    private void checkBorders()
    {
        if (transform.position.x + velocity.x * Time.deltaTime >= rightBorder)
        {
            transform.position = new Vector3(rightBorder, transform.position.y, 0);
            velocity = Vector3.zero;
        }
        if (transform.position.x + velocity.x * Time.deltaTime <= leftBorder)
        {
            transform.position = new Vector3(leftBorder, transform.position.y, 0);
            velocity = Vector3.zero;
        }
        if (transform.position.y + velocity.y * Time.deltaTime >= topBorder)
        {
            transform.position = new Vector3(transform.position.x, topBorder, 0);
            velocity = Vector3.zero;
        }
        if (transform.position.y + velocity.y * Time.deltaTime <= bottomBorder)
        {
            transform.position = new Vector3(transform.position.x, bottomBorder, 0);
            velocity = Vector3.zero;
        }
    }

    private void decelerate()
    {
        if (velocity.magnitude - decceleration * Time.deltaTime <= 0)
        {
            //Debug.Log("Speed set to zero");
            velocity = Vector3.zero;
        }
        else if (velocity.magnitude != 0)
        {
            velocity = velocity / velocity.magnitude * (velocity.magnitude - (decceleration * Time.deltaTime));
        }
    }

    private void OnMouseDown()
    {
        if (velocity.magnitude == 0 && activated)
        {
            aiming = true;
            reticle.Aiming();
        }

    }

    private void OnMouseUp()
    {
        if (velocity.magnitude == 0 && activated && aiming == true)
        {
            reticle.Released();
            aiming = false;
            moving = true;
            gameManager.UpdateMoves();
        }
    }

    private void CheckMoves()
    {
        if (gameManager.turns == 0)
        {
            gameManager.EndGame();
        }
    }

    private void ColourSelf()
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

    private void TallyPoints()
    {
        if (touching.Count > 0)
        {
            tallying = true;
            while (touching.Count > 0)
            {
                Collider2D collider = touching.Dequeue();
                if (collider.gameObject.GetComponent<Ball>().SameColour(colour))
                {
                    gameManager.GainScore();
                }
                else
                {
                    gameManager.LoseScore();
                }
                collider.gameObject.GetComponent<Ball>().Collect();
            }
            tallying = false;
        }
    }

    public void Move(Vector3 relative)
    {
        velocity = relative * maxSpeed / reticle.maxDragMagnitude; // max drag distance = max speed allowed
        gameManager.turns--;
    }

    public void disable()
    {
        activated = false;
    }

    public void enable()
    {
        activated = true;
    }
}
