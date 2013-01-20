using System;
using System.Reflection.Emit;

namespace Inception.Proxying.Generators.ILGeneration
{
    internal interface IEmitter
    {
        void Emit(ILGenerator il);
    }
}