using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] GameObject player;
    [SerializeField] Grid grid;
    [SerializeField] GameObject temporaryCube;

    [Header("Oil Options")]

    [SerializeField, Range(-30, 30)] int moveOnX = 0;
    [SerializeField, Range(-30, 30)] int moveOnY = 0;
    public int numberOfOil = 2;

    private enum PlatformAxis
    {
        HORIZONTAL, 
        VERTICAL
    }

    [SerializeField] private PlatformAxis platformAxis;

    private Vector3 startOil;
    private Vector3 endOil;

    private void Awake()
    {
        if(player == null || grid == null || numberOfOil == 0)
            Debug.Log("<color=gray>[</color><color=#FF00FF>Oil</color><color=gray>]</color><color=red> ATTENTION </color><color=#F48FB1> Some object are null </color><color=gray>-</color><color=cyan> Object Name : </color><color=yellow>" + transform.name + "</color><color=cyan> Player : </color><color=yellow>" + player + "</color><color=cyan> Grid : </color><color=yellow>" + grid + "</color><color=cyan> Number of Oil : </color><color=yellow>" + numberOfOil + "</color>");
        else
        {
            startOil = new Vector3(moveOnX * grid.cellSize.x + grid.cellSize.x / 2, moveOnY * grid.cellSize.y + grid.cellSize.y / 2, 0);
            Init();
        }

    }


    public void Init()
    {
        if (platformAxis == PlatformAxis.HORIZONTAL)
        {
            for (int i = 0; i < numberOfOil; i++)
            {
                GameObject cube = Instantiate(temporaryCube, new Vector3(startOil.x + i * grid.cellSize.x, startOil.y), Quaternion.identity);
                GameManager.Instance.OilCaseList.Add(cube);
            }
        }
        else
        {
            for (int i = 0; i < numberOfOil; i++)
            {
               GameObject cube = Instantiate(temporaryCube, new Vector3(startOil.x, startOil.y + i * grid.cellSize.x), Quaternion.identity);
               GameManager.Instance.OilCaseList.Add(cube);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            startOil = new Vector3(moveOnX * grid.cellSize.x + grid.cellSize.x / 2, moveOnY * grid.cellSize.y + grid.cellSize.y / 2, 0);

            // Draw a semitransparent yellow cube at the transforms position
            Gizmos.color = new Color(1, 1, 0, 0.5f);

            if (platformAxis == PlatformAxis.HORIZONTAL)
            {
                for (int i = 0; i < numberOfOil; i++)
                {
                    Gizmos.DrawCube(new Vector3(startOil.x + i * grid.cellSize.x, startOil.y), new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                }
            }
            else
            {
                for (int i = 0; i < numberOfOil; i++)
                {
                    Gizmos.DrawCube(new Vector3(startOil.x, startOil.y + i * grid.cellSize.x), new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                }
            }
        }
    }
}
