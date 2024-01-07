using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EntityController : MonoBehaviour
{
    public InterfaceController icontroller;
    public NavMeshAgent agent;
    public NavMeshModifierVolume meshModifier;
    public NavMeshSurface meshSurface;
    public Animator animator;
    public Vector3 EndPathPoint;
    public bool showPath;
    public float pathLength;
    protected LineRenderer pathDrawer;
    public ShowEntityInfo objectInfo;
    private BaseEntity entity;
    public GameObject rightHandHandler;
    public PrefabsController prefabsController;
    public GameObject Hitbox;
    public event Action OnEndMoving;
    private bool isMoving;

    public void PlaceToRightHand(GameObject thing)
    {
        if(thing is null)
        {
            Debug.LogError("������� �����������");
        }
        animator.SetBool("HaveKnife", entity.RightHandItem is BaseWeapon && ((BaseWeapon)entity.RightHandItem)?.type == WeaponType.Knife);
        thing.GetComponent<Rigidbody>().isKinematic = true;
        thing.GetComponent<BoxCollider>().enabled = false;           
        thing.transform.SetParent(rightHandHandler.transform);
        thing.transform.localPosition = Vector3.zero;
        thing.transform.localRotation = Quaternion.Euler(Vector3.zero);

        if(entity.RightHandItem is BaseWeapon && ((BaseWeapon)entity.RightHandItem)?.type == WeaponType.SMG)
        {
            SetAsSMG(thing);
            animator.SetBool("HaveGun", true);
        }

        if (entity.RightHandItem is BaseWeapon && ((BaseWeapon)entity.RightHandItem)?.type == WeaponType.Pistol)
        {
            SetAsPistol(thing);
            animator.SetBool("HavePistol", true);
        }

        icontroller.rightHandDropButton.gameObject.SetActive(true);
        thing.SetActive(true);            
    }

    private void SetAsSMG(GameObject item)
    {
        item.transform.localRotation = Quaternion.Euler(new Vector3(-33, 11, 93));
    }

    private void SetAsPistol(GameObject item)
    {
        item.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }

    public void ShootAtPoint(Transform spawnPoint, Vector3 point, RangedAttackModifier  weaponModifier, AmmoData ammo, BaseEntity target)
    {
        GameObject bullet = Instantiate(prefabsController.simpleBullet, null);
        bullet.transform.position = spawnPoint.position;
        bullet.GetComponent<Bullet>().Shoot(point, weaponModifier, ammo, target);
        entity.audioSource.PlayOneShot(ammo.shotSound);
    }

    public bool RemoveFromRightHand(bool drop)
    {
        animator.SetBool("HaveKnife", false);
        animator.SetBool("HaveGun", false);
        animator.SetBool("HavePistol", false);
        icontroller.rightHandDropButton.gameObject.SetActive(false);
        var oldobject = entity.RightHandItem?.gameObject;
        if (oldobject != null)
        {            
            if (drop)
            {
                oldobject.GetComponent<TacticalItem>().Drop();
            }
            else
            {               
                oldobject.transform.SetParent(null);
                oldobject.SetActive(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }



    // Start is called before the first frame update
    protected virtual void Start()
    {
        entity = gameObject.GetComponent<BaseEntity>();
        pathDrawer = GetComponent<LineRenderer>();

        pathDrawer.startWidth = 0.1f;
        pathDrawer.endWidth = 0.02f;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (!entity.lcontroller.Entities.Contains(entity))
            return;
        if (isMoving && !agent.hasPath)
            OnEndMoving?.Invoke();
        isMoving = agent.hasPath;
        animator.SetBool("IsWalking", isMoving);
        pathDrawer.enabled = isMoving;
        if (entity.CurrentHealth <= 0)
        {
            entity.lcontroller.RemoveEntity(entity);
            entity.econtroller.Ancoring(false);
            if (entity.lcontroller.AllEnemiesAreDead())
            {
                icontroller.OnAllEnemyDead();
            }
            animator.SetTrigger("Die");
        }
    }

    private void DrawPath(NavMeshPath path)
    {
        pathDrawer.positionCount = path.corners.Length;
        for (var i = 0; i < pathDrawer.positionCount; i++)
        {
            pathDrawer.SetPosition(i, path.corners[i]);
        }
    }

    public bool PathReachable(NavMeshPath path)
    {
        return PathLength(path) <= entity.MaxDistance();
    }

    public float PathLength(NavMeshPath path)
    {
        float result = 0;
        for (int i = 1; i < path.corners.Length; i++)
        {
            result += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
        return result;
    }

    public Vector3 allignetPointIn(Vector3 sourcePoint)
    {
        var result = sourcePoint;
        if (agent.transform.position.x < result.x)
            result.x = Mathf.FloorToInt(result.x);
        else
            result.x = Mathf.CeilToInt(result.x);
        if (agent.transform.position.z < result.z)
            result.z = Mathf.FloorToInt(result.z);
        else
            result.z = Mathf.CeilToInt(result.z);
        return result;
    }

    public Vector3 allignetPointMid(Vector3 sourcePoint)
    {
        var result = sourcePoint;
        result.x = Mathf.Round(result.x);
        result.z = Mathf.Round(result.z);
        return result;
    }

    public void Ancoring(bool f)
    {
        agent.enabled = false;
        meshModifier.enabled = false;
        Hitbox.SetActive(false);
        var movePointerActive = icontroller.movePointer.gameObject.activeSelf;
        icontroller.movePointer.gameObject.SetActive(false);
        if (f)
        {
            meshModifier.enabled = true;
            meshSurface.BuildNavMesh();
            Hitbox.SetActive(true);
        }
        else
        {            
            meshSurface.BuildNavMesh();
            agent.enabled = true;
            Hitbox.SetActive(true);
        }
        icontroller.movePointer.gameObject.SetActive(movePointerActive);
    }

    public bool MoveIfPossibleLimited(Vector3 targetPoint, float maxDistance, out Vector3 realPoint)
    {
        var correctedPoint = allignetPointIn(targetPoint);
        NavMeshPath path = new NavMeshPath();
        if (CalculateCompletePath(correctedPoint, path))
        {
            Debug.Log("������� ����� " + targetPoint + '(' + correctedPoint + ')');
            Debug.Log("���������� �� ����� " + PathLength(path));

            if (path.corners.Length > 1 && PathLength(path) > maxDistance)
            {
                Debug.Log("�������������� ���� ");
                var i = 0;
                float length = 0;
                do
                {
                    i++;
                    length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                }
                while (length <= maxDistance);

                var lastLineLength = Vector3.Distance(path.corners[i - 1], path.corners[i]);  //����� ���������� ������� (�� ������� � ��������� ����������)
                var exceedLength = length - maxDistance;                                      // ���������� ����� �� ��������� �������
                var f = 1 - exceedLength / lastLineLength;                                  // �� ������� ��������� ����� ��������� ��������� �������

                var middlePoint = Vector3.Lerp(path.corners[i - 1], path.corners[i], f);      //�����, �� ������� ����� ���������� ����� ���������� �� ����


                correctedPoint = allignetPointMid(middlePoint);
                if (CalculateCompletePath(correctedPoint, path))
                {
                    Debug.Log("����������������� ����� " + middlePoint + '(' + correctedPoint + ')');
                    Debug.Log("���������� �� ����������������� ����� " + PathLength(path));
                }
                else
                {
                    Debug.LogError("������ ������������� ����. ����� " + middlePoint + " �����������.");
                    realPoint = Vector3.zero;
                    return false;
                }

            }
            agent.SetPath(path);
            EndPathPoint = agent.pathEndPosition;
            if (showPath)
                DrawPath(path);
            pathLength = PathLength(path);
            realPoint = correctedPoint;
            return true;
        }
        realPoint = Vector3.zero;
        return false;
    }

    public bool MoveIfPossible(Vector3 targetPoint, bool checkReachable = true)
    { 
        var correctedPoint = allignetPointMid(targetPoint);
        NavMeshPath path = new NavMeshPath();
        if (CalculateCompletePath(correctedPoint, path) && (PathReachable(path) || !checkReachable))
         {
            agent.SetPath(path);
            EndPathPoint = agent.pathEndPosition;
            if (showPath)
                DrawPath(path);
            pathLength = PathLength(path);
            return true;
        }
        else
            return false;
    }

    public bool CalculateCompletePath(Vector3 targetPoint, NavMeshPath path)
    {      
        var result = agent.CalculatePath(targetPoint, path);
        if (!result)
            return false;
        var endPoint = path.corners[path.corners.Length - 1];
        return Mathf.Abs(endPoint.x - targetPoint.x) < 0.001 && Mathf.Abs(endPoint.z - targetPoint.z) < 0.001;
    }

    void OnMouseEnter()
    {        
        if (icontroller.UIInact || !entity.lcontroller.isActiveAndEnabled || icontroller.playerController.character.isActing)
        {
            return;
        }
        if (!(entity is  null))
            objectInfo.ShowInfo(entity);
       if (entity is IInteractable)
        {
            icontroller.movePointer.SetActive(true);
            icontroller.movePointer.position = entity.transform.position;
            if (((IInteractable)entity).getType() == InteractableType.Enemy)
            {
                icontroller.movePointer.SetPointerType(PointerType.Target);
            }           
        }
    }

    public float DistanceTo(IInteractable obj)
    {
        return Vector3.Distance(obj.GetPosition(), gameObject.transform.position);
    }

    private void OnMouseExit()
    {
        objectInfo.HideInfo();
        icontroller.movePointer.SetPointerType(PointerType.Nav);
    }

    private void OnMouseDown()
    {
        if (icontroller.UIInact)
            return;
        icontroller.playerController.SelectObject(entity);
    }

    public void TurnFor(float angle)
    {
        StartCoroutine(Turn(angle));
    }

    public void TurnLeft()
    {
        TurnFor(-90);
    }

    public void TurnRight()
    {
        TurnFor(90);
    }

    public void TurnAround()
    {
        TurnFor(180);
    }

    public void TurnToObject(GameObject obj)
    {
        var targetDir = obj.transform.position - transform.position;
        var angle = Vector3.Angle(transform.forward, targetDir);
        var cross = Vector3.Cross(transform.forward, targetDir);
        if (cross.y < 0) // ��������� ���� y ���������� ���������� ������������
        {
            angle = -angle; // ���� ���� �������������, ����������� ����
        }
        TurnFor(angle);
    }


    public void TurnToObject(MonoBehaviour mono)
    {
        TurnToObject(mono.gameObject);
    }

    public void TurnToObject(Transform trans)
    {
        TurnToObject(trans.gameObject);
    }

    private IEnumerator Turn(float angle)
    {
        var angleTurned = 0;
        var turnSpeed = 8;
        if (angle < 0)
            turnSpeed *= -1;
        do
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.Rotate(new Vector3(0, turnSpeed, 0));
            angleTurned += turnSpeed;
        } while (Math.Abs(angleTurned) < Math.Abs(angle));
    }
}
