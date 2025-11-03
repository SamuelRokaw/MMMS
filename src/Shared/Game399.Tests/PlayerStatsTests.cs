using Game399.Shared;
using PlayerStuff;

namespace Game399.Tests;

public class PlayerStatsTests
{

        private PlayerStats stats;

        [SetUp]
        public void Setup()
        {
            stats = new PlayerStats();
        }

        // -----------------------
        // INITIAL VALUES
        // -----------------------
        [Test]
        public void Level_IsZeroOnStart() => Assert.AreEqual(0, stats.Level);

        [Test]
        public void Experience_IsZeroOnStart() => Assert.AreEqual(0, stats.Experience);

        [Test]
        public void ExperienceToNextLevel_IsFiveOnStart() => Assert.AreEqual(5, stats.ExperienceToNextLevel);

        [Test]
        public void MaxHealth_IsThreeOnStart() => Assert.AreEqual(3, stats.MaxHealth);

        [Test]
        public void CurrentHealth_EqualsMaxHealthOnStart() => Assert.AreEqual(stats.MaxHealth, stats.CurrentHealth);

        [Test]
        public void MaxSP_IsFiveOnStart() => Assert.AreEqual(5, stats.MaxSP);

        [Test]
        public void CurrentSP_EqualsMaxSPOnStart() => Assert.AreEqual(stats.MaxSP, stats.CurrentSP);
        

        [Test]
        public void AttackPower_IsOneOnStart() => Assert.AreEqual(1, stats.AttackPower);

        // -----------------------
        // DAMAGE & DEATH
        // -----------------------
        [Test]
        public void TakeDamage_ReducesHealth()
        {
            stats.TakeDamage(2);
            Assert.AreEqual(1, stats.CurrentHealth);
        }

        [Test]
        public void TakeDamage_TriggersDieEventWhenHealthZero()
        {
            bool died = false;
            stats.OnDie += () => died = true;
            stats.TakeDamage(stats.CurrentHealth);
            Assert.IsTrue(died);
        }

        // -----------------------
        // HEALING
        // -----------------------
        [Test]
        public void Heal_IncreasesHealth()
        {
            stats.TakeDamage(2);
            stats.Heal(1);
            Assert.AreEqual(2, stats.CurrentHealth);
        }

        [Test]
        public void Heal_DoesNotExceedMaxHealth()
        {
            stats.Heal(10);
            Assert.AreEqual(stats.MaxHealth, stats.CurrentHealth);
        }

        // -----------------------
        // SP USAGE
        // -----------------------
        [Test]
        public void UseSP_ReducesCurrentSP()
        {
            stats.UseSP(3);
            Assert.AreEqual(2, stats.CurrentSP);
        }

        [Test]
        public void UseSP_DoesNotGoBelowZero()
        {
            stats.UseSP(10);
            Assert.AreEqual(0, stats.CurrentSP);
        }

        [Test]
        public void GainSP_IncreasesCurrentSP()
        {
            stats.UseSP(3);
            stats.GainSP(2);
            Assert.AreEqual(4, stats.CurrentSP);
        }

        [Test]
        public void GainSP_DoesNotExceedMaxSP()
        {
            stats.GainSP(10);
            Assert.AreEqual(stats.MaxSP, stats.CurrentSP);
        }

    
        // -----------------------
        // EXPERIENCE & LEVELING
        // -----------------------
        [Test]
        public void GainExperience_IncreasesExperience()
        {
            stats.GainExperience(3);
            Assert.AreEqual(3, stats.Experience);
        }

        [Test]
        public void GainExperience_LevelsUpWhenThresholdReached()
        {
            int startLevel = stats.Level;
            stats.GainExperience(stats.ExperienceToNextLevel);
            Assert.AreEqual(startLevel + 1, stats.Level);
        }

        [Test]
        public void GainExperience_ResetsExperienceOnLevelUp()
        {
            stats.GainExperience(stats.ExperienceToNextLevel);
            Assert.AreEqual(0, stats.Experience);
        }

        [Test]
        public void GainExperience_IncreasesNextLevelThreshold()
        {
            int oldThreshold = stats.ExperienceToNextLevel;
            stats.GainExperience(stats.ExperienceToNextLevel);
            Assert.Greater(stats.ExperienceToNextLevel, oldThreshold);
        }

        // -----------------------
        // INCREASING STATS
        // -----------------------
        [Test]
        public void IncreaseMaxHealth_WorksCorrectly()
        {
            int oldMax = stats.MaxHealth;
            stats.IncreaseMaxHealth(5);
            Assert.AreEqual(oldMax + 5, stats.MaxHealth);
        }

        [Test]
        public void IncreaseAttack_WorksCorrectly()
        {
            int oldAttack = stats.AttackPower;
            stats.IncreaseAttack(2);
            Assert.AreEqual(oldAttack + 2, stats.AttackPower);
        }

        // -----------------------
        // RESET
        // -----------------------
        [Test]
        public void Reset_RestoresHealthSPAndOxygen()
        {
            stats.TakeDamage(2);
            stats.UseSP(3);

            stats.Reset();

            Assert.AreEqual(stats.MaxHealth, stats.CurrentHealth);
            Assert.AreEqual(stats.MaxSP, stats.CurrentSP);
        }
    }