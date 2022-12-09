using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellHandler : MonoBehaviour
{
    public Transform spellIkPoint;
    InputAction actionMousePosition;
    InputAction actionSpell;
    public Camera mainCamera;

    // Update is called once per frame
    void Start()
    {
        actionMousePosition = GetComponent<PlayerInput>().actions["Mouse Position"];
        actionSpell = GetComponent<PlayerInput>().actions["Spell"];
    }

    // Update is called once per frame
    void Update()
    {
        if (actionSpell.IsPressed())
        {
            Vector3 pos = mainCamera.ScreenToWorldPoint(actionMousePosition.ReadValue<Vector2>());
            pos.z = 0;

            spellIkPoint.transform.position = pos;
        }
    }
}
