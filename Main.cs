using System;
using SQL;
namespace MainProgram
{
    public class MainProgram
    {
        public static void Main()
        {
            /* 连接至数据库 */
            MySQL Database = new("127.0.0.1", "3306", "root", "718293753951");
            Database.ShowDebugInfo = true;
            Database.ShowExecuteStatus = false;

            /* 创建spj数据库,并使用之 */
            Database.Execute("CREATE DATABASE spj");
            MySQL.FormattedPrint(Database.Query("SHOW DATABASES"), new string[] { "数据库名" });

            Database.Execute("USE spj");
            MySQL.FormattedPrint(Database.Query("SELECT DATABASE()"), new string[] { "当前使用的数据库" });

            Console.ReadKey(false);


            /* 创建4个表 */
            Database.Execute("CREATE TABLE s (" +
                                "sno CHAR(5) PRIMARY KEY COMMENT '供应商代码'," +
                                "sname VARCHAR(20) NOT NULL COMMENT '供应商名称'," +
                                "status INT UNSIGNED NOT NULL COMMENT '供应商状态'," +
                                "city CHAR(10) NOT NULL COMMENT '供应商所在城市'" +
                            ") CHARSET = UTF8");
            Database.Execute("CREATE TABLE p (" +
                                "pno CHAR(5) PRIMARY KEY COMMENT '零件代码'," +
                                "pname VARCHAR(20) NOT NULL COMMENT '零件名'," +
                                "color CHAR(5) NOT NULL COMMENT '颜色'," +
                                "weight INT UNSIGNED NOT NULL COMMENT '重量'" +
                            ") CHARSET = UTF8");
            Database.Execute("CREATE TABLE j (" +
                                "jno CHAR(5) PRIMARY KEY COMMENT '工程项目代码'," +
                                "JNAME VARCHAR(20) NOT NULL COMMENT '工程项目名'," +
                                "city CHAR(10) NOT NULL COMMENT '工程项目所在城市'" +
                            ") CHARSET = UTF8");
            Database.Execute("CREATE TABLE spj (" +
                                "sno CHAR(5) COMMENT '供应商代码'," +
                                "pno CHAR(5) COMMENT '零件代码'," +
                                "jno CHAR(5) COMMENT '工程项目代码'," +
                                "qty INT UNSIGNED NOT NULL COMMENT '供应数量'," +
                                "FOREIGN KEY(sno) REFERENCES S(sno)," +
                                "FOREIGN KEY(pno) REFERENCES P(pno)," +
                                "FOREIGN KEY(jno) REFERENCES J(jno)," +
                                "PRIMARY KEY (sno, pno, jno)" +
                            ") CHARSET = UTF8");
            MySQL.FormattedPrint(Database.Query("SHOW TABLES"), new string[] { "表名" });

            Console.ReadKey(false);


            /* 插入数据 S表 */
            Database.Execute("INSERT s (sno, sname, status, city) VALUES ('S1', '精益', 20, '天津')");
            Database.Execute("INSERT s (sno, sname, status, city) VALUES ('S2', '盛锡', 10, '北京')");
            Database.Execute("INSERT s (sno, sname, status, city) VALUES ('S3', '东方红', 30, '北京')");
            Database.Execute("INSERT s (sno, sname, status, city) VALUES ('S4', '丰泰盛', 20, '天津')");
            Database.Execute("INSERT s (sno, sname, status, city) VALUES ('S5', '为民', 30,'上海')");

            /* 插入数据 P表 */
            Database.Execute("INSERT p (pno, pname, color, weight) VALUES ('P1', '螺母', '红', '12')");
            Database.Execute("INSERT p (pno, pname, color, weight) VALUES ('P2', '螺栓', '绿', '17')");
            Database.Execute("INSERT p (pno, pname, color, weight) VALUES ('P3', '螺丝刀', '蓝', '14');");
            Database.Execute("INSERT p (pno, pname, color, weight) VALUES ('P4', '螺丝刀', '红', '14')");
            Database.Execute("INSERT p (pno, pname, color, weight) VALUES ('P5', '凸轮', '蓝', '40')");
            Database.Execute("INSERT p (pno, pname, color, weight) VALUES ('P6', '齿轮', '红', '30')");

            /* 插入数据 J表*/
            Database.Execute("INSERT j (jno, JNAME, city) VALUES ('J1', '三建', '北京')");
            Database.Execute("INSERT j (jno, JNAME, city) VALUES ('J2', '一汽', '长春')");
            Database.Execute("INSERT j (jno, JNAME, city) VALUES ('J3', '弹簧厂', '天津')");
            Database.Execute("INSERT j (jno, JNAME, city) VALUES ('J4', '造船厂', '天津')");
            Database.Execute("INSERT j (jno, JNAME, city) VALUES ('J5', '机车厂', '唐山')");
            Database.Execute("INSERT j (jno, JNAME, city) VALUES ('J6', '无线电厂', '常州')");
            Database.Execute("INSERT j (jno, JNAME, city) VALUES ('J7', '半导体厂', '南京')");

            /* 插入数据 spj表*/
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S1', 'P1', 'J1', 200)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S1', 'P1', 'J3', 100)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S1', 'P1', 'J4', 700)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S1', 'P2', 'J2', 100)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S2', 'P3', 'J1', 400)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S2', 'P3', 'J2', 200)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S2', 'P3', 'J4', 500)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S2', 'P3', 'J5', 400)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S2', 'P5', 'J1', 400)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S2', 'P5', 'J2', 100)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S3', 'P1', 'J1', 200)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S3', 'P3', 'J1', 200)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S4', 'P5', 'J1', 100)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S4', 'P6', 'J3', 300)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S4', 'P6', 'J4', 200)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S5', 'P2', 'J4', 100)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S5', 'P3', 'J1', 200)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S5', 'P6', 'J2', 200)");
            Database.Execute("INSERT spj (sno, pno, jno, qty) VALUES ('S5', 'P6', 'J4', 500)");

            MySQL.FormattedPrint(Database.Query("SELECT * FROM S"), new string[] { "供应商代码", "供应商名", "供应商状态", "供应商所在城市" });
            MySQL.FormattedPrint(Database.Query("SELECT * FROM p"), new string[] { "零件代码", "零件名", "颜色", "重量" });
            MySQL.FormattedPrint(Database.Query("SELECT * FROM j"), new string[] { "工程代码", "工程名", "工程所在城市" });
            MySQL.FormattedPrint(Database.Query("SELECT * FROM spj"), new string[] { "供应商代码", "零件代码", "工程代码", "供应数量" });

            Console.ReadKey(false);


            /* 4.(1)  查询供应工程J1零件的供应商号码SNO */
            Console.WriteLine("4.(1)  查询供应工程J1零件的供应商号码SNO");
            MySQL.FormattedPrint(
                Database.Query("SELECT DISTINCT sno FROM spj WHERE jno='J1'"),
                new String[] { "SNO" });
            Console.ReadKey(false);

            /* 4.(2)  查询供应工程J1零件P1的供应商号码SNO */
            Console.WriteLine("4.(2)  查询供应工程J1零件P1的供应商号码SNO");
            MySQL.FormattedPrint(
                Database.Query("SELECT DISTINCT sno FROM spj WHERE jno='J1' AND pno='P1'"),
                new String[] { "SNO" });
            Console.ReadKey(false);

            /* 4.(3)  查询供应工程J1零件为红色的供应商号码SNO */
            Console.WriteLine("4.(3)  查询供应工程J1零件为红色的供应商号码SNO");
            MySQL.FormattedPrint(
                Database.Query("SELECT DISTINCT spj.sno FROM spj,p " +
                               "WHERE spj.pno=p.pno " +
                                     "AND spj.jno='J1' " +
                                     "AND p.color='红'"),
                    new String[] { "SNO" });
            Console.ReadKey(false);

            /* 4.(4)  查询没有使用天津供应商生产的红色零件的工程号JNO */
            Console.WriteLine("4.(4)  查询没有使用天津供应商生产的红色零件的工程号JNO");
            MySQL.FormattedPrint(
                Database.Query("SELECT jno FROM j " +
                                "WHERE jno NOT IN (" +
                                    "SELECT DISTINCT spj.jno FROM spj,s,p " +
                                    "WHERE spj.sno=s.sno " +
                                        "AND spj.pno=p.pno " +
                                        "AND p.color='红' " +
                                        "AND s.city='天津')"),
                    new string[] { "JNO" });
            Console.ReadKey(false);

            /* 4.(5)  查询供应商S1所供应的所有工程号JNO */
            Console.WriteLine("4.(5)  查询供应商S1所供应的所有工程号JNO");
            MySQL.FormattedPrint(
                Database.Query("SELECT DISTINCT jno FROM spj " +
                               "WHERE sno='s1'"
                ), new string[] { "JNO" }); ;
            Console.ReadKey(false);

            /* 5.(1)  查询所有供应商的名称和所在城市 */
            Console.WriteLine("5.(1)  查询所有供应商的名称和所在城市");
            MySQL.FormattedPrint(
                Database.Query("SELECT sname,city FROM s"),
                new string[] { "供应商名", "供应商所在城市" }
                );
            Console.ReadKey(false);

            /* 5.(2)  查询所有零件的名称、颜色、重量 */
            Console.WriteLine("5.(2)  查询所有零件的名称、颜色、重量");
            MySQL.FormattedPrint(
                Database.Query("SELECT pname,color,weight FROM p"),
                new string[] { "零件名称", "零件颜色", "零件重量" });
            Console.ReadKey(false);

            /* 5.(3)  查询使用供应商S1所供应零件的工程号码 */
            Console.WriteLine("5.(3)  查询使用供应商S1所供应零件的工程号码");
            MySQL.FormattedPrint(
                Database.Query("SELECT DISTINCT jno FROM spj " +
                               "WHERE sno='s1'"
                ), new string[] { "JNO" }); ;
            Console.ReadKey(false);

            /* 5.(4)  查询工程J2使用的各种零件的名称、数量 */
            Console.WriteLine("5.(4)  查询工程J2使用的各种零件的名称、数量");
            MySQL.FormattedPrint(
                Database.Query("SELECT p.pname,spj.qty FROM p,spj " +
                               "WHERE p.pno=spj.pno AND spj.jno='J2'"),
                new string[] { "零件名称", "零件数量" });
            Console.ReadKey(false);

            /* 5.(5)  查询上海厂商供应的所有零件号码 */
            Console.WriteLine("5.(5)  查询上海厂商供应的所有零件号码");
            MySQL.FormattedPrint(
                Database.Query("SELECT DISTINCT spj.pno FROM spj,s WHERE s.sno=spj.sno AND s.city='上海'"),
                new string[] { "零件号码" });
            Console.ReadKey(false);

            /* 5.(6)  查询使用上海产的零件的工程名称 */
            Console.WriteLine("5.(6)  查询使用上海产的零件的工程名称");
            MySQL.FormattedPrint(
                Database.Query("SELECT DISTINCT j.jname FROM j,spj,s " +
                               "WHERE s.sno=spj.sno AND j.jno=spj.jno AND s.city='上海'"),
                new string[] { "工程名称" });
            Console.ReadKey(false);

            /* 5.(7)  查询没有使用天津产的零件的工程号 */
            Console.WriteLine("5.(7)  查询没有使用天津产的零件的工程号");
            MySQL.FormattedPrint(
                Database.Query("SELECT jno FROM j WHERE jno NOT IN (" +
                                "SELECT DISTINCT spj.jno FROM spj,j,s " +
                                "WHERE spj.jno=j.jno AND spj.sno=s.sno AND s.city='天津')"),
                new string[] { "工程号" });
            Console.ReadKey(false);

            /* 5.(8)  将所有的红色零件改为蓝色 */
            Console.WriteLine("5.(8)  将所有的红色零件改为蓝色");

            Console.WriteLine("更改前");
            MySQL.FormattedPrint(
                Database.Query("SELECT * FROM p"),
                new string[] { "零件代码", "零件名", "颜色", "重量" });

            Database.Execute("UPDATE p SET color='蓝' WHERE color='红'");

            Console.WriteLine("更改后");
            MySQL.FormattedPrint(
                Database.Query("SELECT * FROM p"),
                new string[] { "零件代码", "零件名", "颜色", "重量" });

            Console.ReadKey(false);

            /* 5.(9)  由S5供给J4的零件P6改为由S3供应 */
            Console.WriteLine("5.(9)  由S5供给J4的零件P6改为由S3供应");

            Console.WriteLine("更改前");
            MySQL.FormattedPrint(
                Database.Query("SELECT * FROM spj WHERE jno='J4' AND pno='P6'"),
                new string[] { "SNO", "PNO", "JNO", "QTY" });

            Database.Execute("UPDATE spj SET sno='S3' WHERE sno='S5' AND jno='J4' AND pno='P6'");

            Console.WriteLine("更改后");
            MySQL.FormattedPrint(
                Database.Query("SELECT * FROM spj WHERE jno='J4' AND pno='P6'"),
                new string[] { "SNO", "PNO", "JNO", "QTY" });

            Console.ReadKey(false);

            /* 5.(10) 从供应商中删除S2，并从供应关系中删除相应的记录 */
            Console.WriteLine("5.(10) 从供应商中删除S2，并从供应关系中删除相应的记录");

            Console.WriteLine("删除前");
            MySQL.FormattedPrint(Database.Query("SELECT * FROM spj"), new string[] { "供应商代码", "零件代码", "工程代码", "供应数量" });
            MySQL.FormattedPrint(Database.Query("SELECT * FROM S"), new string[] { "供应商代码", "供应商名", "供应商状态", "供应商所在城市" });

            Database.Execute("DELETE FROM spj WHERE sno='s2'");
            Database.Execute("DELETE FROM s WHERE sno='s2'");

            Console.WriteLine("删除后");
            MySQL.FormattedPrint(Database.Query("SELECT * FROM spj"), new string[] { "供应商代码", "零件代码", "工程代码", "供应数量" });
            MySQL.FormattedPrint(Database.Query("SELECT * FROM S"), new string[] { "供应商代码", "供应商名", "供应商状态", "供应商所在城市" });

            Console.ReadKey(false);

            /* 5.(11) 插入（S2,J6,P4,200） */
            Console.WriteLine("5.(11) 插入（S2,J6,P4,200）");

            Console.WriteLine("增加前");
            MySQL.FormattedPrint(Database.Query("SELECT * FROM spj WHERE sno='S2'"),
                new string[] { "供应商代码", "零件代码", "工程代码", "供应数量" });

            Database.Execute("INSERT s (sno, sname, status, city) VALUES ('S2', '盛锡', 10, '北京')");
            Database.Execute("INSERT spj (sno,jno,pno,qty) VALUES ('S2','J6','P4',200)");

            Console.WriteLine("增加后");
            MySQL.FormattedPrint(Database.Query("SELECT * FROM spj WHERE sno='S2'"),
                new string[] { "供应商代码", "零件代码", "工程代码", "供应数量" });

            Console.ReadKey(false);

            /* 9.  建立三建工程项目的供应情况视图 */
            Console.WriteLine("9.  建立三建工程项目的供应情况视图");
            Database.Execute("CREATE VIEW sj AS " +
                "SELECT p.*,spj.qty,s.* FROM s,p,j,spj " +
                "WHERE s.sno=spj.sno AND p.pno=spj.pno AND spj.jno=j.jno AND jname='三建'");
            MySQL.FormattedPrint(
                Database.Query("SELECT * FROM sj"),
                new string[] { "零件代号", "零件名称", "零件颜色", "零件重量", "零件数量", "供应商代码", "供应商名称", "供应商状态", "供应商所在地" });
            Console.ReadKey(false);

            /* 9.(1)  找出三建工程使用的各种零件代码、零件数量 */
            Console.WriteLine("9.(1)  找出三建工程使用的各种零件代码、零件数量");
            MySQL.FormattedPrint(
                Database.Query("SELECT pno,qty FROM sj"),
                new string[] { "零件代码", "零件数量" });
            Console.ReadKey(false);

            /* 9.(2)  找出供应商S1的供应情况 */
            Console.WriteLine("9.(2)  找出供应商S1的供应情况");
            MySQL.FormattedPrint(
                Database.Query("SELECT pno,pname,weight,color,qty,status FROM sj " +
                                "WHERE sno='S1'"),
                new string[] { "零件代码", "零件名称", "零件重量", "零件颜色", "供应数量", "供应商状态" });
            Console.ReadKey();
        }
    }
}