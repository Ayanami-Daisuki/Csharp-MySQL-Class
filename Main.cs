using System;
using System.Collections.Generic;
using SQL;
namespace MainProgram
{
    public class MainProgram
    {
        public static void Main()
        {
            List<List<string>> Result;
            MySQL Target = new("127.0.0.1", "3306", "root", "718293753951");
            Target.ShowDebugInfo = false;
            Console.WriteLine("输出结果为：" + Target.UseDatabase("ctos"));
            Console.WriteLine("输出结果为：" + Target.CreateTable(
                "test",
                new string[] { "age", "name" },
                new string[] { "INT", "VARCHAR(10)" }
                new string?[] { null, "NOT NULL" },
                new string?[] { "年龄", "姓名" },
                "UTF8"));
            Console.WriteLine("Hello, world!");
            MySQL.FormattedPrint(Target.Query("SELECT DATABASE()"), new string[] { "数据库" }, new int[] { 10 });
            Console.ReadLine();

            /* 创建数据库 */

            Result = Target.Query("SHOW DATABASES");
            if (!MySQL.Exist("exp3", Result))
            {
                Target.Execute("CREATE DATABASE exp3");
                MySQL.FormattedPrint(Target.Query("SHOW DATABASES"), new string[] { "数据库名" }, new int[] { 20 });
            }

            /* 创建表 */

            Target.Execute("USE exp3");
            Result = Target.Query("SHOW TABLES");
            if (!MySQL.Exist("users", Result))
            {
                Target.Execute(
                        "CREATE TABLE users(username VARCHAR(10) PRIMARY KEY COMMENT '用户名',pass VARCHAR(8) NOT NULL DEFAULT '888888' COMMENT '密码')");
                MySQL.FormattedPrint(Target.Query("SHOW TABLES"), new string[] { "表名" }, new int[] { 12 });
            }
            if (!MySQL.Exist("person", Result))
            {
                Target.Execute(
                        "CREATE TABLE person(username VARCHAR(10) PRIMARY KEY COMMENT '用户名',name VARCHAR(10) NOT NULL COMMENT '姓名',age INT COMMENT '年龄',teleno CHAR(11) COMMENT '电话',FOREIGN KEY (username) REFERENCES users(username) ON DELETE CASCADE)");
                MySQL.FormattedPrint(Target.Query("SHOW TABLES"), new string[] { "表名" }, new int[] { 12 });
            }

            /* 向表users中插入数据 */

            Target.Execute("INSERT users (username, pass) VALUES ('ly', '123456')");
            Target.Execute("INSERT users (username, pass) VALUES ('liming', '345678')");
            Target.Execute("INSERT users (username, pass) VALUES ('test', '11111')");
            Target.Execute("INSERT users (username, pass) VALUES ('test1', '12345')");

            MySQL.FormattedPrint(Target.Query("SELECT * FROM users"), new string[] { "用户名", "密码" },
                    new int[] { 10, 10 });

            /* 向表person中插入数据(1) */

            Target.Execute("INSERT person (username, name) VALUES ('ly', '雷力')");
            Target.Execute("INSERT person (username, name, age) VALUES ('liming', '李明', 25)");
            Target.Execute("INSERT person (username, name, age, teleno) VALUES ('test', '测试用户', 20, '13388449933')");

            MySQL.FormattedPrint(Target.Query("SELECT * FROM person"),
                    new string[] { "用户名", "姓名", "年龄", "电话号码" },
                    new int[] { 10, 10, 5, 12 });

            /* 向表person中插入数据(2) */

            if (MySQL.Exist("ly", Target.Query("SELECT username FROM users")))
            {
                if (MySQL.Exist("ly", Target.Query("SELECT username FROM person")))
                    Target.Execute("UPDATE person SET name = '王五' WHERE username = 'ly'");
                else
                    Target.Execute("INSERT person (username, name) VALUES ('ly', '王五')");
            }
            else
            {
                Target.Execute("INSERT users (username) VALUES ('ly')");
                Target.Execute("INSERT person (username, name) VALUES ('ly', '王五')");
            }

            if (MySQL.Exist("test2", Target.Query("SELECT username FROM users")))
            {
                if (MySQL.Exist("test2", Target.Query("SELECT username FROM person")))
                    Target.Execute("UPDATE person SET name = '测试用户2' WHERE username = 'test2'");
                else
                    Target.Execute("INSERT person (username, name) VALUES ('test2', '测试用户2')");
            }
            else
            {
                Target.Execute("INSERT users (username) VALUES ('test2')");
                Target.Execute("INSERT person (username, name) VALUES ('test2', '测试用户2')");
            }

            if (MySQL.Exist("test1", Target.Query("SELECT username FROM users")))
            {
                if (MySQL.Exist("test1", Target.Query("SELECT username FROM person")))
                    Target.Execute("UPDATE person SET name='测试用户1',age='33' WHERE username='test1'");
                else
                    Target.Execute("INSERT person (username, name, age) VALUES ('test1', '测试用户1', 33)");
            }
            else
            {
                Target.Execute("INSERT users (username) VALUES ('test1')");
                Target.Execute("INSERT person (username, name, age) VALUES ('test1', '测试用户1', 33)");
            }

            if (MySQL.Exist("test", Target.Query("SELECT username FROM users")))
            {
                if (MySQL.Exist("test", Target.Query("SELECT username FROM person")))
                    Target.Execute("UPDATE person SET name='张三',age=23,teleno='18877009966' WHERE username='test'");
                else
                    Target.Execute("INSERT person (username, name, age, teleno) VALUES ('test', '张三', 23, '18877009966')");
            }
            else
            {
                Target.Execute("INSERT users (username) VALUES ('test')");
                Target.Execute("INSERT person (username, name, age, teleno) VALUES ('test', '张三', 23, '18877009966')");
            }

            if (MySQL.Exist("admin", Target.Query("SELECT username FROM users")))
            {
                if (MySQL.Exist("admin", Target.Query("SELECT username FROM person")))
                    Target.Execute("UPDATE person SET name='admin' WHERE username='admin'");
                else
                    Target.Execute("INSERT person (username, name) VALUES ('admin', 'admin')");
            }
            else
            {
                Target.Execute("INSERT users (username) VALUES ('admin')");
                Target.Execute("INSERT person (username, name) VALUES ('admin', 'admin')");
            }

            MySQL.FormattedPrint(Target.Query("SELECT * FROM users"), new string[] { "用户名", "密码" },
                    new int[] { 10, 10 });

            MySQL.FormattedPrint(Target.Query("SELECT * FROM person"),
                    new string[] { "用户名", "姓名", "年龄", "电话号码" },
                    new int[] { 10, 10, 5, 12 });

            /* 删除数据 */

            Target.Execute("DELETE FROM users WHERE username LIKE 'test%'");

            MySQL.FormattedPrint(Target.Query("SELECT * FROM users"), new string[] {
                "用户名", "密码" },
                    new int[] { 10, 10 });

            MySQL.FormattedPrint(Target.Query("SELECT * FROM person"),
                    new string[] { "用户名", "姓名", "年龄", "电话号码" },
                    new int[] { 10, 10, 5, 12 });
        }
    }
}
