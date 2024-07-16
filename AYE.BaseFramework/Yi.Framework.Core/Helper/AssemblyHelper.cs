using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Helper
{
    public static class AssemblyHelper
    {

        /// <summary>
        /// 此处统一获取程序集，排除微软内部相关
        /// 获取当前应用程序域中加载的所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllLoadAssembly()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }


        /// <summary>
        /// 获取当前应用程序域中所有引用的程序集
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static List<Assembly> GetReferanceAssemblies(this AppDomain domain)
        {
            var list = new List<Assembly>();
            domain.GetAssemblies().ToList().ForEach(i =>
            {
                GetReferanceAssemblies(i, list);
            });
            return list;
        }

        /// <summary>
        /// 递归获取给定程序集的所有引用的程序集。
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="list"></param>
        private static void GetReferanceAssemblies(Assembly assembly, List<Assembly> list)
        {
            assembly.GetReferencedAssemblies().ToList().ForEach(i =>
            {
                var ass = Assembly.Load(i);
                if (!list.Contains(ass))
                {
                    list.Add(ass);
                    GetReferanceAssemblies(ass, list);
                }
            });
        }


        /// <summary>
        /// 从指定的程序集文件中获取指定类名和命名空间的所有类。
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="className"></param>
        /// <param name="spaceName"></param>
        /// <returns></returns>
        public static List<Type> GetClass(string assemblyFile, string? className = null, string? spaceName = null)
        {
            Assembly assembly = Assembly.Load(assemblyFile);
            return assembly.GetTypes().Where(m => m.IsClass
            && className == null ? true : m.Name == className
            && spaceName == null ? true : m.Namespace == spaceName
            && !m.Name.StartsWith("<>")
             ).ToList();
        }


        /// <summary>
        /// 获取继承自指定父类的所有类
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Type> GetClassByParentClass(string assemblyFile, Type type)
        {
            Assembly assembly = Assembly.Load(assemblyFile);

            List<Type> resList = new List<Type>();

            List<Type> typeList = assembly.GetTypes().Where(m => m.IsClass).ToList();
            foreach (var t in typeList)
            {
                var data = t.BaseType;
                if (data == type)
                {
                    resList.Add(t);
                }

            }
            return resList;
        }


        /// <summary>
        /// 获取实现指定接口的所有类。
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Type> GetClassByInterfaces(string assemblyFile, Type type)
        {
            Assembly assembly = Assembly.Load(assemblyFile);

            List<Type> resList = new List<Type>();

            List<Type> typeList = assembly.GetTypes().Where(m => m.IsClass).ToList();
            foreach (var t in typeList)
            {
                var data = t.GetInterfaces();
                if (data.Contains(type))
                {
                    resList.Add(t);
                }

            }
            return resList;
        }

    }
}
