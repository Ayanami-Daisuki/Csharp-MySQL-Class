<h1 align="center">MySQL��װ��</h1>

һ���Լ�д��������MySQL���ݿ��C#��װ�ࡣ
<font color="red">Դ�������Ѿ�����������Ա������˵����</font>

<h2>�Ǿ�̬��Ա����</h2>

* <code>Checker</code> �����ڼ�⴫��������ַ����Ƿ�Ϸ���������ʽ����<font color="red">����˽�е�������Ӧ�� <code>set</code> ���������������ⲿ���ġ�</font>
* <code>Command</code> ������SQL���Ķ���<font color="red">����˽�е�������Ӧ�� <code>set</code> ���������������ⲿ���ġ�</font>
* <code>Connection</code> ��������ySQL�Ķ���<font color="red">����˽�е�������Ӧ�� <code>set</code> ���������������ⲿ���ġ�</font>
* <code>Config</code> ������������MySQL��������Ϣ���ַ�����

>�����ַ����ĸ�ʽӦ�����磺"server=��ַ;port=�˿�;user=�û���;password=����;"��

* <code>ShowDebugInfo</code> �������Ƿ�Ҫ��ʾ������Ϣ��Ĭ��ֵΪ <code>true</code> ��
* <code>ShowExecuteStatus</code> �������Ƿ�Ҫ��ʾִ��״̬��Ĭ��ֵΪ <code>true</code> ��

<h2>���캯��</h2>

| �������� | �������� | ˵�� |
| :---: | :---: | :---: |
| �� | �� | �Բ����ӵ�MySQL�������ķ�ʽ��ʼ������|
| <code>string</code> | �����ַ��� | ��ָ�������ַ����ķ�ʽ���ӵ�MySQL������������ʼ�� |
| <code>string, string, string, string</code> | MySQL��������ַ���˿ںţ��û��������� | ��ָ�������ķ�ʽ���ӵ�MySQL���ݿ⣬����ʼ�� |

<h2>��Ա����</h2>

| ������ | <code>static</code> | �������� | �������� | ˵�� | ����ֵ���� | ����ֵ���� |
| :---: |  :---: | :---: | :---: | :---: | :---: | :---: |
| <code>Link()</code> | �� | �� | �� | ������MySQL���ݿ⡣<font color="red">������֮ǰ��Ӧ�������ú� <code>Config</code> ��Ա������</font> | �� | �� |
| <code>Query()</code> | �� | <code>string</code> | SQL��ѯ��� | ִ�в�ѯ��䣬�����ؽ���� | <code>List&lt;List&lt;string&gt;&gt;</code> | ��ѯ�Ľ��������� <code></code> Ԫ�ر������ÿһ�еĽ���� |
| <code>Execute()</code> | �� | <code>string</code> | SQL��� | ִ��SQL��䣬�������޸ĵ������� | <code>int</code> | �޸ĵ���������ִ��ʧ���򷵻�-1�� |
| <code>Exist()</code> | <font color="red">��</font> | <code>string, List&lt;List&lt;string&gt;&gt;</code> | ����ѯ��Ԫ�أ����� | ����ָ����Ԫ���Ƿ�λ�������� | <code>bool</code> | �����������򷵻� <code>true</code> �����򷵻� <code>false</code> |
| <code>FormattedPrint()</code> |  <font color="red">��</font> | <code>List&lt;string&gt;, string[], int[]</code> | ����ӡ�Ĳ�ѯ�����ÿһ�е����ƣ�ÿһ��ռ�ݵ��ֽڿ�� | �Ծ����ı����ʽ�ڿ���̨�����ѯ��� | �� | �� |
| <code>Length()</code> |  <font color="red">��</font> | <code>string</code> | ��������ַ��� | �����ַ����ڴ�ӡʱռ�ݵ��ֽڿ�� | <code>int</code> | ���ַ����ڴ�ӡʱռ�ݵ��ֽڿ�� |
| <code>Close</code> | �� | �� | �� | �ر���MySQL���ݿ������ | �� | �� |
