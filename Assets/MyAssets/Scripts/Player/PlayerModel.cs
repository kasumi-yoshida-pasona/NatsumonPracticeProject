using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace natsumon
{
    public class PlayerModel : MonoBehaviour
    {
        private ReactiveProperty<Vector3> playerPosition = new ReactiveProperty<Vector3>();
        public IReadOnlyReactiveProperty<Vector3> PlayerPosition { get { return playerPosition; } }


        void Awake()
        {
            playerPosition.Value = new Vector3(500, 15, 500);
        }
    }
}
