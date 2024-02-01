using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path path;
        private int pathIndex;

        [SerializeField] private UnityEvent OnEndPath; 

        public void SetPath(Path newPath)
        {
            path = newPath;
            pathIndex = 0;     
            SetPatrolBehaviour(newPath[pathIndex]);
        }

        protected override void GetNewPoint()
        {
            pathIndex += 1;
            
            if (path.Length > pathIndex) // like as: ++pathIndex;
            {
                SetPatrolBehaviour(path[pathIndex]);
            }
            else
            {
                OnEndPath?.Invoke(); 
                Destroy(gameObject);
            }
        }
    }
}
