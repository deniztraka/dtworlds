
namespace DTWorlds.Interfaces
{
    public interface IDamagablePropertyX
    {
        float CurrentValue { get; set; }

        float MaxValue { get; }

        event Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler OnAfterValueChangedEvent;
    }
}

