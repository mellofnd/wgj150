using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : Singleton<FlockManager>
{
    [Header ("Flock Properties")]
    [Tooltip ("Number of agents in the scene")] 
    public int boidNum = 100;
    [SerializeField] private float boundSize = 5;

    [Tooltip ("Refresh rate in which the boids get the target current position")]
    [SerializeField] private float goalRefreshRate = 5;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject boidPrefab = null;
    
    [Space]
    public GameObject[] boids;

    public Vector3 GoalPos { get => goalPos; set => goalPos = value; }
    public float BoundSize { get => boundSize; set => boundSize = value; }

    private Vector3 goalPos;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        boids = new GameObject[boidNum];
        for (int i = 0; i < boidNum; i++)
        {
            GameObject currentBoid = (GameObject) Instantiate (boidPrefab, new Vector3 (
                Random.Range (-BoundSize, BoundSize),
                Random.Range (-BoundSize, BoundSize),
                Random.Range (-BoundSize, BoundSize)
            ), Random.rotation, transform);
            boids[i] = currentBoid;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // timer+= Time.deltaTime;
        // if (timer >= goalRefreshRate) {
        //     goalRefreshRate = Random.Range (1, 5);
        //     timer = 0;
        // }
        
        GoalPos = target.position;
    }
}
