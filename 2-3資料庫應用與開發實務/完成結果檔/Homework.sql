--============================================================【任務一】============================================================================================
--(一)基本查詢

--1	在【員工】資料表中找出所有在1993年(含)以後到職的資料。
SELECT * 
FROM Employees Em
WHERE YEAR(EM.HireDate)>='1993'

--2.在【訂單】資料表找出送貨郵遞區號為44087與05022及82520的資料。
SELECT * 
FROM Orders O
WHERE O.ShipPostalCode in ('44087','05022','82520')

--3.在【產品】資料表中找出庫存量最多的前6名資料記錄。
SELECT TOP 6 with ties * 
FROM Products P
ORDER BY P.UnitsInStock DESC

--4.在【訂單】資料表中找出尚未有送貨日期的記錄資料。
SELECT * 
FROM Orders O
WHERE O.ShippedDate is null

--5.在【訂單明細】資料表中找出訂購的產品數量介於20~40件的資料記錄。
SELECT * 
FROM OrderDetails OD
WHERE OD.Quantity between 20 and 40

----------------------------------------------------------------------------------------------------------------------------------------
--(二)統計查詢

--1.計算【產品】資料表中類別號為2的產品資料平均單價。
SELECT  P.CategoryID, AVG(P.UnitPrice)as average 
FROM Products P
WHERE P.CategoryID=2
GROUP BY P.CategoryID

--2.在【產品】資料表中找出庫存量小於安全存量，且尚未進行採購的產品資料記錄。
SELECT  * 
FROM Products P
WHERE P.UnitsInStock<P.ReorderLevel AND P.UnitsOnOrder=0

--3.在【訂單明細】資料表找出訂單中包含超過5種產品的資料記錄。
SELECT  OD.OrderID　, count(*)as amount
FROM OrderDetails OD
GROUP BY OD.OrderID
having count(*)>=5

--4.在【訂單明細】資料表中顯示出訂單號碼10263所有產品的價格小計。
SELECT　*, OD.Quantity*OD.UnitPrice*(1-OD.Discount)as SubTotal
FROM OrderDetails OD
WHERE OD.OrderID='10263'

--5.利用【產品】資料表資料，統計出每一個供應商各提供了幾樣產品。
SELECT  P.SupplierID, count(*)as Total
FROM Products P
GROUP BY P.SupplierID

--6.利用【訂單明細】資料表資料，統計出各項商品的平均單價與平均銷售數量，並列出平均銷售數量大於10的資料，且將資料依產品編號由小到大排序。
SELECT　OD.ProductID, AVG(OD.UnitPrice)as AVG_Price, AVG(OD.Quantity)as AVG_Quamtity
FROM OrderDetails OD
GROUP BY OD.ProductID
having AVG(OD.Quantity)>10
ORDER BY OD.ProductID



--============================================================【任務二】============================================================================================
--1.請試寫一合併查詢，查詢出訂購日期落在1996年7月並指定送貨公司為「United Package」的有訂單之訂貨明細資料，
--並列出訂單號碼、產品類別名稱、產品名稱、產品訂購單價、產品訂購數量、產品價錢小計、客戶編號、客戶名稱、
--       收貨人名字、訂購日期、處理訂單員工的姓名、貨運公司、供應商名稱等資料項目，其中產品價錢小計請以四捨五入計算至整數位。
SELECT　OD.OrderID,C.CategoryName, P.ProductName, OD.UnitPrice, OD.Quantity, ROUND((OD.Quantity*OD.UnitPrice*(1-OD.Discount)),0)as Subtotal, O.CustomerID, Cum.CompanyName,
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


--2.請利用exists運算子配合子查詢式，找出哪些客戶從未下過訂單，並列出客戶的所有欄位。(不可用到in運算，亦不可用合併查詢式)
SELECT *
FROM Customers C
WHERE not exists(SELECT * FROM Orders O WHERE C.CustomerID=O.CustomerID)


--3. 請利用in運算子配合子查詢式，查詢哪些員工有處理過訂單，並列出員工的員工編號、姓名、職稱、內部分機號碼、附註欄位。(不可用到exists運算，亦不可用合併查詢式) 
SELECT E.EmployeeID, (E.FirstName+' ' +E.LastName)as EmployeeName, E.Title, E.Extension, E.Notes
FROM Employees E
WHERE E.EmployeeID in (SELECT O.EmployeeID FROM Orders O )

--4. 請合併查詢與子查詢兩種寫法，列出1998年度所有被訂購過的產品資料，並列出產品的所有欄位，且依產品編號由小到大排序。
--(寫合併查詢時不得用任何子查詢式，寫子查詢時不得用任何合併查詢式)
--合併查詢：
SELECT DISTINCT P.*
FROM Products P
INNER JOIN OrderDetails OD on P.ProductID=OD.ProductID
INNER JOIN Orders O on OD.OrderID=O.OrderID
WHERE year(O.OrderDate)='1998'
ORDER BY P.ProductID

--子查詢：
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




--============================================================【任務三】============================================================================================
--1.請寫出建立一個名為【MySchool】資料庫的SQL DDL Script。
create database MySchool
go

--2.請參考ER圖及下列資料表規格，寫出相對應之SQL DDL Script，使其可於【MySchool】資料庫中建立這些資料表。
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


--============================================================【任務四】============================================================================================
--請利用任務三所建立的【MySchool】資料庫，建立一個名為「InsertDeptmentData」的預存程序，
--新增科系資料時必須呼叫執行此預存程序進行資料新增。
use MySchool
go
create proc InsertDeptmentData 
 @id nchar(1),
 @name nvarchar(30)
as
begin
	declare @i int
	declare @j int
	--1.預存程序內需檢查科系代碼(DeptID)及科系名稱(DeptName)是否已在資料庫中。
	Select @i = count(*) from Department D where D.DeptID=@id 
	Select @j = count(*) from Department D where D.DeptName=@name

	if @i <> 0 or  @j <> 0
		--2.若欲新增的資料值檢查到已被使用，則輸出對應的錯誤訊息且不進行Insert動作。
		print 'DeptID or DeptName is exist'
	else
		begin
			--3.若欲新增的資料值皆未被使用，則進行Insert動作將資料寫入資料庫。
			INSERT INTO Department values(@id, @name)
		end
end
go
-------------------------------
--測試
--exec InsertDeptmentData 'A','Business Administration'



--============================================================【任務五】============================================================================================
--請利用任務三所建立的【MySchool】資料庫，建立一個名為「getCourseID」的自訂函數，
--其功能為新增課程資料時可呼叫此函數自動取得一個新的課程編號。CourseID的編碼規則為英文字母C加上開課科系代碼再加上3碼流水號。
--(例如科系代碼為G開的第123門課程，CourseID為CG123，科系代碼為B開的第1門課程，CourseID為CB001)。
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
--測試
Insert into Course values(dbo.getCourseID('A'), '會計學(一)', 3, 3, 'A')
Insert into Course values(dbo.getCourseID('A'), '會計學(二)', 3, 3, 'A')

select * from Course

