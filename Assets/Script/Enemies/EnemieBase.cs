using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class EnemieBase : MonoBehaviour
{

    [Header("Stats")] 
    [SerializeField][Tooltip("MoveDistance du GameManager * rangeVision = ennemy range")][Range(1,10)] private int rangeVision;
    [SerializeField][Tooltip("MoveDistance du GameManager * moveDistance = ennemy move distance")][Range(1, 10)] private int moveDistance;

    [Header("Patern")] 
    [SerializeField] [Tooltip("Choose between inverse and loop")] private bool hasLoopMouvement;
    [SerializeField][Tooltip("N/S/E/W -> direction + TR/TL -> rotate")] private string[] patern;
    [SerializeField] private string[] invertPatern;

    [SerializeField] private int paternNumber = 0;
    private bool paternIncrease = true;
    private Vector3 endPos;

    void Start()
    {
       invertPatern = InvertPatern(patern);
       endPos = transform.position;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * (rangeVision + (GameManager.Instance.GetMoveDistance / 2)) , Color.red);
        transform.position = Vector3.MoveTowards(transform.position, endPos, GameManager.Instance.GetMoveSpeed * Time.deltaTime);

    }

    public void Action()
    {
        Mouve();
    }

    void Mouve()
    {
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
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;

            case "S":
                endPos = new Vector2(transform.position.x, transform.position.y - GameManager.Instance.GetMoveDistance * moveDistance);
                transform.eulerAngles = new Vector3(0, 0, -90);
                break;

            case "E":
                endPos = new Vector2(transform.position.x + GameManager.Instance.GetMoveDistance * moveDistance, transform.position.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case "W":
                endPos = new Vector2(transform.position.x - GameManager.Instance.GetMoveDistance * moveDistance, transform.position.y);
                transform.eulerAngles = new Vector3(0, 180, 00);
                break;
            case "TR":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
                break;
            case "TL":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);
                break;
        }
    }

    public void CheckForPlayer()
    {
        //Debug.Log("ça va check");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), rangeVision + (GameManager.Instance.GetMoveDistance / 2));

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
}


[CustomEditor(typeof(EnemieBase))]
public class Car_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemieBase _script = (EnemieBase) target;
        if (GUILayout.Button("Play a Turn"))
        {
            _script.Action();
        }
    }
}
