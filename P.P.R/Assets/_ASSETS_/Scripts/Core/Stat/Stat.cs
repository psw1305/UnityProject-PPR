namespace PSW.Core.Stat
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    [Serializable]
    public class Stat
    {
        public int BaseValue;

        public virtual int Value
        {
            get
            {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    currentValue = CalculateFinalValue();
                    isDirty = false;
                }

                return currentValue;
            }
        }

        protected bool isDirty = true;
        protected int currentValue;
        protected int lastBaseValue = int.MinValue;

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public Stat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public Stat(int baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            else
                return 0;
        }

        protected virtual int CalculateFinalValue()
        {
            int finalValue = BaseValue;
            //int sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Int)
                {
                    finalValue += mod.Value;
                }
                //else if (mod.Type == StatModType.PercentMult)
                //{
                //    sumPercentAdd += mod.Value;

                //    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                //    {
                //        finalValue *= 1 + sumPercentAdd;
                //        sumPercentAdd = 0;
                //    }
                //}
                //else if (mod.Type == StatModType.PercentMult)
                //{
                //    finalValue *= 1 + mod.Value;
                //}
            }

            return finalValue;
        }
    }
}