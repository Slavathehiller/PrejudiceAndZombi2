using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScenarioController : MonoBehaviour
{
    [SerializeField] protected Dialog _dialog;
    [SerializeField] protected LifeController _lifeController;
    [SerializeField] protected InterfaceController _interfaceController;
    [SerializeField] protected PrefabsController prefabsController;
    [SerializeField] protected GameObject _cameraContainer;



    protected void WaitAndGo(float delay, Action action = null)
    {
        StartCoroutine(waitAndGo(delay, action));
    }

    private IEnumerator waitAndGo(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
