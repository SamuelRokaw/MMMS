namespace PlayerStuff
{
    [System.Serializable]
    public class StatsData
    {
        public int level;
        public int experience;
        public int experienceToNextLevel;
        public int maxHealth;
        public int currentHealth;
        public int maxSP;
        public int currentSP;
        public int luck;
        public int attackPower;
        public bool hasDashSkill;
        public bool hasSpearSkill;
        public bool hasThreeSkill;
        public bool hasFourSkill;
        public bool hasFiveSkill;
        public bool hasSixSkill;
        public SkillTypes skillOne;
        public SkillTypes skillTwo;
        public int currentGold;
        public int currentCafBean;
        public int currentDecafBean;
        public int currentCarCreamer;
        public int currentMilkCreamer;
    }
}