using System.Reflection;
using System;
using System.Collections.Generic;

namespace Kember
{
    public abstract class Metric
    {
        public abstract metric[] RunMetric(Assembly[] assembly, object args);

        public abstract metric[] Read(string input);

        public abstract string Write(metric[] output);

        

        protected string TypeClassification(Type type)
        {
            if (type.IsEnum) return "Перечисление";
            else if (type.IsValueType) return "Структура";
            else if (type.IsInterface) return "Интерфейс";
            else if (typeof(Delegate).IsAssignableFrom(type.BaseType)) return "Делегат";
            else return "Класс";
        }

        protected string CleanTypeName(Type type)
        {
            string name = type.Name.Split('+')[^1];
            int index = name.IndexOf('`');
            if (index >= 0) name = name.Substring(0, index);
            return name;
        }

        public struct metric
        {
            public object obj;
            public string assembly;

            public metric(object obj, string assembly)
            {
                this.obj = obj;
                this.assembly = assembly;
            }
        }
    }
}
