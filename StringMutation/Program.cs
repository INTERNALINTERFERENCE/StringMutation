// See https://aka.ms/new-console-template for more information

using StringMutation;

const string constStr = "000";
//IlMutation.Mutate(constStr);
//Marshal.Mutate(constStr);
Marshal.UnsafeMutate(constStr);
var nonConst = "000";
Console.WriteLine(nonConst);