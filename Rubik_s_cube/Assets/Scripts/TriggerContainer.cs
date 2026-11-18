using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerContainer : MonoBehaviour
{
    public List<Transform> cubes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateList();
    }

    // Update is called once per frame
    void Update()
    {
        if(InputSystem.actions.FindAction("Refresh").WasPressedThisFrame())
        {
            UpdateList();
        }
    }
    public void UpdateList()
    {
        cubes.Clear();
        foreach (Collider cube in Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size))
        {
            if(cube.GetComponent<TriggerContainer>() == null && cube.gameObject.layer != LayerMask.NameToLayer("UI"))
            {
                cubes.Add(cube.transform.parent);
            }
        }
    }
}
