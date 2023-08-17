using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StringMutation;

public class Marshal
{
    private const string ErrorCodeStr = "404";
    private const ulong ErrorCode = 0x0000003400300034ul;
    
    public static void Mutate(string str)
    {
        ErrorCodeStr.CopyTo(MemoryMarshal.CreateSpan(                       
            ref MemoryMarshal.GetReference(str.AsSpan()), str.Length));
    }

    public static void UnsafeMutate(string str)
    {
        Unsafe.As<char, ulong>(ref MemoryMarshal
            .GetReference(str.AsSpan())) = ErrorCode;
    }
}