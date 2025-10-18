using System;

namespace PlayerStuff
{
    public class PlayerStats
    {
        public int Level { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public int ExperienceToNextLevel { get; set; } = 5;
        public int MaxHealth { get; set; } = 3;
        public int CurrentHealth { get; set; }
        public int MaxSP { get; set; } = 5;
        public int CurrentSP { get; set; }
        public int MaxOxygen { get; set; } = 120;
        public int CurrentOxygen { get; set; }
        public int AttackPower { get; set; } = 1;
        public int MaxLevel { get; set; } = 60;

        // C# events instead of Unity events
        public event Action OnDie;
        public event Action<int> OnTakeDamage;

        public PlayerStats()
        {
            Reset();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = 10000000;
        }

        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
        }

        public void UseSP(int amount)
        {
            CurrentSP = Math.Max(0, CurrentSP - amount);
        }

        public void GainSP(int amount)
        {
            CurrentSP = Math.Min(MaxSP, CurrentSP + amount);
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            if (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            if (Level >= MaxLevel) return;
            Experience = 0;
            Level++;
            ExperienceToNextLevel += 1;
            MaxOxygen += 2;

            if (Level % 4 == 0)
                MaxSP += 1;
        }

        public void IncreaseMaxHealth(int amount)
        {
            MaxHealth += amount;
        }

        public void IncreaseAttack(int amount)
        {
            AttackPower += amount;
        }

        public void DecreaseOxygen(int amount)
        {
            CurrentOxygen -= amount;
            if (CurrentOxygen <= 0)
            {
                CurrentOxygen = 0;
                OnTakeDamage?.Invoke(1);
            }
        }

        public void Reset()
        {
            CurrentHealth = MaxHealth;
            CurrentSP = MaxSP;
            CurrentOxygen = MaxOxygen;
        }
    }
}
