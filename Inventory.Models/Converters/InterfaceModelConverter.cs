using Newtonsoft.Json.Converters;
using System;

namespace Inventory.Models.Converters
{
    public class InterfaceModelConverter<I, C> : CustomCreationConverter<I> where C : I, new()
    {
        public override I Create(Type objectType)
        {
            var obj = Activator.CreateInstance(typeof(C));
            if (!(obj is I))
            {
                throw new InvalidCastException($"Can't convert {nameof(objectType)} to {nameof(I)}");
            }
            return (I)obj;
        }
    }
}
