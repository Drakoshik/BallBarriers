using System;
using GameLogic.Pool;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLogic
{
    public class PlayerBall : MonoBehaviour
    {
        [SerializeField] private BulletBall _bulletPrefab;

        [SerializeField] private Transform _spawnPosition;

        [SerializeField] private Transform _endPosition;

        private BulletBall _currentBulletBall;
        private ObjectPool<BulletBall> _bulletPool;

        private float _sizeMultiplier;
        private bool _canTouch;

        private void Start()
        {
            _bulletPool ??= new ObjectPool<BulletBall>(_bulletPrefab, 7, true);
            _sizeMultiplier = transform.localScale.x / 100;
            _canTouch = true;
            
        }

        private void OnEnable()
        {
            PlayerBallAction.onFreePath += LastShot;
        }

        private void OnDisable()
        {
            PlayerBallAction.onFreePath -= LastShot;
        }


        private void Update()
        {
            if(!_canTouch) return;
            OnInput();
        }

        private void FixedUpdate()
        {
            if(!_canTouch) return;
            IncreaseBall();
        }


        private void OnInput()
        {
            if(!_canTouch) return;
            if (CreateBall()) return;

            ShotBall();
        }

        private void ShotBall()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (transform.localScale.x <= _sizeMultiplier * 10)
                {
                    var currentBulletBallScale = _currentBulletBall.transform.localScale;
                    currentBulletBallScale = new Vector3(
                        currentBulletBallScale.x + transform.localScale.x,
                        currentBulletBallScale.y + transform.localScale.x,
                        currentBulletBallScale.z + transform.localScale.x);
                    _currentBulletBall.transform.localScale = currentBulletBallScale;
                    transform.localScale = Vector3.zero;
                    _canTouch = false;
                    GameScenario.Instance.FailState();
                    return;
                }

                _currentBulletBall.Shot();
            }
        }

        private void IncreaseBall()
        {
            if (Input.GetMouseButton(0))
            {
                if (transform.localScale.x <= _sizeMultiplier * 10)
                {
                    return;
                }

                if (_currentBulletBall == null) return;
                if (transform.localScale == Vector3.zero) return;
                var playerBallScale = transform.localScale;
                playerBallScale = new Vector3(
                    playerBallScale.x - _sizeMultiplier * .5f,
                    playerBallScale.y - _sizeMultiplier * .5f,
                    playerBallScale.z - _sizeMultiplier * .5f);
                transform.localScale = playerBallScale;

                var currentBulletBallScale = _currentBulletBall.transform.localScale;
                currentBulletBallScale = new Vector3(
                    currentBulletBallScale.x + _sizeMultiplier * .5f,
                    currentBulletBallScale.y + _sizeMultiplier * .5f,
                    currentBulletBallScale.z + _sizeMultiplier * .5f);
                _currentBulletBall.transform.localScale = currentBulletBallScale;

                RoadChangeAction.onChange?.Invoke(playerBallScale.x);
            }
            
        }

        private bool CreateBall()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (transform.localScale.x <= _sizeMultiplier * 10)
                {
                    return true;
                }

                _currentBulletBall = _bulletPool.GetFreeElement();
                _currentBulletBall.transform.position = _spawnPosition.transform.position;

                var playerBallScale = transform.localScale;
                playerBallScale = new Vector3(
                    playerBallScale.x - _sizeMultiplier * 10f,
                    playerBallScale.y - _sizeMultiplier * 10f,
                    playerBallScale.z - _sizeMultiplier * 10f);
                transform.localScale = playerBallScale;

                _currentBulletBall.transform.localScale = new Vector3(
                    _sizeMultiplier * 10,
                    _sizeMultiplier * 10,
                    _sizeMultiplier * 10);
            }

            return false;
        }

        private void LastShot()
        {
            if(!_canTouch) return;
            _currentBulletBall = _bulletPool.GetFreeElement();
            _currentBulletBall.transform.position = _spawnPosition.transform.position;
            _currentBulletBall.transform.localScale = transform.localScale;
            transform.localScale = Vector3.zero;
            _canTouch = false;
            _currentBulletBall.LastJump(_endPosition.localPosition);
        }
    }
}
