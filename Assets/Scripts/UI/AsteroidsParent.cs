using UISystem;
using UnityEngine;
using VContainer;
using static Game.Constants;

namespace AsteroidsSystem
{
    public class AsteroidsParent : MonoBehaviour
    {
        [Inject] private UIController _uiController;

        [SerializeField] private AsteroidsParentType _type;

        [Inject]
        public void Construct()
        {
            _uiController.AddItemUI(ParentAsteroids + _type, new ItemUI(transform));
        }
    } 
}
