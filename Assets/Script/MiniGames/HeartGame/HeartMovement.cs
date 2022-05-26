using UnityEngine;

public class HeartMovement : MonoBehaviour
{
    [SerializeField] float _speed = 20f;
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = -transform.up * _speed; //on fait juste avancer nos papillions vers la droite a une certaine vitesse
    }

}
