namespace RecyclingStation.WasteDisposal.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class BurnableAttribute : DisposableAttribute
    {
    }
}
