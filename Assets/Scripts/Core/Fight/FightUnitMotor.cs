using UnityEngine;

namespace Core.Fight
{
    /**
     *  @brief Часть контроллера юнита отвечающая за перемещение по карте.
     */
    public class FightUnitMotor : MonoBehaviour
    {
        public HexPath activePath;

        public float movingSpeed = 1.0f; // cells/sec.
        public bool isMoving = false;

        private Vector3 previousPosition;
        private Vector3 nextPosition;
        private float time; // [0, 1] for smooth moving between neighbor cells

        public void MoveWithPath(HexPath path)
        {
            if (!isMoving)
            {
                isMoving = true;
                activePath = path;
                time = 0.0f;
                previousPosition = transform.position;
                nextPosition = activePath.GetCurrentCell().transform.position;
            }
        }

        private void FixedUpdate()
        {
            if (GameManager.IsPaused())
                return;
            
            if (activePath != null && activePath.IsNotCompleted())
            {
                if (time >= 1.0f)
                {
                    time = 0.0f;
                    previousPosition = nextPosition;
                    activePath.GoToNextCell();
                    if (activePath.IsCompleted())
                    {
                        transform.position = previousPosition;
                        isMoving = false;
                    }
                    else
                    {
                        nextPosition = activePath.GetCurrentCell().transform.position;
                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(previousPosition, nextPosition, time);
                    time += Time.fixedDeltaTime * movingSpeed;
                }
            }
        }
    }
}