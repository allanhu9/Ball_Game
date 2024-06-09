using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingReticle : MonoBehaviour
{

    [SerializeField] private PlayerBall playerBall;
    private Vector3 start;
    private Vector3 end;
    private Vector3 relative;
    [SerializeField] public float maxDragMagnitude = 2.5f;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        end = Camera.main.ScreenToWorldPoint(mousePos);
        end.z = 0;
        relative = Vector3.ClampMagnitude(end - start, maxDragMagnitude);
        transform.position = start + relative;
        rotate();
    }

    private void rotate()
    {
        float rotationAngle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }
    public void Aiming()
    {
        gameObject.SetActive(true);
        start = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void Released()
    {
        transform.position = start;
        playerBall.Move(relative);
        //Debug.Log("Relative Magn: " + relative.magnitude);
        gameObject.SetActive(false);
        //Debug.Log("released");

    }
}
