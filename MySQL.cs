using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace SQL
{
    public class MySQL
    {
        private Regex? Checker = new(@"^server=\d{1,4}\.\d{1,4}\.\d{1,4}\.\d{1,4};\s*port=\d{1,5};\s*user=[A-Za-z0-9]+;\s*password=[A-Za-z0-9]+;\s*$");
        private MySqlCommand? Command = null;
        private MySqlConnection? Connection = null;



        private string? config = null;
        /// <summary>
        /// 配置连接参数并自动连接。
        /// </summary>
        public string? Config
        {
            get => config;
            set
            {
                if (value == null)
                {
                    if (showDebugInfo)
                        Console.WriteLine("Config：不允许设置为空值！");
                }
                else if (Checker != null)
                {
                    if (Checker.IsMatch(value))
                        config = value;
                }
            }
        }



        private bool showDebugInfo = true;
        /// <summary>
        /// 控制是否打印调试信息。默认为 <c>true</c>。
        /// </summary>
        public bool ShowDebugInfo
        {
            get => showDebugInfo;
            set
            {
                if (value == true)
                    showDebugInfo = true;
                else if (value == false)
                    showDebugInfo = false;
                else
                {
                    if (showDebugInfo)
                        Console.WriteLine("ShowDebugInfo：非法的值！");
                }
            }
        }




        private bool showExecuteStatus = true;
        /// <summary>
        /// 控制是否打印执行信息。默认为 <c>true</c>。
        /// </summary>
        private bool ShowExecuteStatus
        {
            get => showExecuteStatus;
            set
            {
                if (value == true)
                    showExecuteStatus = true;
                else if (value == false)
                    showExecuteStatus = false;
                else
                {
                    if (showDebugInfo)
                        Console.WriteLine("ShowExecuteStatus：非法的值！");
                }
            }
        }



        /// <summary>
        /// 以不连接到MySQL服务器的方式初始化。
        /// </summary>
        public MySQL()
        { }



        /// <summary>
        /// 以指定详细信息的方式连接到MySQL服务器，并初始化。<br /><br />
        /// 参数格式应该形如：server=地址;port=端口;user=用户名;password=密码;
        /// </summary>
        /// <param name="Config">完整的的参数信息。</param>
        public MySQL(string Config)
        {
            this.Config = Config;
            Link();
        }



        /// <summary>
        /// 以指定详细信息的方式连接到MySQL服务器，并初始化。
        /// </summary>
        /// <param name="Address">MySQL服务器的地址</param>
        /// <param name="Port">MySQL服务器的端口号</param>
        /// <param name="Username">登录用的用户名</param>
        /// <param name="Password">登录用的密码</param>
        public MySQL(string Address, string Port, string Username, string Password)
        {
            Config = "server=" + Address + ";port=" + Port + ";user=" + Username + ";password=" + Password + ";";
            Link();
        }



        /// <summary>
        /// 即便不执行Close()，析构函数也会帮你执行。
        /// </summary>
        ~MySQL()
        {
            Config = null;
            Command = null;
            Checker = null;
            Close();
        }



        /// <summary>
        /// 连接至MySQL服务器。
        /// </summary>
        public void Link()
        {
            if (Connection != null)
            {
                if (ShowDebugInfo) Console.WriteLine("现有连接未关闭！");
            }
            else if (Config == null)
            {
                if (ShowDebugInfo) Console.WriteLine("未设置连接！");
            }
            else
            {
                Connection = new(Config);
                try
                {
                    Connection.Open();
                    if (ShowExecuteStatus) Console.WriteLine("已连接至MySQL服务器");
                }
                catch (Exception Error)
                {
                    if (showDebugInfo)
                    {
                        Console.WriteLine("连接数据库时发生错误！");
                        Console.WriteLine("\t报错信息" + Error.Message);
                    }
                }
            }
        }



        /// <summary>
        /// 在MySQL服务器上执行给定的查询语句。
        /// </summary>
        /// <param name="Command">指定的查询语句</param>
        /// <returns>一个 <c>List&lt;List&lt;string&gt;&gt;</c> 类型的对象。<br />
        /// 其中每一个<c>List&lt;string&gt;</c> 类型的元素存储一行的结果。
        /// </returns>
        public List<List<string>> Query(string Command)
        {
            List<List<string>> Result = new();
            this.Command = new(Command, Connection);
            try
            {
                MySqlDataReader Information = this.Command.ExecuteReader();
                while (Information.Read())
                {
                    List<string> temp = new();
                    for (int i = 0; i < Information.FieldCount; i++)
                    {
                        try
                        {
                            temp.Add(Information.GetString(i));
                        }
                        catch (Exception)
                        {
                            temp.Add("NULL");
                        }
                    }
                    Result.Add(temp);
                }
                Information.Close();
                if (ShowExecuteStatus) Console.WriteLine("查询完毕");
            }
            catch (Exception Error)
            {
                if (ShowDebugInfo)
                {
                    Console.WriteLine("执行查询时出错！");
                    Console.WriteLine("\t报错信息" + Error.Message);
                }
            }
            return Result;
        }



        /// <summary>
        /// 在MySQL服务器上执行给定的操作语句。
        /// </summary>
        /// <param name="Command">指定的操作语句</param>
        /// <returns>执行成功则返回修改的行数，若执行失败则返回-1 。</returns>
        public int Execute(String Command)
        {
            this.Command = new(Command, Connection);
            int Result = -1;
            try
            {
                Result = this.Command.ExecuteNonQuery();
                if (ShowExecuteStatus) Console.WriteLine("执行完毕");
            }
            catch (Exception Error)
            {
                if (ShowDebugInfo)
                {
                    Console.WriteLine("执行操作时出错！");
                    Console.WriteLine("\t报错信息：" + Error.Message);
                }
            }
            return Result;
        }



        /// <summary>
        /// 执行使用数据库命令。
        /// </summary>
        /// <param name="DatabaseName">要使用的数据库名称</param>
        /// <returns>执行成功则返回 <c>true</c> ，否则返回 <c>false</c> 。</returns>
        public bool UseDatabase(string DatabaseName)
        {
            return Execute("USE " + DatabaseName) != -1;
        }



        /// <summary>
        /// 执行创建数据库的命令。
        /// </summary>
        /// <param name="DatabaseName">要创建的数据库名称</param>
        /// <param name="Charset">该数据库使用的编码格式，可以为 <c>null</c></param>
        /// <returns>执行成功则返回 <c>true</c> ，否则返回 <c>false</c> 。</returns>
        public bool CreateDatabase(string DatabaseName, string? Charset)
        {
            string temp = DatabaseName;
            if (Charset != null)
                temp += "CHARACTER SET " + Charset;
            return Execute(temp) != -1;
        }



        /// <summary>
        /// 执行删除数据库的命令。
        /// </summary>
        /// <param name="DatabaseName">要删除的数据库名称</param>
        /// <returns>执行成功则返回 <c>true</c> ，否则返回 <c>false</c> 。</returns>
        public bool DeleteDatabase(string DatabaseName)
        {
            return Execute("DROP DATABASE " + DatabaseName) != -1;
        }



        /// <summary>
        /// 执行创建表的命令。
        /// </summary>
        /// <param name="Table">要创建的表名</param>
        /// <param name="Attributes">各个属性的名称</param>
        /// <param name="Type">各个属性对应的类型</param>
        /// <param name="Constraint">各个属性对应的约束，其中的元素可以为 <c>null</c></param>
        /// <param name="Comment">各个属性对应的注释，其中的元素可以为<c>null</c></param>
        /// <param name="Charset">该表使用的编码格式，可以为 <c>null</c></param>
        /// <returns>执行成功则返回 <c>true</c> ，否则返回 <c>false</c> 。</returns>
        public bool CreateTable(string Table, string[] Attributes, string[] Type, string?[] Constraint, string?[] Comment, string? Charset)
        {
            if (Attributes.Length != Type.Length || Attributes.Length != Constraint.Length || Attributes.Length != Comment.Length)
            {
                if (showDebugInfo)
                {
                    Console.WriteLine("创建表时出错！");
                    Console.WriteLine("\t报错信息：“Attributes” 与 “Type” 长度不一致");
                }
                return false;
            }
            string temp = "";
            for (int i = 0; i < Attributes.Length - 1; i++)
            {
                temp += Attributes[i] + " " + Type[i];
                if (Constraint[i] != null)
                    temp += " " + Constraint[i];
                if (Comment[i] != null)
                    temp += " COMMENT '" + Comment[i] + "'";
                temp += ",";
            }
            temp += Attributes[^1] + " " + Type[^1];
            if (Constraint[^1] != null)
                temp += " " + Constraint[^1];
            if (Comment[^1] != null)
                temp += " COMMENT '" + Comment[^1] + "'";
            if (Charset != null)
                temp = "CREATE TABLE " + Table + " (" + temp + ") DEFAULT CHARSET = " + Charset;
            else
                temp = "CREATE TABLE " + Table + " (" + temp + ")";
            return Execute(temp) != -1;
        }



        /// <summary>
        /// 执行向表中插入数据的命令。
        /// </summary>
        /// <param name="Table">要插入数据的表名</param>
        /// <param name="AttributeNames">要插入的数据的属性名</param>
        /// <param name="AttributeValues">要插入的数据的属性值，不要忘记单引号</param>
        /// <returns>执行成功则返回 <c>true</c> ，否则返回 <c>false</c> 。</returns>
        public bool Insert(string Table, string[] AttributeNames, string[] AttributeValues)
        {
            if (AttributeNames.Length != AttributeValues.Length)
            {
                if (showDebugInfo)
                {
                    Console.WriteLine("插入数据时出错！");
                    Console.WriteLine("\t报错信息：“AttributeNames” 与 “AttributeValues” 长度不一致");
                }
                return false;
            }
            string temp = "INSERT " + Table + " (";
            for (int i = 0; i < AttributeNames.Length - 1; i++)
            {
                temp += AttributeNames[i] + ",";
            }
            temp += AttributeNames[^1] + ") VALUES (";
            for (int i = 0; i < AttributeValues.Length - 1; i++)
            {
                temp += AttributeValues[i] + ",";
            }
            temp += AttributeValues[^1] + ")";
            return Execute(temp) != -1;
        }



        /// <summary>
        /// 执行修改表中数据的命令。
        /// </summary>
        /// <param name="Table">要修改数据的表名</param>
        /// <param name="AttributeNames">要修改的属性名</param>
        /// <param name="AttributeValues">新的属性值</param>
        /// <param name="Conditions">用于筛选要修改的条目的SQL语句</param>
        /// <returns></returns>
        public bool Update(string Table, string[] AttributeNames, string[] AttributeValues, string? Conditions)
        {
            if (AttributeNames.Length != AttributeValues.Length)
            {
                if (showDebugInfo)
                {
                    Console.WriteLine("插入数据时出错！");
                    Console.WriteLine("\t报错信息：“AttributeNames” 与 “AttributeValues” 长度不一致");
                }
                return false;
            }
            string temp = "UPDATE " + Table + " SET ";
            for (int i = 0; i < AttributeNames.Length - 1; i++)
            {
                temp += AttributeNames[i] + "=" + AttributeValues[i] + ",";
            }
            temp += AttributeNames[^1] + "=" + AttributeValues[^1];
            if (Conditions != null)
            {
                temp += " WHERE " + Conditions;
            }
            return Execute(temp) != -1;
        }



        /// <summary>
        /// 判断元素是否在容器内。
        /// </summary>
        /// <param name="Element">给定的元素</param>
        /// <param name="Container">给定的容器</param>
        /// <returns>元素在容器内则返回 <c>true</c>，否则返回 <c>false</c> 。</returns>
        public static bool Exist(string Element, List<List<string>> Container)
        {
            foreach (List<String> i in Container)
            {
                if (i.Contains(Element))
                    return true;
            }
            return false;
        }



        /// <summary>
        /// 以精美的方式打印给定的 <c>List&lt;List&lt;string&gt;&gt;</c> 类型的对象。<br />
        /// 建议用于配合打印 <c>Query()</c> 方法的返回值。
        /// </summary>
        /// <param name="Result">要打印的对象</param>
        /// <param name="ColumnName">每一列的名称</param>
        public static void FormattedPrint(List<List<string>> Result, string[] ColumnName, int[] Area)
        {
            Console.Write("+");
            for (int i = 0; i < Area.Length; i++)
            {
                Console.Write("-".PadRight(Area[i], '-') + "+");
            }
            Console.Write("\n|");
            for (int i = 0; i < ColumnName.Length; i++)
            {
                Console.Write(ColumnName[i]);
                for (int j = 0; j < Area[i] - Length(ColumnName[i]); j++)
                {
                    Console.Write(' ');
                }
                Console.Write("|");
            }
            Console.Write("\n|");
            for (int i = 0; i < Area.Length; i++)
            {
                Console.Write("-".PadRight(Area[i], '-') + "|");
            }
            Console.Write("\n");
            for (int i = 0; i < Result.Count; i++)
            {
                Console.Write("|");
                for (int j = 0; j < Result[i].Count; j++)
                {
                    Console.Write(Result[i][j]);
                    for (int k = 0; k < Area[j] - Length(Result[i][j]); k++)
                    {
                        Console.Write(' ');
                    }
                    Console.Write("|");
                }
                Console.Write("\n");
            }
            Console.Write("+");
            for (int i = 0; i < Area.Length; i++)
            {
                Console.Write("-".PadRight(Area[i], '-') + "+");
            }
            Console.Write("\n");
        }



        /// <summary>
        /// 计算指定的字符串在打印时的占用宽度。
        /// </summary>
        /// <param name="Target">目标字符串</param>
        /// <returns>该字符串打印时占用的宽度。</returns>
        public static int Length(string Target)
        {
            if (Target.Length == 0)
                return 0;

            int RealLength = Target.Length;
            for (int i = 0; i < Target.Length; i++)
            {
                if ((int)Target[i] > 128)
                    RealLength++;
            }

            return RealLength;
        }



        /// <summary>
        /// 关闭与MySQL服务器的连接。
        /// </summary>
        public void Close()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }
            if (ShowExecuteStatus) Console.WriteLine("已关闭连接");
        }
    }
}
