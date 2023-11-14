using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace natsumon
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform playerPosition;

        private void Awake()
        {
            Debug.Log(playerPosition);
            var current = Keyboard.current;
            Debug.Log(current);
        }
    }
}
