using System;
using System.Reflection.Emit;

namespace InceptionCore.Proxying.Generators.ILGeneration
{
    interface IEmitter
    {
        void Emit(ILGenerator il);
    }
}