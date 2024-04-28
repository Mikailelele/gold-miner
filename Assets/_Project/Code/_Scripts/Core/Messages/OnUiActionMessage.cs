using System;
using _Project.Core.UI;

namespace _Project.Core.Messages
{
    public readonly struct OnUiActionMessage : IEquatable<OnUiActionMessage>
    {
        public EUiType Type { get; }
        
        public OnUiActionMessage(in EUiType type)
        {
            Type = type;
        }

        public bool Equals(OnUiActionMessage other)
        {
            return Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            return obj is OnUiActionMessage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)Type;
        }
    }
}