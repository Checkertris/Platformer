using UnityEngine;

namespace Wrenji.Utils.Mechanics
{
    public class LevellingController : MonoBehaviour
    {
        private int level = 0;

        public int GetLevel()
        {
            return this.level;
        }

        public void SetLevel(int level)
        {
            if (this.level != level)
            {
                OnLevelChange(this.level, level);
                this.level = level;
            }
        }

        public void IncrementLevel()
        {
            OnLevelChange(this.level, ++this.level);
        }

        public void Reset()
        {
            if (this.level != 0)
            {
                OnLevelChange(this.level, 0);
            }
            this.level = 0;
        }

        protected void OnLevelChange(int from, int to)
        {
            // Override as necessary
        }

    }
}