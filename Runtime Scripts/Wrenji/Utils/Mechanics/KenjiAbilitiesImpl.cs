using UnityEngine;

namespace Wrenji.Utils.Mechanics
{
    public abstract class KenjiAbilitiesImpl : MonoBehaviour
    {
        private double[] attackTime = {0, 0, 0, 0, 0, 0, 0};
        private double[] attackDamage = {0, 0, 0, 0, 0, 0, 0};
        private Vector3[] attackPoint = { new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f) };
        private float[] attackRadius = {0f, 0f, 0f, 0f, 0f, 0f, 0f};

        private string[] attackControl = { "", "", "", "", "", "", "" };

        //For Gizmo
        public Vector3 placeholderPosition;
        public float placeholderRange;

        void Start()
        {



        }

        void Settings()
        {

            AbilityController abilityController = this.GetComponent<AbilityController>();
            PlayerController playerMovementController = this.GetComponent<PlayerController>();

            abilityController.attackTime = attackTime;
            abilityController.attackDamage = attackDamage;
            abilityController.attackPoint = attackPoint;
            abilityController.attackRadius = attackRadius;

            playerMovementController.attackControl = attackControl;

        }

        void OnDrawGizmosSelected()
        {
            int i = 0;
            Gizmos.DrawWireSphere(attackPoint[i] + this.transform.position + placeholderPosition, attackRadius[i] + placeholderRange);
        }

    }

}

//Ability specific scripts go here, ability arrays are defined here