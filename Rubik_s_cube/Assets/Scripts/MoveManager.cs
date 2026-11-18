using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
struct TuplePerso<T,R>
{
    public T first;
    public R second;
}

public class MoveManager : MonoBehaviour
{

    InputAction IAReverse;

    [SerializeField] float rotationSpeed = 1f;

    [SerializeField] List<TuplePerso<InputActionReference,TriggerContainer>> moves;

    [SerializeField] GameObject cube;


    InputAction IAX;
    InputAction IAY;
    InputAction IAZ;


    public bool canRotate = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        IAX = InputSystem.actions.FindActionMap("Move").FindAction("X");
        IAY = InputSystem.actions.FindActionMap("Move").FindAction("Y");
        IAZ = InputSystem.actions.FindActionMap("Move").FindAction("Z");

        IAReverse = InputSystem.actions.FindActionMap("Move").FindAction("Reverse");
        UpdateLists();
    }

    void UpdateLists()
    {
        foreach (TuplePerso<InputActionReference,TriggerContainer> trigger in moves)
        {
            trigger.second.UpdateList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canRotate)
        {
            return;
        }

        int reverse = IAReverse.IsPressed() ? -1 : 1;

        foreach (TuplePerso<InputActionReference, TriggerContainer> trigger in moves)
        {
            if(trigger.first.action.WasPerformedThisFrame())
            {
                Vector3 axis = trigger.second.GetComponent<BoxCollider>().size;
                axis = new Vector3(
                    (axis.x <= 0.5 ? 1 : 0) * trigger.second.transform.localScale.x, 
                    (axis.y <= 0.5 ? 1 : 0) * trigger.second.transform.localScale.x,
                    (axis.z <= 0.5 ? 1 : 0) * trigger.second.transform.localScale.x);
                StartCoroutine(RotateLayer(trigger.second, axis*reverse));
                return;
            }
        }

        if (IAX.WasPressedThisFrame())
        {
            StartCoroutine(RotateCube(new Vector3(1, 0, 0)*reverse));
            return;
        }
        else if (IAY.WasPressedThisFrame())
        {
            StartCoroutine(RotateCube(new Vector3(0, 1, 0) * reverse));
            return;
        }
        else if (IAZ.WasPressedThisFrame())
        {
            StartCoroutine(RotateCube(new Vector3(0, 0, 1) * reverse));
            return;
        }
    }

    IEnumerator RotateLayer(TriggerContainer container, Vector3 axis)
    {
        canRotate = false;
        float rotation = 0;
        float frameRotation = 0;
        while (rotation < 90)
        {
            frameRotation = 90 / (rotationSpeed * 90);
            rotation += frameRotation;
            foreach(Transform cube in container.cubes)
            {
                cube.transform.Rotate(axis, frameRotation, Space.World);
            }
            yield return null;
        }

        frameRotation = rotation - 90f;
        rotation -= frameRotation;
        foreach (Transform cube in container.cubes)
        {
            cube.transform.Rotate(axis, -frameRotation, Space.World);
        }

        yield return new WaitForFixedUpdate();
        UpdateLists();
        canRotate = true;
        yield return null;
    }

    IEnumerator RotateCube(Vector3 axis)
    {
        float rotation = 0;
        float frameRotation = 0;
        canRotate = false;
        while (rotation < 90)
        {
            frameRotation = 90 / (rotationSpeed * 90);
            rotation += frameRotation;
            cube.transform.Rotate(axis, frameRotation, Space.World);
            yield return null;
        }

        frameRotation = rotation - 90f;
        rotation -= frameRotation;
        cube.transform.Rotate(axis, -frameRotation, Space.World);

        yield return new WaitForFixedUpdate();
        UpdateLists();
        canRotate = true;
        yield return null;

    }
}
