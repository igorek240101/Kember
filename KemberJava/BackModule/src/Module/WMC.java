package Module;

import BackModule.*;

import java.util.ArrayList;

public class WMC implements IMetric {

    public Object runMetric(Class<?> _class, Object args) {

        Flags flags = (Flags)(args);
        ArrayList<Class> types = cleanType(_class, flags);
        ArrayList<Cortege<String, Cortege<String, Integer>>> res = new ArrayList<>();
        for (int i = 0; i < types.size(); i++)
        {
            if (true)//typeof(Delegate).IsAssignableFrom(types[i].BaseType))
            {
                res.set(i,new Cortege<>(typeClassification(types.get(i)), new Cortege<>(cleanTypeName(types.get(i)), 0)));
            }
            else
            {/*
                BindingFlags binding = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
                if ((flags & Flags.PrivateMethods) != 0) binding |= BindingFlags.NonPublic;
                if ((flags & Flags.StaticMethods) != 0) binding |= BindingFlags.Static;
                int plas = (flags & Flags.Property) != 0 ? types[i].GetProperties(binding).Length : 0;
                res[i] = (TypeClassification(types[i]), CleanTypeName(types[i]), types[i].GetMethods(binding).Length + plas);
                */
            }
        }
        return res;
    }

    public Cortege<Class<?>, Object> read(String input) {
        return null;
    }

    public String write(Cortege<Class<?>, Object> output) {
        return null;
    }

    private ArrayList<Class> cleanType(Class _class, Flags flags) {

        ArrayList<Class> types = new ArrayList<>();
        types.add(_class);
        for (int i = 0; i < types.size(); i++)
        {
            if (false)//(flags & Flags.StaticClass) == 0 && types[i].IsAbstract && types[i].IsSealed && false) // Требуется уточнение подхода для языков отличных от C$
            {
                types.remove(i);
                i--; continue;
            }
            else
            {
                if (false)//(flags & Flags.Delegate) == 0 && typeof(Delegate).IsAssignableFrom(types[i].BaseType)) // Костыль
                {
                    types.remove(i);
                    i--; continue;
                }
                else
                {
                    if (false)//(flags & Flags.AnonymousType) == 0 && false) // В явном виде не может быть реализовано
                    {
                        types.remove(i);
                        i--; continue;
                    }
                    else
                    {
                        if (false)//(flags & Flags.Struct) == 0 && types[i].IsValueType && !types[i].IsEnum)
                        {
                            types.remove(i);
                            i--; continue;
                        }
                        else
                        {
                            if (false)//(flags & Flags.Nested) == 0 && types[i].IsNested)
                            {
                                types.remove(i);
                                i--; continue;
                            }
                            else
                            {
                                if (false)//(flags & Flags.Enum) == 0 && types[i].IsEnum)
                                {
                                    types.remove(i);
                                    i--; continue;
                                }
                                else
                                {
                                    if (false)//(flags & Flags.Interface) == 0 && types[i].IsInterface)
                                    {
                                        types.remove(i);
                                        i--; continue;
                                    }
                                    else
                                    {
                                        //Object[] array = types.get(i).GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
                                        if (false)//array != null && array.Length > 0)
                                        {
                                            types.remove(i);
                                            i--; continue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return types;
    }

    private String typeClassification(Class type){
        //if (type.IsEnum) return "Перечисление";
        //else if (type.IsValueType) return "Структура";
        //else if (type.IsInterface) return "Интерфейс";
        //else if (typeof(Delegate).IsAssignableFrom(type.BaseType)) return "Делегат";
        //else if (type.IsAbstract) return "Абстрактный класс";
        //else return "Класс";
        return  null;
    }

    private String cleanTypeName(Class type) {

        String[] names = type.getName().split("\\+");
        String name = names[names.length-1];
        int index = name.indexOf('`');
        if (index >= 0) name = name.substring(0, index);
        return name;
    }
}
