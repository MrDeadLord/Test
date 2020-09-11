using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class StrategyTest : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private AICharacterControl _agent;
    [SerializeField] private Material mat;

    private Queue<Transform> _pointsQueue = new Queue<Transform>();

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

        lineRenderer.SetPosition(0, _agent.transform.position);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _pointsQueue.Enqueue(DrowedPoint(hit.point));

                _agent.SetTarget(_pointsQueue.Peek());
            }
        }

        if (_pointsQueue.Count == 0)
            return;
        else if (!_pointsQueue.Peek())
            _pointsQueue.Dequeue();
        else
            _agent.SetTarget(_pointsQueue.Peek());

        RaycastHit hitInfo;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        {
            lineRenderer.SetPosition(0, hitInfo.point);
        }
    }

    private Transform DrowedPoint(Vector3 point)
    {
        var temPoint = Instantiate(_target, point, Quaternion.identity);

        lineRenderer.SetPosition(0, temPoint.transform.position);

        return temPoint.transform;
    }
}
