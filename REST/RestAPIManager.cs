﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using Z.Log;

namespace Z.Rest
{
    /// <summary>
    /// 代理方法定义：需Rest处理的方法
    /// </summary>
    /// <param name="target">动态方法类型</param>
    /// <param name="parameters">动态方法的参数</param>
    /// <returns></returns>
    public delegate object FuncParams(object target, string[] parameters);
 
    /// <summary>
    /// Rest管理类
    /// </summary>
    public class RestAPIManager
    {
        private readonly static Logger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        private IDictionary<string, int> nameActionMapping = new Dictionary<string, int>();
        private IDictionary<int, MethodInvoker> actionIdToMethod = new Dictionary<int, MethodInvoker>();       
        private Dictionary<int, RestMethodInfo> restInfos = new Dictionary<int, RestMethodInfo>();

        /// <summary>
        /// 程序集生成器
        /// </summary>
        private AssemblyBuilder assemblyBuilder;

        /// <summary>
        /// 类型生成器
        /// </summary>
        private TypeBuilder typeBuilder;

        /// <summary>
        /// Rest接口信息
        /// </summary>
        public Dictionary<int, RestMethodInfo> RestInfos
        {
            get
            {
                return restInfos;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RestAPIManager()
        {
            AssemblyName aName = new AssemblyName("AutoGenerated");
            assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder mb = assemblyBuilder.DefineDynamicModule("Rest", "AutoGenerated.dll");
            typeBuilder = mb.DefineType("RestAPI", TypeAttributes.Public);
        }

        /// <summary>
        /// 注册需进行Rest的类
        /// </summary>
        /// <param name="info"></param>
        internal void Register(RestMethodInfo info)
        {
            MethodInvoker invoker = MethodInvoker.CreateMethodInvoker(info, typeBuilder);
            if (invoker != null)
            {
                actionIdToMethod.Add(info.ActionId, invoker);
                nameActionMapping.Add(info.ActionName.ToLower(), info.ActionId);
            }
        }


        internal void RegisterComplete()
        {
            Type t = typeBuilder.CreateType();
            foreach (MethodInvoker invoker in actionIdToMethod.Values)
            {
                invoker.Bind(t);
            }
            SaveAutoGeneratedDll();
        }

        [Conditional("DEBUG")]
        private void SaveAutoGeneratedDll()
        {
            //assemblyBuilder.Save("AutoGenerated.dll");
        }

        /// <summary>
        /// 是否存在ActionID指定的Rest接口
        /// </summary>
        /// <param name="actionId">actionID来标识特定的rest请求</param>
        /// <returns></returns>
        public bool HasActionId(int actionId)
        {
            return actionIdToMethod.ContainsKey(actionId);
        }

        /// <summary>
        /// 获取action id
        /// </summary>
        /// <param name="name">资源名称</param>
        /// <returns></returns>
        public int? GetActionId(string name)
        {
            if (nameActionMapping.ContainsKey(name.ToLower()))
            {
                return nameActionMapping[name.ToLower()];
            }
            return null;
        }

        /// <summary>
        /// 处理Rest请求
        /// </summary>
        /// <param name="actionId">actionID来标识特定的rest请求</param>
        /// <param name="parameters">rest请求参数</param>
        /// <returns></returns>
        public object ProcessRequest(int actionId, string[] parameters)
        {
            return actionIdToMethod[actionId].Process(parameters);
        }

        private class MethodInvoker
        {
            private readonly static Logger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
            private static MethodInfo intParser = typeof(int).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
            private static MethodInfo longParser = typeof(long).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
            private static MethodInfo doubleParser = typeof(double).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
            private static MethodInfo boolParser = typeof(bool).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
            private static MethodInfo getHttpContext = typeof(HttpContext).GetMethod("get_Current", BindingFlags.Public | BindingFlags.Static, null, new Type[] { }, null);
            private static MethodInfo getHttpRequest = typeof(HttpContext).GetMethod("get_Request", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { }, null);
            private static MethodInfo getRequestFiles = typeof(HttpRequest).GetMethod("get_Files", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { }, null);
            private static MethodInfo getFileItem = typeof(HttpFileCollection).GetMethod("get_Item", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string) }, null);
            private static MethodInfo getTypeInfo = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(RuntimeTypeHandle) }, null);
            private static MethodInfo isNullOrEmpty = typeof(String).GetMethod("IsNullOrEmpty", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(String) }, null);


            private MethodInvoker() { }
            private FuncParams function;
            private object serverInstance;
            private RestMethodInfo info;

            internal object Process(string[] p)
            {
                object result = function(serverInstance, p);
                return result;
            }

            internal void Bind(Type type)
            {
                MethodInfo tmx = type.GetMethod(info.MethodName);
                function = (FuncParams)Delegate.CreateDelegate(typeof(FuncParams), tmx);
            }

            //This method can only be called at the static constructor of APIMethodManager. CLR will keep the thread safety.
            internal static MethodInvoker CreateMethodInvoker(RestMethodInfo info, TypeBuilder tb)
            {
                if (!CheckMethod(info)) return null;

                Type[] argumentTypes = new Type[] { typeof(object), typeof(string[]) };
                MethodInvoker invoker = new MethodInvoker();
                invoker.serverInstance = Activator.CreateInstance(info.ProxyMethodInfo.DeclaringType);
                invoker.info = info;
                MethodBuilder mb = null;
                mb = tb.DefineMethod(info.MethodName, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, info.ReturnType, argumentTypes);
                ILGenerator il = mb.GetILGenerator();
                LocalBuilder[] locals = new LocalBuilder[info.ParameterInfos.Count];
                Label[] tryCatchs = new Label[info.ParameterInfos.Count];

                for (int i = 0; i < info.ParameterInfos.Count; i++)
                {
                    tryCatchs[i] = il.BeginExceptionBlock();
                    locals[i] = il.DeclareLocal(info.ParameterInfos[i].ParameterType);
                    il.Emit(OpCodes.Ldarg_1);
                    LoadLiteral(il, i);
                    il.Emit(OpCodes.Ldelem_Ref);
                    object dValue = null;
                    if (!info.ParameterInfos[i].IsRequired)
                    {
                        dValue = info.ParameterInfos[i].DefaultValue;
                    }

                    if (info.ParameterInfos[i].ParameterType == typeof(string))
                    {
                        Label label = il.DefineLabel();
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Call, isNullOrEmpty);
                        il.Emit(OpCodes.Brfalse_S, label);
                        if (info.ParameterInfos[i].IsRequired)
                        {
                            il.Emit(OpCodes.Newobj, typeof(Exception).GetConstructor(new Type[] { }));
                            il.Emit(OpCodes.Throw);
                        }
                        else
                        {
                            il.Emit(OpCodes.Pop);
                            if (dValue == null)
                            {
                                il.Emit(OpCodes.Ldnull);
                            }
                            else
                            {
                                string v = dValue as string;
                                if (v == null) throw new Exception(string.Format("错误的默认值设定 method:{0} parameter:{1}", info.ResourceName, info.ParameterInfos[i].ParameterName));
                                il.Emit(OpCodes.Ldstr, v);
                            }
                        }
                        il.MarkLabel(label);
                    }
                    else if (info.ParameterInfos[i].ParameterType == typeof(int))
                    {                        
                        if (info.ParameterInfos[i].IsRequired)
                        {
                            il.Emit(OpCodes.Call, intParser);
                        }
                        else
                        {
                            int v = 0;
                            try
                            {
                                v = dValue == null ? 0 : Convert.ToInt32(dValue);
                            }
                            catch
                            {
                                throw new Exception(string.Format("错误的默认值设定 method:{0} parameter:{1}", info.ResourceName, info.ParameterInfos[i].ParameterName));
                            }
                            Label label1 = il.DefineLabel();
                            Label label2 = il.DefineLabel();
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Call, isNullOrEmpty);
                            il.Emit(OpCodes.Brtrue_S, label2);
                            il.Emit(OpCodes.Call, intParser);
                            il.Emit(OpCodes.Br_S, label1);                            
                            il.MarkLabel(label2);
                            il.Emit(OpCodes.Pop);
                            LoadLiteral(il, v);
                            il.MarkLabel(label1);
                        }                                                
                    }
                    else if (info.ParameterInfos[i].ParameterType == typeof(long))
                    {
                        if (info.ParameterInfos[i].IsRequired)
                        {
                            il.Emit(OpCodes.Call, longParser);
                        }
                        else
                        {
                            long v = 0;
                            try
                            {
                                v = dValue == null ? 0 : Convert.ToInt64(dValue);
                            }
                            catch
                            {
                                throw new Exception(string.Format("错误的默认值设定 method:{0} parameter:{1}", info.ResourceName, info.ParameterInfos[i].ParameterName));
                            }

                            Label label1 = il.DefineLabel();
                            Label label2 = il.DefineLabel();
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Call, isNullOrEmpty);
                            il.Emit(OpCodes.Brtrue_S, label2);
                            il.Emit(OpCodes.Call, longParser);
                            il.Emit(OpCodes.Br_S, label1);
                            il.MarkLabel(label2);
                            il.Emit(OpCodes.Pop);
                            il.Emit(OpCodes.Ldc_I8, v);                            
                            il.MarkLabel(label1);
                        }
                        
                        
                    }
                    else if (info.ParameterInfos[i].ParameterType == typeof(double))
                    {
                        if (info.ParameterInfos[i].IsRequired)
                        {
                            il.Emit(OpCodes.Call, doubleParser);
                        }
                        else
                        {
                            double v = 0;
                            try
                            {
                                v = dValue == null ? 0 : Convert.ToDouble(dValue);
                            }
                            catch
                            {
                                throw new Exception(string.Format("错误的默认值设定 method:{0} parameter:{1}", info.ResourceName, info.ParameterInfos[i].ParameterName));
                            }

                            Label label1 = il.DefineLabel();
                            Label label2 = il.DefineLabel();
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Call, isNullOrEmpty);
                            il.Emit(OpCodes.Brtrue_S, label2);
                            il.Emit(OpCodes.Call, doubleParser);
                            il.Emit(OpCodes.Br_S, label1);
                            il.MarkLabel(label2);
                            il.Emit(OpCodes.Pop);
                            il.Emit(OpCodes.Ldc_R8, v);
                            il.MarkLabel(label1);
                        }                        
                    }
                    else if (info.ParameterInfos[i].ParameterType == typeof(bool))
                    {
                        if (info.ParameterInfos[i].IsRequired)
                        {
                            il.Emit(OpCodes.Call, boolParser);
                        }
                        else
                        {
                            bool v = false;
                            try
                            {
                                v = dValue == null ? false : Convert.ToBoolean(dValue);
                            }
                            catch
                            {
                                throw new Exception(string.Format("错误的默认值设定 method:{0} parameter:{1}", info.ResourceName, info.ParameterInfos[i].ParameterName));
                            }

                            Label label1 = il.DefineLabel();
                            Label label2 = il.DefineLabel();
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Call, isNullOrEmpty);
                            il.Emit(OpCodes.Brtrue_S, label2);
                            il.Emit(OpCodes.Call, boolParser);
                            il.Emit(OpCodes.Br_S, label1);
                            il.MarkLabel(label2);
                            il.Emit(OpCodes.Pop);
                            il.Emit(v ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                            il.MarkLabel(label1);
                        }
                    }
                    else if (info.ParameterInfos[i].ParameterType == typeof(HttpPostedFile))
                    {
                        il.Emit(OpCodes.Call, getHttpContext);
                        il.Emit(OpCodes.Callvirt, getHttpRequest);
                        il.Emit(OpCodes.Callvirt, getRequestFiles);
                        il.Emit(OpCodes.Ldstr, info.ParameterInfos[i].ParameterName);
                        il.Emit(OpCodes.Callvirt, getFileItem);
                    }
                    else
                    {
                        throw new NotSupportedException("Donot support this parameters type in API, which is found in method : " + info.ResourceName);
                    }
                    il.Emit(OpCodes.Stloc, locals[i]);
                    il.Emit(OpCodes.Leave, tryCatchs[i]);
                    il.BeginCatchBlock(typeof(Exception));
                    il.Emit(OpCodes.Ldstr, info.ParameterInfos[i].ParameterName);
                    il.Emit(OpCodes.Ldarg_1);
                    LoadLiteral(il, i);
                    il.Emit(OpCodes.Ldelem_Ref);
                    il.Emit(OpCodes.Ldstr, info.ParameterInfos[i].ParameterType.FullName);
                    il.Emit(OpCodes.Newobj, typeof(RestParameterException).GetConstructor(new Type[] { typeof(string), typeof(string), typeof(string) }));
                    il.Emit(OpCodes.Throw);
                    il.EndExceptionBlock();
                }
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Castclass, info.ProxyMethodInfo.DeclaringType);
                for (int i = 0; i < locals.Length; i++)
                {
                    il.Emit(OpCodes.Ldloc, locals[i]);
                }
                il.Emit(OpCodes.Callvirt, info.ProxyMethodInfo);
                il.Emit(OpCodes.Ret);

                return invoker;
            }

            //API manager 只暴露那些参数均为基本类型的方法
            private static bool CheckMethod(RestMethodInfo info)
            {
                foreach (RestParameterInfo pInfo in info.ParameterInfos)
                {
                    if (!pInfo.ParameterType.IsPrimitive 
                        && (pInfo.ParameterType != typeof(string)) 
                        && (pInfo.ParameterType != typeof(HttpPostedFile)))
                    {
                        throw new Exception("不支持的参数格式 资源名 " + info.ResourceName + " 参数名 " + pInfo.ParameterName);
                    }
                }

                return true;
            }

            private static void LoadLiteral(ILGenerator il, int value)
            {
                switch (value)
                {
                    case -1:
                        il.Emit(OpCodes.Ldc_I4_M1);
                        return;
                    case 0:
                        il.Emit(OpCodes.Ldc_I4_0);
                        return;
                    case 1:
                        il.Emit(OpCodes.Ldc_I4_1);
                        return;
                    case 2:
                        il.Emit(OpCodes.Ldc_I4_2);
                        return;
                    case 3:
                        il.Emit(OpCodes.Ldc_I4_3);
                        return;
                    case 4:
                        il.Emit(OpCodes.Ldc_I4_4);
                        return;
                    case 5:
                        il.Emit(OpCodes.Ldc_I4_5);
                        return;
                    case 6:
                        il.Emit(OpCodes.Ldc_I4_6);
                        return;
                    case 7:
                        il.Emit(OpCodes.Ldc_I4_7);
                        return;
                    case 8:
                        il.Emit(OpCodes.Ldc_I4_8);
                        return;
                }

                if (value > -129 && value < 128)
                {
                    il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
                }
                else
                {
                    il.Emit(OpCodes.Ldc_I4, value);
                }
            }
        }
    }
}