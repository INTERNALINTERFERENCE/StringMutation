using System.Reflection.Emit;

namespace StringMutation;

public static class IlMutation
{
    public static void Mutate(string str)
    {
        var method = new DynamicMethod("foo", typeof(void), new[] { typeof(string) });
        
        var il = method.GetILGenerator();
        
        il.Emit(OpCodes.Ldarg_0);             // load string OBJECTREF
        il.Emit(OpCodes.Ldc_I4, IntPtr.Size); // skip "m_pMethodTable"
        il.Emit(OpCodes.Ldc_I4, sizeof(int)); // skip "length"
        il.Emit(OpCodes.Add);
        il.Emit(OpCodes.Conv_I);
        il.Emit(OpCodes.Add);
        // now we have reference to a first char, store '4'
        il.Emit(OpCodes.Dup);
        il.Emit(OpCodes.Ldc_I4, '4');
        il.Emit(OpCodes.Conv_I2);
        il.Emit(OpCodes.Stind_I2);
        // move to third char
        il.Emit(OpCodes.Ldc_I4, sizeof(char) * 2);
        il.Emit(OpCodes.Conv_I);
        il.Emit(OpCodes.Add);
        // now we have reference to a third char, store '4' again
        il.Emit(OpCodes.Ldc_I4, '4');
        il.Emit(OpCodes.Conv_I2);
        il.Emit(OpCodes.Stind_I2);
        il.Emit(OpCodes.Ret);
        // call
        method.CreateDelegate<Action<string>>()(str);
    }
}