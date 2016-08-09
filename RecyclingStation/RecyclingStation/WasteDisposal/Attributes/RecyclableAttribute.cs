namespace RecyclingStation.WasteDisposal.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class RecyclableAttribute : DisposableAttribute
    {
    }
}
