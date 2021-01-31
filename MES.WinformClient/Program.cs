using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Grpc.Core;

namespace MES.WinformClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                AppDomain.CurrentDomain.AssemblyResolve += Resolver;
                LoadApp();
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);
                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadApp()
        {
            //Perform dependency check to make sure all relevant resources are in our output directory.
            var settings = new CefSettings();

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            var browser = new Form1();
            Application.Run(browser);
        }

        // Will attempt to load missing assembly from either x86 or x64 subdir
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    Environment.Is64BitProcess ? "x64" : "x86",
                    assemblyName);

                return File.Exists(archSpecificPath)
                    ? Assembly.LoadFile(archSpecificPath)
                    : null;
            }

            return null;
        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            if (ex is Grpc.Core.RpcException  gRpcException)
            {
                sb.AppendLine("****************************异常信息****************************");
                sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
                if (gRpcException != null)
                {
                    sb.AppendLine("【异常类型】：" + gRpcException.Status.StatusCode);
                    switch (gRpcException.StatusCode)
                    {
                        case StatusCode.Cancelled:
                            sb.AppendLine("【异常信息】：操作被取消");
                            break;
                        case StatusCode.Unknown:
                            sb.AppendLine("【异常信息】：未知的错误");
                            break;
                        case StatusCode.InvalidArgument:
                            sb.AppendLine("【异常信息】：客户端指定了无效的参数");
                            break;
                        case StatusCode.DeadlineExceeded:
                            sb.AppendLine("【异常信息】：操作完成前截止日期已过期");
                            break;
                        case StatusCode.NotFound:
                            sb.AppendLine("【异常信息】：请求的实体(例如，文件或目录)没有找到");
                            break;
                        case StatusCode.AlreadyExists:
                            sb.AppendLine("【异常信息】：试图创建的实体(例如，文件或目录)已经存在");
                            break;
                        case StatusCode.PermissionDenied:
                            sb.AppendLine("【异常信息】：没有权限执行指定的操作");
                            break;
                        case StatusCode.Unauthenticated:
                            sb.AppendLine("【异常信息】：未认证/授权");
                            break;
                        case StatusCode.ResourceExhausted:
                            sb.AppendLine("【异常信息】：服务器资源已经耗尽");
                            break;
                        case StatusCode.FailedPrecondition:
                            sb.AppendLine("【异常信息】：操作被拒绝，因为系统没有处于执行操作所需的状态");
                            break;
                        case StatusCode.Aborted:
                            sb.AppendLine("【异常信息】：操作被中止");
                            break;
                        case StatusCode.OutOfRange:
                            sb.AppendLine("【异常信息】：操作尝试超过有效范围");
                            break;
                        case StatusCode.Unimplemented:
                            sb.AppendLine("【异常信息】：此服务中未实现或不支持/启用操作");
                            break;
                        case StatusCode.Internal:
                            sb.AppendLine("【异常信息】：内部错误");
                            break;
                        case StatusCode.Unavailable:
                            sb.AppendLine("【异常信息】：服务不可用");
                            break;
                        case StatusCode.DataLoss:
                            sb.AppendLine("【异常信息】：不可恢复的数据丢失或损坏");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                   
                   
                }
                else
                {
                    sb.AppendLine("【未处理异常】：" + backStr);
                }
                sb.AppendLine("***************************************************************");
            }
            else
            {
                sb.AppendLine("****************************异常信息****************************");
                sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
                if (ex != null)
                {
                    sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                    sb.AppendLine("【异常信息】：" + ex.Message);
                    //sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                }
                else
                {
                    sb.AppendLine("【未处理异常】：" + backStr);
                }
                sb.AppendLine("***************************************************************");
            }

           
           
            return sb.ToString();
        }
    }
}
