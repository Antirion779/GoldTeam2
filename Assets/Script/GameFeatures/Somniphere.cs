using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Somniphere : MonoBehaviour
{
    [Header("Range Options")]
    [SerializeField] private int RangeSize;
    [SerializeField] private int RangeRadius;
    [SerializeField] private Gradient color;
    private bool canShoot;

    private void Start()
    {
        circleRenderer.colorGradient = color;
        circleRenderer.startWidth = RangeSize;
    }

    public LineRenderer circleRenderer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("smoniphere");
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        canShoot = true;
        //player cant move
        DrawCircle(100, RangeRadius);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
    }

    private void DrawCircle(int steps, float radius)
    {
        circleRenderer.positionCount = steps;

        for(int currentSteps = 0; currentSteps<steps; currentSteps++)
        {
            float circumferenceProgress = (float)currentSteps / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius; 

            Vector3 currentPosition = new Vector3(x, y, 0) + transform.position;

            circleRenderer.SetPosition(currentSteps, currentPosition);
        }
    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "Enemy")
        {
            canShoot = false;

            //enemy cant move

            //player can move

            Destroy(gameObject);
        }
    }
}
