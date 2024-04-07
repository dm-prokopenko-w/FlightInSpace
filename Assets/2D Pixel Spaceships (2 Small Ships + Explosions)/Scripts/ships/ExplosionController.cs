using UnityEngine;
using UnityEngine.Serialization;

namespace SmallShips
{
    public class ExplosionController : MonoBehaviour {

        [Tooltip("Child game objects that should be destroyed during explosion. For that 'DestroyPart(x)' will be called from animation clip.")]
        public GameObject[] removeParts;
        [Tooltip("Array of children that have animation for explosion and should explode by calling from parent animation clip.")]
        public ExplosionController[] childrenExplosion;
        
        [FormerlySerializedAs("animator")] [SerializeField]  Animator _animator;

        public void DestroyPart(int index)
        {
            if (removeParts != null && index >= 0 && index < removeParts.Length)
                removeParts[index].SetActive(false);
            else
                Debug.LogWarning("Index is out of range in DestroPart. index: " + index);
        }

        public void StartExplosion()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            _animator.SetBool("IsDied", true);
        }

        public void Restart()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            _animator.SetBool("IsDied", false);

            foreach (var part in removeParts)
            {
                part.SetActive(true);
            }
            gameObject.SetActive(true);
            transform.parent.gameObject.SetActive(true);

        }
        
        public void DestroyObject()
        {
            gameObject.SetActive(false);
        }

        public void DestroyParentObject()
        {
            transform.parent.gameObject.SetActive(false);
        }

        public void ChildExplosion(int index)
        {
            if (childrenExplosion != null && index >= 0 && index < childrenExplosion.Length)
                childrenExplosion[index].StartExplosion();
        }

        public void DestroyChildren()
        {
            if (removeParts != null && removeParts.Length > 0)
                foreach (GameObject child in removeParts)
                    child.SetActive(false);
        }
    }
}
