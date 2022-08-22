using UnityEngine;

namespace Wrenji.Utils.Mechanics
{
    public class AbilityController : MonoBehaviour
    {
        private int attackIndex;

        //Constants
        public LayerMask enemyLayer;

        //Depends on Character
        public double[] attackTime;
        public double[] attackDamage;
        public Vector3[] attackPoint;
        public float[] attackRadius;

        //Others
        private float timer;
       

        void Start()
        {
            Settings();

            
        }

        void Update()
        {

        }

        void Settings()
        {
            //assign enemy layer

        }

        void attack(int i)
        {

            //Create hit radius
            Vector3 realAttackPoint = attackPoint[i] + this.transform.position;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(realAttackPoint, attackRadius[i], enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                GameObject enemyGameObject = enemy.gameObject;
                ExecuteDamage(enemyGameObject, attackDamage[i]);
            }
 
        }

        public void ExecuteDamage(GameObject other, double damage)
        {
            HealthController healthController = other.GetComponent<HealthController>();

            if (healthController != null)
            {
                healthController.IncrementHealth(-damage);
            }
        }


    }
}

//abilites are exicuted here