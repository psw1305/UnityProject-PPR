using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PSW.Core.Stat
{
    [Serializable]
    public class Stat
    {
        public int BaseValue;

        public virtual int Value
        {
            get
            {
                if (this.isDirty || this.BaseValue != this.lastBaseValue)
                {
                    this.lastBaseValue = this.BaseValue;
                    this.currentValue = CalculateFinalValue();
                    this.isDirty = false;
                }

                return this.currentValue;
            }
        }

        protected bool isDirty = true;
        protected int currentValue;
        protected int lastBaseValue = int.MinValue;

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public Stat()
        {
            this.statModifiers = new List<StatModifier>();
            this.StatModifiers = statModifiers.AsReadOnly();
        }

        public Stat(int baseValue) : this()
        {
            this.BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            this.isDirty = true;
            this.statModifiers.Add(mod);
            this.statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (this.statModifiers.Remove(mod))
            {
                this.isDirty = true;
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

            for (int i = this.statModifiers.Count - 1; i >= 0; i--)
            {
                if (this.statModifiers[i].Source == source)
                {
                    this.isDirty = true;
                    didRemove = true;
                    this.statModifiers.RemoveAt(i);
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

            for (int i = 0; i < this.statModifiers.Count; i++)
            {
                StatModifier mod = this.statModifiers[i];

                if (mod.Type == StatModType.Int)
                {
                    finalValue += mod.Value;
                }
            }

            return finalValue;
        }
    }
}