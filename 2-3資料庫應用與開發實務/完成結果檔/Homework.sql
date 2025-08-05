--============================================================�i���Ȥ@�j============================================================================================
--(�@)�򥻬d��

--1	�b�i���u�j��ƪ���X�Ҧ��b1993�~(�t)�H���¾����ơC
SELECT * 
FROM Employees Em
WHERE YEAR(EM.HireDate)>='1993'

--2.�b�i�q��j��ƪ��X�e�f�l���ϸ���44087�P05022��82520����ơC
SELECT * 
FROM Orders O
WHERE O.ShipPostalCode in ('44087','05022','82520')

--3.�b�i���~�j��ƪ���X�w�s�q�̦h���e6�W��ưO���C
SELECT TOP 6 with ties * 
FROM Products P
ORDER BY P.UnitsInStock DESC

--4.�b�i�q��j��ƪ���X�|�����e�f������O����ơC
SELECT * 
FROM Orders O
WHERE O.ShippedDate is null

--5.�b�i�q����ӡj��ƪ���X�q�ʪ����~�ƶq����20~40�󪺸�ưO���C
SELECT * 
FROM OrderDetails OD
WHERE OD.Quantity between 20 and 40

----------------------------------------------------------------------------------------------------------------------------------------
--(�G)�έp�d��

--1.�p��i���~�j��ƪ����O����2�����~��ƥ�������C
SELECT  P.CategoryID, AVG(P.UnitPrice)as average 
FROM Products P
WHERE P.CategoryID=2
GROUP BY P.CategoryID

--2.�b�i���~�j��ƪ���X�w�s�q�p��w���s�q�A�B�|���i����ʪ����~��ưO���C
SELECT  * 
FROM Products P
WHERE P.UnitsInStock<P.ReorderLevel AND P.UnitsOnOrder=0

--3.�b�i�q����ӡj��ƪ��X�q�椤�]�t�W�L5�ز��~����ưO���C
SELECT  OD.OrderID�@, count(*)as amount
FROM OrderDetails OD
GROUP BY OD.OrderID
having count(*)>=5

--4.�b�i�q����ӡj��ƪ���ܥX�q�渹�X10263�Ҧ����~������p�p�C
SELECT�@*, OD.Quantity*OD.UnitPrice*(1-OD.Discount)as SubTotal
FROM OrderDetails OD
WHERE OD.OrderID='10263'

--5.�Q�Ρi���~�j��ƪ��ơA�έp�X�C�@�Ө����ӦU���ѤF�X�˲��~�C
SELECT  P.SupplierID, count(*)as Total
FROM Products P
GROUP BY P.SupplierID

--6.�Q�Ρi�q����ӡj��ƪ��ơA�έp�X�U���ӫ~����������P�����P��ƶq�A�æC�X�����P��ƶq�j��10����ơA�B�N��ƨ̲��~�s���Ѥp��j�ƧǡC
SELECT�@OD.ProductID, AVG(OD.UnitPrice)as AVG_Price, AVG(OD.Quantity)as AVG_Quamtity
FROM OrderDetails OD
GROUP BY OD.ProductID
having AVG(OD.Quantity)>10
ORDER BY OD.ProductID



--============================================================�i���ȤG�j============================================================================================
--1.�иռg�@�X�֬d�ߡA�d�ߥX�q�ʤ�����b1996�~7��ë��w�e�f���q���uUnited Package�v�����q�椧�q�f���Ӹ�ơA
--�æC�X�q�渹�X�B���~���O�W�١B���~�W�١B���~�q�ʳ���B���~�q�ʼƶq�B���~�����p�p�B�Ȥ�s���B�Ȥ�W�١B
--       ���f�H�W�r�B�q�ʤ���B�B�z�q����u���m�W�B�f�B���q�B�����ӦW�ٵ���ƶ��ءA�䤤���~�����p�p�ХH�|�ˤ��J�p��ܾ�Ʀ�C
SELECT�@OD.OrderID,C.CategoryName, P.ProductName, OD.UnitPrice, OD.Quantity, ROUND((OD.Quantity*OD.UnitPrice*(1-OD.Discount)),0)as Subtotal, O.CustomerID, Cum.CompanyName,
		O.ShipName, O.OrderDate, (E.FirstName+' ' +E.LastName)as EmployeeName, S.CompanyName, Sup.CompanyName
FROM OrderDetails OD
INNER JOIN Orders O on OD.OrderID=O.OrderID
INNER JOIN Shippers S on O.ShipVia=S.ShipperID
INNER JOIN Products P on OD.ProductID=P.ProductID
INNER JOIN Categories C on P.CategoryID=C.CategoryID
INNER JOIN Customers Cum on O.CustomerID=Cum.CustomerID
INNER JOIN Employees E on O.EmployeeID=E.EmployeeID
INNer JOIN Suppliers Sup on P.SupplierID=Sup.SupplierID
WHERE O.OrderDate between '1996/7/1' and '1996/7/31' AND S.CompanyName='United Package'


--2.�ЧQ��exists�B��l�t�X�l�d�ߦ��A��X���ǫȤ�q���U�L�q��A�æC�X�Ȥ᪺�Ҧ����C(���i�Ψ�in�B��A�礣�i�ΦX�֬d�ߦ�)
SELECT *
FROM Customers C
WHERE not exists(SELECT * FROM Orders O WHERE C.CustomerID=O.CustomerID)


--3. �ЧQ��in�B��l�t�X�l�d�ߦ��A�d�߭��ǭ��u���B�z�L�q��A�æC�X���u�����u�s���B�m�W�B¾�١B�����������X�B�������C(���i�Ψ�exists�B��A�礣�i�ΦX�֬d�ߦ�) 
SELECT E.EmployeeID, (E.FirstName+' ' +E.LastName)as EmployeeName, E.Title, E.Extension, E.Notes
FROM Employees E
WHERE E.EmployeeID in (SELECT O.EmployeeID FROM Orders O )

--4. �ЦX�֬d�߻P�l�d�ߨ�ؼg�k�A�C�X1998�~�שҦ��Q�q�ʹL�����~��ơA�æC�X���~���Ҧ����A�B�̲��~�s���Ѥp��j�ƧǡC
--(�g�X�֬d�߮ɤ��o�Υ���l�d�ߦ��A�g�l�d�߮ɤ��o�Υ���X�֬d�ߦ�)
--�X�֬d�ߡG
SELECT DISTINCT P.*
FROM Products P
INNER JOIN OrderDetails OD on P.ProductID=OD.ProductID
INNER JOIN Orders O on OD.OrderID=O.OrderID
WHERE year(O.OrderDate)='1998'
ORDER BY P.ProductID

--�l�d�ߡG
SELECT DISTINCT P.*
FROM Products P
WHERE P.ProductID in (
		SELECT OD.ProductID 
		FROM OrderDetails OD 
		WHERE OD.OrderID in(
			SELECT O.OrderID 
			FROM Orders O 
			WHERE year(O.OrderDate)='1998')) 
ORDER BY P.ProductID




--============================================================�i���ȤT�j============================================================================================
--1.�мg�X�إߤ@�ӦW���iMySchool�j��Ʈw��SQL DDL Script�C
create database MySchool
go

--2.�аѦ�ER�ϤΤU�C��ƪ�W��A�g�X�۹�����SQL DDL Script�A�Ϩ�i��iMySchool�j��Ʈw���إ߳o�Ǹ�ƪ�C
create table [Student](
	StuID nchar(10) not null primary key,
	StuName nvarchar(20) not null,
	Tel nvarchar(20) not null,
	Address nvarchar(100),
	Birthday datetime, 
	DeptID nchar(1) not null,
	foreign key (DeptID) references [Department](DeptID)
);

create table [Course](
	CourseID nchar(5) not null primary key,
	CourseName nvarchar(30) not null,
	Credit int not null default 0,
	Hour int not null default 2,
	DeptID nchar(1) not null,
	foreign key (DeptID) references [Department](DeptID)
);

create table [SelectionDetail](
	StuID nchar(10) not null,
	CourseID nchar(5) not null,
	Year int not null default (Year(Getdate())),
	Term tinyint not null,
	Score int not null default 0,
	primary key(StuID,CourseID),
	foreign key (StuID) references [Student](StuID),
	foreign key (CourseID) references [Course](CourseID)
);

create table [Department](
	DeptID nchar(1) not null primary key,
	DeptName nvarchar(30) not null unique
);


--============================================================�i���ȥ|�j============================================================================================
--�ЧQ�Υ��ȤT�ҫإߪ��iMySchool�j��Ʈw�A�إߤ@�ӦW���uInsertDeptmentData�v���w�s�{�ǡA
--�s�W��t��Ʈɥ����I�s���榹�w�s�{�Ƕi���Ʒs�W�C
use MySchool
go
create proc InsertDeptmentData 
 @id nchar(1),
 @name nvarchar(30)
as
begin
	declare @i int
	declare @j int
	--1.�w�s�{�Ǥ����ˬd��t�N�X(DeptID)�ά�t�W��(DeptName)�O�_�w�b��Ʈw���C
	Select @i = count(*) from Department D where D.DeptID=@id 
	Select @j = count(*) from Department D where D.DeptName=@name

	if @i <> 0 or  @j <> 0
		--2.�Y���s�W����ƭ��ˬd��w�Q�ϥΡA�h��X���������~�T���B���i��Insert�ʧ@�C
		print 'DeptID or DeptName is exist'
	else
		begin
			--3.�Y���s�W����ƭȬҥ��Q�ϥΡA�h�i��Insert�ʧ@�N��Ƽg�J��Ʈw�C
			INSERT INTO Department values(@id, @name)
		end
end
go
-------------------------------
--����
--exec InsertDeptmentData 'A','Business Administration'



--============================================================�i���Ȥ��j============================================================================================
--�ЧQ�Υ��ȤT�ҫإߪ��iMySchool�j��Ʈw�A�إߤ@�ӦW���ugetCourseID�v���ۭq��ơA
--��\�ର�s�W�ҵ{��Ʈɥi�I�s����Ʀ۰ʨ��o�@�ӷs���ҵ{�s���CCourseID���s�X�W�h���^��r��C�[�W�}�Ҭ�t�N�X�A�[�W3�X�y�����C
--(�Ҧp��t�N�X��G�}����123���ҵ{�ACourseID��CG123�A��t�N�X��B�}����1���ҵ{�ACourseID��CB001)�C
use MySchool
go
alter function getCourseID(@DeptID nchar(1))
	returns nchar(5) 
as 
begin
	declare @i char(3)
	Select @i=right('000'+ CAST(count(*)+1 as nvarchar),3) from Course where DeptID=@DeptID



	return 'C'+@DeptID+@i
end
go
------------------
--����
Insert into Course values(dbo.getCourseID('A'), '�|�p��(�@)', 3, 3, 'A')
Insert into Course values(dbo.getCourseID('A'), '�|�p��(�G)', 3, 3, 'A')

select * from Course

