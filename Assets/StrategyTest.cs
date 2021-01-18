using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
public class StrategyTest : MonoBehaviour
{
    [SerializeField] [Tooltip("Flag(where to go)")] GameObject _target;    
    [SerializeField] [Tooltip("Material for LineRenderer")] Material mat;

    Queue<Transform> _pointsQueue = new Queue<Transform>(); //Queue of moving

    AICharacterControl _agent;
    private Color c1 = Color.green;
    private Color c2 = Color.red;
    private int lenght = 1;
    private LineRenderer lineRenderer;

    void Start()
    {
        var tempLineRenderer = new GameObject("LineRenderer");
        lineRenderer = tempLineRenderer.AddComponent<LineRenderer>();

        lineRenderer.material = mat;
        lineRenderer.SetColors(c1, c2);
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = lenght;

        _agent = GetComponent<AICharacterControl>();

        lineRenderer.SetPosition(0, _agent.transform.position);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _pointsQueue.Enqueue(DrowedPoint(hit.point));   //Adding one move point to queue

                _agent.SetTarget(_pointsQueue.Peek());  //Set aget's "move-to" the first from queue
            }
        }

        //Keep telling agetn to move to points from queue till queue is empty
        if (_pointsQueue.Count == 0)
            return;
        else if (!_pointsQueue.Peek())
            _pointsQueue.Dequeue();
        else
            _agent.SetTarget(_pointsQueue.Peek());

        RaycastHit hitInfo;

        //Drawing the line where agent'll go
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            lineRenderer.SetPosition(0, hitInfo.point);
        }
    }

    /// <summary>
    /// Drawing "move-to" flag
    /// </summary>
    /// <param name="point">Position of the flag</param>
    /// <returns></returns>
    private Transform DrowedPoint(Vector3 point)
    {
        var temPoint = Instantiate(_target, point, Quaternion.identity);

        lineRenderer.SetPosition(0, temPoint.transform.position);

        return temPoint.transform;
    }
}