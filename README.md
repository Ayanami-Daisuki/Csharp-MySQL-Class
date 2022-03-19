<h1 align="center">MySQL包装类</h1>

一个自己写的适用于MySQL数据库的C#包装类。
<font color="red">源代码中已经包含各个成员的完整说明。</font>

<h2>非静态成员变量</h2>

* <code>Checker</code> ，用于检测传入的配置字符串是否合法的正则表达式对象。<font color="red">它是私有的且无相应的 <code>set</code> 方法，不允许在外部更改。</font>
* <code>Command</code> ，保存SQL语句的对象。<font color="red">它是私有的且无相应的 <code>set</code> 方法，不允许在外部更改。</font>
* <code>Connection</code> ，连接至ySQL的对象。<font color="red">它是私有的且无相应的 <code>set</code> 方法，不允许在外部更改。</font>
* <code>Config</code> ，保存连接至MySQL的配置信息的字符串。

>配置字符串的格式应该形如："server=地址;port=端口;user=用户名;password=密码;"。

* <code>ShowDebugInfo</code> ，控制是否要显示调试信息。默认值为 <code>true</code> 。
* <code>ShowExecuteStatus</code> ，控制是否要显示执行状态。默认值为 <code>true</code> 。

<h2>构造函数</h2>

| 参数类型 | 参数意义 | 说明 |
| :---: | :---: | :---: |
| 无 | 无 | 以不连接到MySQL服务器的方式初始化对象|
| <code>string</code> | 配置字符串 | 以指定配置字符串的方式连接到MySQL服务器，并初始化 |
| <code>string, string, string, string</code> | MySQL服务器地址，端口号，用户名，密码 | 以指定参数的方式连接到MySQL数据库，并初始化 |

<h2>成员方法</h2>

| 方法名 | <code>static</code> | 参数类型 | 参数意义 | 说明 | 返回值类型 | 返回值意义 |
| :---: |  :---: | :---: | :---: | :---: | :---: | :---: |
| <code>Link()</code> | 否 | 无 | 无 | 连接至MySQL数据库。<font color="red">在连接之前你应该先设置好 <code>Config</code> 成员变量。</font> | 无 | 无 |
| <code>Query()</code> | 否 | <code>string</code> | SQL查询语句 | 执行查询语句，并返回结果。 | <code>List&lt;List&lt;string&gt;&gt;</code> | 查询的结果。保存的 <code></code> 元素保存的是每一行的结果。 |
| <code>Execute()</code> | 否 | <code>string</code> | SQL语句 | 执行SQL语句，并返回修改的行数。 | <code>int</code> | 修改的行数，若执行失败则返回-1。 |
| <code>Exist()</code> | <font color="red">是</font> | <code>string, List&lt;List&lt;string&gt;&gt;</code> | 待查询的元素，容器 | 查找指定的元素是否位于容器内 | <code>bool</code> | 若在容器内则返回 <code>true</code> ，否则返回 <code>false</code> |
| <code>FormattedPrint()</code> |  <font color="red">是</font> | <code>List&lt;string&gt;, string[], int[]</code> | 待打印的查询结果，每一列的名称，每一列占据的字节宽度 | 以精美的表格形式在控制台输出查询结果 | 无 | 无 |
| <code>Length()</code> |  <font color="red">是</font> | <code>string</code> | 待计算的字符串 | 计算字符串在打印时占据的字节宽度 | <code>int</code> | 该字符串在打印时占据的字节宽度 |
| <code>Close</code> | 否 | 无 | 无 | 关闭与MySQL数据库的连接 | 无 | 无 |
