using System;
using UnityEngine;

namespace Wrenji.Utils.Mechanics
{
    public class HealthController : MonoBehaviour
    {

        private double health;

        public double GetHealth() => this.health;

        public double SetHealth(double health) => this.health = health;

        public double IncrementHealth(double value)
        {
            double newHealth = Math.Max(this.health + value, 0);
            OnHealthChange(this.health, newHealth);
            this.health = newHealth;
            return this.health;
        }

        public void OnHealthChange(double oldValue, double newValue) {
            // Override as necessary
        }

    }
}