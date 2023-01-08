using System;
using DG.Tweening;
using UnityEngine;

namespace GameLogic.Animations
{
    public class DoorAnimation : MonoBehaviour
    {
        
        private Sequence _doorSequence;
        private bool _isOpen;

        private void OnEnable()
        {
            _isOpen = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isOpen) return;
            if (other.GetComponent<BulletBall>().GetIsMain())
            {
                OpenDoor();
                GameScenario.Instance.WinState();
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }

        private void OpenDoor()
        {
            _doorSequence.Kill();
            _doorSequence = DOTween.Sequence();
            _doorSequence.Append(transform.DOLocalRotate(new Vector3(0, -90, 0), 1f)
                .From(transform.localEulerAngles));
            _doorSequence.Join(transform.DOLocalMove(new Vector3(-2.5f, 2.6f, 13.7f), 1f)
                .From(new Vector3(.8f, 2.6f, 13.7f)));
            _isOpen = true;

        }
    }
}
