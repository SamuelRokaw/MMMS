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
        
        [Test]
        public void LevelUp_DoesNotExceedMaxLevel()
        {
            stats.Level = stats.MaxLevel;
            stats.GainExperience(stats.ExperienceToNextLevel);
            Assert.AreEqual(stats.MaxLevel, stats.Level);
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
        
        [Test]
        public void IncreaseLuck_WorksCorrectly()
        {
            int oldLuck = stats.Luck;
            stats.IncreaseLuck(3);
            Assert.AreEqual(oldLuck + 3, stats.Luck);
        }

        [Test]
        public void IncreaseMaxSP_WorksCorrectly()
        {
            int oldMaxSP = stats.MaxSP;
            stats.IncreaseMaxSP(2);
            Assert.AreEqual(oldMaxSP + 2, stats.MaxSP);
        }
        // -----------------------
        // Resources
        // -----------------------
        [Test]
        public void ChangeGold_IncreaseGoldCount()
        {
            stats.ChangeGold(10);
            Assert.AreEqual(110, stats.CurrentGold);
        }
        [Test]
        public void ChangeGold_DecreaseGoldCount()
        {
            stats.ChangeGold(-10);
            Assert.AreEqual(90, stats.CurrentGold);
        }

        [Test]
        public void ChangeCafBean_IncreasesCafBeanCount()
        {
            stats.ChangeCafBean(3);
            Assert.AreEqual(53, stats.CurrentCafBean);
        }

        [Test]
        public void ChangeCafBean_DecreasesCafBeanCount()
        {
            stats.ChangeCafBean(5);
            stats.ChangeCafBean(-2);
            Assert.AreEqual(53, stats.CurrentCafBean);
        }

        [Test]
        public void ChangeDecafBean_IncreasesDecafBeanCount()
        {
            stats.ChangeDecafBean(4);
            Assert.AreEqual(54, stats.CurrentDecafBean);
        }

        [Test]
        public void ChangeDecafBean_DecreasesDecafBeanCount()
        {
            stats.ChangeDecafBean(6);
            stats.ChangeDecafBean(-1);
            Assert.AreEqual(55, stats.CurrentDecafBean);
        }
        [Test]
        public void ChangeCarCreamer_IncreasesCarCreamerCount()
        {
            stats.ChangeCarCreamer(2);
            Assert.AreEqual(52, stats.CurrentCarCreamer);
        }

        [Test]
        public void ChangeCarCreamer_DecreasesCarCreamerCount()
        {
            stats.ChangeCarCreamer(4);
            stats.ChangeCarCreamer(-1);
            Assert.AreEqual(53, stats.CurrentCarCreamer);
        }

        [Test]
        public void ChangeMilkCreamer_IncreasesMilkCreamerCount()
        {
            stats.ChangeMilkCreamer(5);
            Assert.AreEqual(55, stats.CurrentMilkCreamer);
        }

        [Test]
        public void ChangeMilkCreamer_DecreasesMilkCreamerCount()
        {
            stats.ChangeMilkCreamer(3);
            stats.ChangeMilkCreamer(-2);
            Assert.AreEqual(51, stats.CurrentMilkCreamer);
        }
        // -----------------------
        // Skills
        // -----------------------
        [Test]
        public void SkillFlags_Default()
        {
            Assert.IsTrue(stats.HasDashSkill);
            Assert.IsTrue(stats.HasSpearSkill);
            Assert.IsFalse(stats.hasThreeSkill);
            Assert.IsFalse(stats.hasFourSkill);
            Assert.IsFalse(stats.hasFiveSkill);
            Assert.IsFalse(stats.hasSixSkill);
        }

        [Test]
        public void SkillTypes_DefaultToNone()
        {
            Assert.AreEqual(SkillTypes.None, stats.SkillOne);
            Assert.AreEqual(SkillTypes.None, stats.SkillTwo);
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