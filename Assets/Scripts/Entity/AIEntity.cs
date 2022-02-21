using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIEntity : BaseEntity
{

    protected bool wantToMove = true;

    public EntityController econtroller;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        econtroller = GetComponent<EntityController>();
        Side = -1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!isMyTurn || isActing)
            return;

        isMyTurn = NextActing();
         
        if (!isMyTurn)
        {
            StartCoroutine(EndTurn());
        }
    }

    public InteractableType interType 
    {
        get 
        {
            return getType(); 
        }
    }


    protected IEnumerator EndTurn()
    {        
        econtroller.Ancoring(true);
        yield return new WaitForSecondsRealtime(0.5f);
        isActive = false;
    }

    public IEnumerator RandomMove(int numTry = 5)
    {
        isActing = true;
        var MaxRange = Mathf.Sqrt(Mathf.Pow(MaxDistance(), 2) / 2);
        Debug.Log("Объект находится в точке " + transform.position);
        Debug.Log("Объект имеет " + currentActionPoint + " AP");
        Debug.Log("Максимальная дистанция " + MaxDistance());
        Debug.Log("Предельное значение x и y в рандоме " + MaxRange);
        bool res;
        int pass = 0;
        var realPoint = new Vector3();
        Vector3 targetPoint;
        do
        {
            pass++;
            targetPoint = new Vector3(gameObject.transform.position.x + Random.Range(-MaxRange, MaxRange), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-MaxRange, MaxRange));
            res = econtroller.MoveIfPossibleLimited(targetPoint, MaxDistance(), out realPoint);
            if (res)
            {
                currentActionPoint -= Vector3.Distance(gameObject.transform.position, realPoint) * MovePerAP();

            }
        }
        while (!res && pass < numTry);
        wantToMove = res;

        if (res)
            Debug.Log("Объект идет в точку " + realPoint);

        while (econtroller.agent.hasPath || econtroller.agent.pathPending)
        {
            yield return null;
        }
        isActing = false;
    }

    public IEnumerator Move(Vector3 targetPoint)
    {
        isActing = true;

        Vector3 realPoint;
        Debug.Log("Объект находится в точке " + transform.position);
        Debug.Log("Объект имеет " + currentActionPoint + " AP");
        Debug.Log("Максимальная дистанция " + MaxDistance());
        if (econtroller.MoveIfPossibleLimited(targetPoint, MaxDistance(), out realPoint))
        {
            currentActionPoint -= Vector3.Distance(gameObject.transform.position, realPoint) * MovePerAP();
            Debug.Log("Объект идет в точку " + realPoint);
        }

        while (econtroller.agent.hasPath || econtroller.agent.pathPending)
        {
            yield return null;
        }
        isActing = false;

    }

    public Vector3 NearestFreeAmongPoints (Vector3[] surrpoints)
    {
        Vector3 result = Vector3.zero;
        float dist = float.PositiveInfinity;
        foreach(var point in surrpoints)
        {
            NavMeshPath currPath = new NavMeshPath();
            if (econtroller.CalculateCompletePath(point, currPath)  && !lcontroller.isObjectHere(point))
            {
                var currDist = econtroller.PathLength(currPath);
                if (currDist < dist)
                {
                    result = point;
                    dist = currDist;
                }
            }
        }
        return result;
    }
    public override void StartTurn()
    {
        base.StartTurn();
        wantToMove = true;
        econtroller.Ancoring(false);
    }

    protected virtual bool ActPrimary()
    {
        return false;
    }

    protected virtual bool ActSecondary()
    {
        return false;
    }

    protected virtual bool ActOther()
    {
        return false;
    }

    private bool NextActing()
    {
        if (ActPrimary())
        {
            return true;
        }

        if (ActSecondary())
        {
            return true;
        }

        return ActOther();
    }
}
