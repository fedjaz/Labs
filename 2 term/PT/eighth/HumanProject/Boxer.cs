using System;
using System.Text;

namespace HumanProject
{
    class Boxer : Athlete, IFighter
    {
        public delegate void TrainFunction(ref float rightHand, ref float leftHand, Hands dominantHand,
                                           ref float strength, ref float stamina, ref float agility, int time);
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

        public float Kick()
        {
            if(!IsAlive)
            {
                throw new InvalidOperationException("This fighter is dead.");
            }
            float force = Strength * 0.3f + Skill * 0.1f +
                          Stamina * 0.1f + Agility * 0.1f;
            return force;
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
            Train(time, DefaultTrainFunction);
        }

        public void Train(int time, TrainFunction function)
        {
            if(!IsAlive)
            {
                throw new InvalidOperationException("This athlete can't train because he is dead.");
            }
            float rightHand = Skills.RightHandSkill;
            float leftHand = Skills.LeftHandSkill;
            float strength = Strength;
            float stamina = Stamina;
            float agility = Agility;
            function.Invoke(ref rightHand, ref leftHand, DominantHand, ref strength, ref stamina, ref agility, time);
            (Skills.RightHandSkill, Skills.LeftHandSkill, Strength, Stamina, Agility) =
            (rightHand, leftHand, strength, stamina, agility);
        }

        static void DefaultTrainFunction(ref float rightHand, ref float leftHand, Hands dominantHand,
                                  ref float strength, ref float stamina, ref float agility, int time)
        {
            if(dominantHand == Hands.Right)
            {
                rightHand += time * 0.05f;
            }
            if(dominantHand == Hands.Left)
            {
                leftHand += time * 0.05f;
            }

            rightHand += time * 0.01f;
            leftHand += time * 0.01f;
            strength += time * 0.2f;
            stamina += time * 0.2f;
            agility += time * 0.1f;
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
