
namespace DTWorlds.Interfaces
{
    public interface IDamagableProperty
    {
        float CurrentValue { get; set; }

        float MaxValue { get; }

        event Mobiles.DamagableProperties.Health.DamagablePropertyValueChangedHandler OnAfterValueChangedEvent;
    }
}

