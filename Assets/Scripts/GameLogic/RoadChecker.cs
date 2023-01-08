using System;
using UnityEngine;

namespace GameLogic
{
    public class RoadChecker : MonoBehaviour
    {
        private int _counter;
        private void Start()
        {
            
            _counter = 0;
        }

        private void OnEnable()
        {
            RoadChangeAction.onChange += ChangeRoad;
        }

        private void OnDisable()
        {
            RoadChangeAction.onChange -= ChangeRoad;
        }

        private void ChangeRoad(float value)
        {
            transform.localScale = new Vector3(value,
                transform.localScale.y,
                transform.localScale.z);
        }


        private void OnCollisionEnter(Collision collision)
        {
            _counter++;
        }

        private void OnCollisionExit(Collision collision)
        {
            _counter--;
            if (_counter <= 1)
            {
                print("win win");
                PlayerBallAction.onFreePath?.Invoke();
            }
        }
    }
}
