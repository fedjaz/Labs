using System;
using System.Text;

namespace HumanProject
{
    class Boxer : Athlete
    {
        public struct SkillOfHands
        {
            public SkillOfHands(float rightHandSkill, float leftHandSkill)
            {
                RightHandSkill = rightHandSkill;
                LeftHandSkill = leftHandSkill;
            }
            public float RightHandSkill { get; set; }
            public float LeftHandSkill { get; set; }
            
        }

        public SkillOfHands Skills;
        public override float Skill 
        { 
            get => (Skills.RightHandSkill + Skills.LeftHandSkill) / 2; 
        }
        public enum Hands
        {
            Right,
            Left
        }
        public Hands DominantHand { get; set; }
        public float Weight { get; set; }

        public Boxer(Hands dominantHand, float weight,
                     float rightHandSkill, float leftHandSkill,
                     float strength, float agility, float stamina)
                     : base(strength, agility, stamina, 
                            (leftHandSkill + rightHandSkill) / 2)
        {

            DominantHand = dominantHand;
            Weight = weight;
            Skills = new SkillOfHands(rightHandSkill, leftHandSkill);
        }

        public Boxer(Hands dominantHand, float weight,
                     float rightHandSkill, float leftHandSkill,
                     float strength, float agility, float stamina,
                     string name, string surname,
                     bool isAlive = true)
                     : base(strength, agility, stamina,
                            (leftHandSkill + rightHandSkill) / 2,
                            name, surname, isAlive)
        {
            DominantHand = dominantHand;
            Weight = weight;
            Skills = new SkillOfHands(rightHandSkill, leftHandSkill);
        }

        public Boxer(Hands dominantHand, float weight,
                     float rightHandSkill, float leftHandSkill,
                     float strength, float agility, float stamina,
                     string name, string surname,
                     DateTime dateOfBirth, Genders gender,
                     string adress = null, bool isAlive = true)
                     : base(strength, agility, stamina,
                            (leftHandSkill + rightHandSkill) / 2,
                            name, surname, dateOfBirth, gender,
                            adress, isAlive)
        {
            DominantHand = dominantHand;
            Weight = weight;
            Skills = new SkillOfHands(rightHandSkill, leftHandSkill);
        }

        public override void Train(int time)
        {
            if(DominantHand == Hands.Right)
            {
                Skills.RightHandSkill += time * 0.05f;
            }
            if(DominantHand == Hands.Left)
            {
                Skills.LeftHandSkill += time * 0.05f;
            }

            Skills.RightHandSkill += time * 0.01f;
            Skills.LeftHandSkill += time * 0.01f;
            Strength += time * 0.2f;
            Stamina += time * 0.2f;
            Agility += time * 0.1f;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder(base.ToString());
            output.AppendLine();
            output.AppendLine(string.Format("Weight: {0}", Weight));
            output.AppendLine(string.Format("Dominant hand: {0}", DominantHand.ToString()));
            output.AppendLine(string.Format("Right hand skill: {0}", Skills.RightHandSkill));
            output.Append(string.Format("Left hand skill: {0}", Skills.LeftHandSkill));
            return output.ToString();
        }
    }
}
