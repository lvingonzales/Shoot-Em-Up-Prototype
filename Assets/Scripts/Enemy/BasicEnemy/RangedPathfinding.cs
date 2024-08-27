using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedPathfinding : MonoBehaviour
{
    // Bezier Curve Route Variables
    public Transform route;

    private int routeToGo;
    private float tParam;
    private Vector2 enemyPosition;

    // Move Speed Modifier
    private float speedModifier;

    // Coroutine Condition
    bool coroutineAllowed;

    // Waypoint System Variables
    bool isAtRouteStart = false;
    float minDistance = 0.1f;

    // Attack System Variables
    [SerializeField] private RangedBehaviour rangedBehaviour;

    private void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.25f;
        coroutineAllowed = true;
        rangedBehaviour = GetComponent<RangedBehaviour>();
    }

    private void Update()
    {
        if (coroutineAllowed) StartCoroutine(TraverseRoute(routeToGo));
    }

    private IEnumerator TraverseRoute (int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = route.GetChild(0).position;
        Vector2 p1 = route.GetChild(1).position;
        Vector2 p2 = route.GetChild(2).position;
        Vector2 p3 = route.GetChild(3).position;
        
        while (!isAtRouteStart)
        {
            tParam = Time.deltaTime * speedModifier;

            float distance = Vector2.Distance(transform.position, p0);
            //Debug.Log("Distance: " + distance);
            transform.position = Vector2.MoveTowards(transform.position, p0, tParam*25);
            if (distance < minDistance)
            {
                tParam = 0;
                isAtRouteStart = true;
            }
            yield return new WaitForEndOfFrame();
        }

        while (rangedBehaviour.enemyAmmoCount > 0)
        {
            rangedBehaviour.Shoot();
            yield return new WaitForSeconds(.25f);
        }

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            enemyPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = enemyPosition;
            yield return new WaitForEndOfFrame();
        }
        ResetPath();
    }

    public void ResetPath()
    {
        if (tParam >= 1f) gameObject.SetActive(false);

        tParam = 0f;
        isAtRouteStart = false;
        coroutineAllowed = true;
    }
    
}
