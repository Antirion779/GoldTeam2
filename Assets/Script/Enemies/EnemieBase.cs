using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class EnemieBase : MonoBehaviour
{
    //champs de vision clean
    //peut bouger quand game state = enemie move et le repasse en player move après

    //Orienter le joueur vers son prochain mouvement
    //Detection à la fin du déplacement
    //Dectection bien placé dès le début -> faire avec les anim je pense 

    [Header("Stats")] 
    [SerializeField][Tooltip("MoveDistance du GameManager * rangeVision = ennemy range")][Range(1,10)] private float rangeVision;
    [SerializeField][Tooltip("MoveDistance du GameManager * moveDistance = ennemy move distance")][Range(1, 10)] private int moveDistance;

    [Header("Patern")] 
    [SerializeField][Tooltip("Choose between inverse and loop")] private bool hasLoopMouvement;
    [SerializeField][Tooltip("To know where you are in the patrol")] private int paternNumber = 0;
    [SerializeField] [Tooltip("Play one times before the patrol loop /// don't use the Element 0")] private string[] prePartern;
    [SerializeField][Tooltip("N/S/E/W -> direction + TR/TL -> rotate")] private string[] patern;
    [SerializeField] private string[] invertPatern;

    [Header("Vision")] 
    [SerializeField] private GameObject vision;
    public enum visionOrientation { West, Est, North, South }
    [SerializeField][Tooltip("Setup the direct he facing at the start")]private visionOrientation orientation;
    private Vector3 visionDir;

    private bool paternIncrease = true;
    private Vector3 endPos;
    private bool isInMovement = false;
    private bool hasPlayed = false;

    void Start()
    {
       invertPatern = InvertPatern(patern);
       endPos = transform.position;
       SetupOrientVision(orientation);
       hasPlayed = false;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, visionDir * (GameManager.Instance.GetMoveDistance * rangeVision) , Color.red);
        transform.position = Vector3.MoveTowards(transform.position, endPos, GameManager.Instance.GetMoveSpeed * Time.deltaTime);
        CheckForPlayer();
        
        if (Vector3.Distance(transform.position, endPos) < 0.02f)
        {
            isInMovement = false;
        }

        if (!isInMovement && hasPlayed)
        {
            Debug.Log("Fin du tour des méchants");
            GameManager.Instance.ActualGameState = GameManager.GameState.PlayerMove;
            hasPlayed = false;
        }
    }

    public void Action()
    {
        Move();
    }

    void Move()
    {
        if (prePartern.Length > 0)
        {
            MakeAMove(prePartern, paternNumber);

            if (paternNumber < prePartern.Length - 1 && paternIncrease)
                paternNumber++;
            else
            {
                paternNumber = 0;
                prePartern = new string[0];
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
                TurnPlayer(_patern, _paternNumber);
                break;
            case "S":
                endPos = new Vector2(transform.position.x, transform.position.y - GameManager.Instance.GetMoveDistance * moveDistance);
                TurnPlayer(_patern, _paternNumber);

                break;
            case "E":
                endPos = new Vector2(transform.position.x + GameManager.Instance.GetMoveDistance * moveDistance, transform.position.y);
                TurnPlayer(_patern, _paternNumber);
                break;
            case "W":
                endPos = new Vector2(transform.position.x - GameManager.Instance.GetMoveDistance * moveDistance, transform.position.y);
                TurnPlayer(_patern, _paternNumber);
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
    void TurnPlayer(string[] _patern, int _paternNumber)
    {
        if (_paternNumber + 1 < _patern.Length)
        {
            switch (_patern[_paternNumber + 1])
            {
                case "N":
                    visionDir = transform.TransformDirection(Vector3.up);
                    vision.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case "S":
                    visionDir = transform.TransformDirection(Vector3.down);
                    vision.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case "E":
                    visionDir = transform.TransformDirection(Vector3.right);
                    vision.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case "W":
                    visionDir = transform.TransformDirection(Vector3.left);
                    vision.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
            }
        }

        else
        {
            switch (_patern[0])
            {
                case "N":
                    visionDir = transform.TransformDirection(Vector3.up);
                    vision.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case "S":
                    visionDir = transform.TransformDirection(Vector3.down);
                    vision.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case "E":
                    visionDir = transform.TransformDirection(Vector3.right);
                    vision.transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case "W":
                    visionDir = transform.TransformDirection(Vector3.left);
                    vision.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
            }
        }
    }

    public void CheckForPlayer()
    {
        //Debug.Log("ça va check");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, visionDir, GameManager.Instance.GetMoveDistance * rangeVision);

        if (hit && hit.transform.tag == "Player")
        {
            //Fonction fin de partie
            Debug.Log("PLAYER !!!!!!!!");
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
                vision.transform.eulerAngles = new Vector3(0, 0, 90);
                return;
            case visionOrientation.South:
                vision.transform.eulerAngles = new Vector3(0, 0, 270);
                return;
            case visionOrientation.Est:
                vision.transform.eulerAngles = new Vector3(0, 0, 0);
                return;
            case visionOrientation.West:
                vision.transform.eulerAngles = new Vector3(0, 0, 180);
                return;
        }
    }
}