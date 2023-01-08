using System;
using DG.Tweening;
using UnityEngine;

namespace GameLogic
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _disappearTime = 1f;
        
        private MeshRenderer _meshRenderer;
        private Sequence _enemySequence;
        private Color _disappearColor = new Color(1f,.5f,.2f);
        private Collider _collider;
        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            
        }

        private void OnEnable()
        {
            if(!_collider)
                _collider = GetComponent<Collider>();
            _collider.isTrigger = false;
        }

        public void Disappear()
        {
            _enemySequence.Kill();
            _enemySequence = DOTween.Sequence();
            _enemySequence.AppendCallback(delegate { _collider.isTrigger = true; });
            _enemySequence.Append(_meshRenderer.material.DOColor(_disappearColor,
                _disappearTime));
            _enemySequence.AppendCallback((delegate { gameObject.SetActive(false); }));

        }
    }
}
