using DG.Tweening;
using UnityEngine;

namespace GameLogic
{
    public class BulletBall : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _contaminationRadius = 2;
        private bool _canMove;
        private bool isMain;

        public void Shot(bool isMainBall = false)
        {
            isMain = isMainBall;
            _canMove = true;
        }

        public void LastJump(Vector3 endPoint)
        {
            isMain = true;
            transform.DOJump(endPoint, 2, 20, 15);

        }

        public bool GetIsMain()
        {
            return isMain;
        }
        private void OnEnable()
        {
            _canMove = false;
        }

        private void Update()
        {
            if(!_canMove) return;
            transform.position += Vector3.forward * (_speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!collision.gameObject.GetComponent<Enemy>()) return;
            
            _contaminationRadius = transform.localScale.x * 2.5f;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _contaminationRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<Enemy>())
                {
                    hitCollider.GetComponent<Enemy>().Disappear();
                }
            }
            gameObject.SetActive(false);
        }
  
    }
}
