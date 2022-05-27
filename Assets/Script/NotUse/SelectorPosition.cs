using UnityEngine;

public class SelectorPosition : MonoBehaviour
{
    //On déclare nos variables
    [SerializeField] float speed = 2f;
    [SerializeField, Range(0f, 200f)] private float limiteHauteur = 150f;
    [SerializeField, Range(0f, 200f)] private float limiteBas = 150f;
    private Vector3 limiteHauteurPosition;
    private Vector3 limiteBasPosition;
    private Rigidbody2D rb;
    private float direction = 1f;

    //public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        limiteHauteurPosition = transform.position + new Vector3(0, limiteHauteur, 0);
        limiteBasPosition = transform.position - new Vector3(0, limiteBas, 0);
    }


    void Update()
    {
        // Si l'ennemi se coince contre quelque chose (sa vitesse plus petite que 0.1 m/s) alors il se retourne
        if (Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            direction = -direction;
        }

        //Si il dépasse sa limite Droite, il se retourne
        if (transform.position.y > limiteHauteurPosition.y)
        {
            direction = -1f;
        }

        if (transform.position.y < limiteBasPosition.y)
        {
            direction = 1f;
        }

        rb.velocity = new Vector2(rb.velocity.x, direction * speed);
    }
   
    void OnDrawGizmos() //tracés dans l'editor
    {
        if (!Application.IsPlaying(gameObject))
        {
            limiteHauteurPosition = transform.position + new Vector3(0, limiteHauteur, 0);
            limiteBasPosition = transform.position - new Vector3(0, limiteBas, 0);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(limiteHauteurPosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawCube(limiteBasPosition, new Vector3(0.2f, 1, 0.2f));
        Gizmos.DrawLine(limiteHauteurPosition, limiteBasPosition);
    }
}

