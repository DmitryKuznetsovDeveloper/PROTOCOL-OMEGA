using System;
using Zenject;

namespace App
{
    public abstract class ViewModelBase : IDisposable
    {
        public virtual void Dispose() { }
    }
}