using System;
using System.Collections.Generic;

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
        public int CurrentGold { get; set; } = 0;
        public int CurrentCafBean { get; set; } = 0;
        public int CurrentDecafBean{ get; set; } = 0;
        public int CurrentCarCreamer { get; set; } = 0;
        public int CurrentMilkCreamer { get; set; } = 0;
        public int Luck { get; set; } = 0;
        public int AttackPower { get; set; } = 1;
        public int MaxLevel { get; set; } = 60;
        public bool HasDashSkill { get; set; } = false;
        public bool HasSpearSkill { get; set; } = false;
        public bool hasThreeSkill { get; set; } = false;
        public bool hasFourSkill { get; set; } = false;
        public bool hasFiveSkill { get; set; } = false;
        public bool hasSixSkill { get; set; } = false;
        public SkillTypes SkillOne { get; set; } = SkillTypes.None;
        public SkillTypes SkillTwo { get; set; } = SkillTypes.None;

        // C# events instead of Unity events
        public event Action OnDie;
        public event Action<int> OnTakeDamage;

        public event Action OnLevelUp;

        public PlayerStats()
        {
            Reset();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDie?.Invoke();
            }
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
            OnLevelUp?.Invoke();
        }

        public void IncreaseMaxHealth(int amount)
        {
            MaxHealth += amount;
        }

        public void IncreaseAttack(int amount)
        {
            AttackPower += amount;
        }
        public void IncreaseLuck(int amount)
        {
            Luck += amount;
        }

        public void IncreaseMaxSP(int amount)
        {
            MaxSP += amount;
        }
        public void ChangeGold(int amount)
        {
            CurrentGold += amount;
        }
        public void ChangeCafBean(int amount)
        {
            CurrentCafBean += amount;
        }
        public void ChangeDecafBean(int amount)
        {
            CurrentDecafBean += amount;
        }
        public void ChangeCarCreamer(int amount)
        {
            CurrentCarCreamer += amount;
        }
        public void ChangeMilkCreamer(int amount)
        {
            CurrentMilkCreamer += amount;
        }
        
        

        public void Reset()
        {
            CurrentHealth = MaxHealth;
            CurrentSP = MaxSP;
        }
    }
}
