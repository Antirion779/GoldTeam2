using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemieBase : MonoBehaviour
{
    //champs de vision clean

    //rotation a bien faire après la fin du déplacement
    //peut bouger quand game state = enemie move et le repasse en player move après
    //Orienter le joueur vers son prochain mouvement
    //Detection à la fin du déplacement
    //Dectection bien placé dès le début -> faire avec les anim je pense 

    [Header("Stats")] 
    [SerializeField][Tooltip("MoveDistance du GameManager * rangeVision = ennemy range")][Range(1,100)] private float rangeVision;
    [SerializeField][Tooltip("MoveDistance du GameManager * moveDistance = ennemy move distance")][Range(1, 10)] private int moveDistance;

    [Header("Patern")] 
    [SerializeField][Tooltip("Choose between inverse and loop")] private bool hasLoopMouvement;
    [SerializeField][Tooltip("To know where you are in the patrol")] private int paternNumber = 0;
    [SerializeField] [Tooltip("Play one times before the patrol loop /// don't use the Element 0")] private string[] prepatern;
    [Tooltip("N/S/E/W -> direction + TR/TL -> rotate")] public string[] patern;
    [SerializeField] private string[] invertPatern;

    [Header("Vision")] 
    [SerializeField] private GameObject vision;
    private string nextorientation;
    public enum visionOrientation { North, South, Est, West }
    [SerializeField][Tooltip("Setup the direct he facing at the start")]private visionOrientation orientation;
    private Vector3 visionDir;

    [Header("Condition")]
    public bool isStunt;
    private bool paternIncrease = true;
    private Vector3 endPos;
    private bool isInMovement;
    private bool hasPlayed;
    
    [Header("Sprite")] 
    [SerializeField] 
    [Tooltip("N/S/E/W")] private GameObject[] enemies;
    
    void OnEnable()
    {
       invertPatern = InvertPatern(patern);
       endPos = transform.position;
       SetupOrientVision(orientation);
       hasPlayed = false;


       vision.transform.localScale = new Vector3((0.055f + rangeVision  * 0.1f), vision.transform.localScale.y);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, visionDir * (GameManager.Instance.GetMoveDistance * rangeVision) , Color.red);
        transform.position = Vector3.MoveTowards(transform.position, endPos, GameManager.Instance.GetMoveSpeed * Time.deltaTime);
        CheckForPlayer();
        
        if (Vector3.Distance(transform.position, endPos) < 0.02f)
        {
            isInMovement = false;
            TurnPlayer(nextorientation);
        }

        if (!isInMovement && hasPlayed && transform.position == endPos)
        {
            GameManager.Instance.EnemyEndMovement();
            hasPlayed = false;
        }
    }

    public void Action()
    {
        if (!isStunt)
        {
            Move();
        }
        else
        {
            isStunt = false;
        }
    }

    void Move()
    {
        if (prepatern.Length > 0)
        {
            MakeAMove(prepatern, paternNumber);

            if (paternNumber < prepatern.Length - 1 && paternIncrease)
                paternNumber++;
            else
            {
                paternNumber = 0;
                prepatern = new string[0];
                return;
            }
            CheckForPlayer();
            return;
        }

        //Debug.Log("MOVE BITCH GET OUT THE WAY !");
        if (paternIncrease)
        {
            MakeAMove(patern, paternNumber);

            if (paternNumber < patern.Length - 1 && paternIncrease)
                paternNumber++;
            else if (paternNumber < patern.Length && hasLoopMouvement)
            {
                paternNumber = 0;
                CheckForPlayer();
                return;
            }
            else
                paternIncrease = false;

            CheckForPlayer();
            return;
        }

        if (!paternIncrease)
        {
            MakeAMove(invertPatern, paternNumber);

            if (paternNumber > 0 && !paternIncrease)
                paternNumber--;
            else
                paternIncrease = true;
            CheckForPlayer();
        }
    }


    void MakeAMove(string[] _patern, int _paternNumber)
    {
        switch (_patern[_paternNumber])
        {
            case "N":
                endPos = new Vector2(transform.position.x, transform.position.y + GameManager.Instance.GetMoveDistance * moveDistance);
                nextorientation = GiveNextOrientation(_patern, _paternNumber);
                break;
            case "S":
                endPos = new Vector2(transform.position.x, transform.position.y - GameManager.Instance.GetMoveDistance * moveDistance);
                nextorientation = GiveNextOrientation(_patern, _paternNumber);

                break;
            case "E":
                endPos = new Vector2(transform.position.x + GameManager.Instance.GetMoveDistance * moveDistance, transform.position.y);
                nextorientation = GiveNextOrientation(_patern, _paternNumber);
                break;
            case "W":
                endPos = new Vector2(transform.position.x - GameManager.Instance.GetMoveDistance * moveDistance, transform.position.y);
                nextorientation = GiveNextOrientation(_patern, _paternNumber);
                break;

            case "TR":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);

                break;
            case "TL":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);
                break;
        }

        isInMovement = true;
        hasPlayed = true;
    }
    string GiveNextOrientation(string[] _patern, int _paternNumber)
    {
        if (_paternNumber + 1 < _patern.Length && paternIncrease)
        {
            switch (_patern[_paternNumber + 1])
            {
                case "N":
                    return "N";
                case "S":
                    return "S";
                case "E":
                    return "E";
                case "W":
                    return "W";
            }
        }

        if (_paternNumber - 1 > 0 && !paternIncrease)
        {
            switch (_patern[_paternNumber - 1])
            {
                case "N":
                    return "N";
                case "S":
                    return "S";
                case "E":
                    return "E";
                case "W":
                    return "W";
            }
        }

        else
        {
            switch (_patern[0])
            {
                case "N":
                    return "N";
                case "S":
                    return "S";
                case "E":
                    return "E";
                case "W":
                    return "W";
            }
        }

        return "S";
    }
    void TurnPlayer(string _orientation)
    {
        switch (_orientation)
        {
            case "N":
                visionDir = transform.TransformDirection(Vector3.up);
                vision.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case "S":
                visionDir = transform.TransformDirection(Vector3.down);
                vision.transform.eulerAngles = new Vector3(0, 0, -90);
                break;
            case "E":
                visionDir = transform.TransformDirection(Vector3.right);
                vision.transform.eulerAngles = new Vector3(0, 0, 0);
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                break;
            case "W":
                visionDir = transform.TransformDirection(Vector3.left);
                vision.transform.eulerAngles = new Vector3(0, 0, 180);
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                break;
        }
    }

    public void CheckForPlayer()
    {
        //Debug.Log("ça va check");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, visionDir, GameManager.Instance.GetMoveDistance * rangeVision);

        if (hit && hit.transform.tag == "Player")
        {
            GameManager.Instance.EndGame();
        }
    }

    string[] InvertPatern(string[] _patern)
    {
        string[] _invertPatern = new string[_patern.Length];
        for (int i = 0; i < patern.Length; i++)
        {
            if (_patern[i] == "N")
                _invertPatern[i] = "S";

            if (_patern[i] == "S")
                _invertPatern[i] = "N";

            if (_patern[i] == "E")
                _invertPatern[i] = "W";

            if (_patern[i] == "W")
                _invertPatern[i] = "E";

            if (_patern[i] == "TR")
                _invertPatern[i] = "TL";
            if (_patern[i] == "TL")
                _invertPatern[i] = "TR";
        }
        return _invertPatern;
    }

    void SetupOrientVision(visionOrientation _visionOrientation)
    {
        switch (_visionOrientation)
        {
            case visionOrientation.North:
                visionDir = transform.TransformDirection(Vector3.up);
                vision.transform.eulerAngles = new Vector3(0, 0, 90);
                return;
            case visionOrientation.South:
                visionDir = transform.TransformDirection(Vector3.down);
                vision.transform.eulerAngles = new Vector3(0, 0, 270);
                return;
            case visionOrientation.Est:
                visionDir = transform.TransformDirection(Vector3.right);
                vision.transform.eulerAngles = new Vector3(0, 0, 0);
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                return;
            case visionOrientation.West:
                visionDir = transform.TransformDirection(Vector3.left);
                vision.transform.eulerAngles = new Vector3(0, 0, 180);
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                return;
        }
    }
}