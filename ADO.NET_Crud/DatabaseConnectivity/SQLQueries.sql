USE ADO_ASP_Crud

GO
CREATE PROCEDURE spAddStudentInfo  
( 
@stdName varchar(255),  @stdEmail varchar(255),  @stdAge int,  @stdPhone varchar(255),  @stdImage varchar(255)  
)  
as  
Begin   
	Insert into Student_Info(Std_Name, Std_Email, age , Std_Phone, Std_Image) values (@stdName, @stdEmail, @stdAge, @stdPhone, @stdImage);  
End;

GO

CREATE PROCEDURE spGetStudentData  as  Begin   Select * from Student_Info order by Std_Id desc;  End;



GO
CREATE PROCEDURE spUpdateStudentData
(
@id int,
@name varchar(255),
@email varchar(255),
@age int,
@phone varchar(255),
@image varchar(255)
)
as
BEGIN
	Update Student_Info Set Std_Name = @name, Std_Email = @email, age = @age,  Std_Image = @image, Std_Phone = @phone where Std_Id = @id;
END;
GO

CREATE PROCEDURE spDeleteStudentData
(
@id int
)
as
BEGIN
	Delete from Student_Info where Std_Id = @id;
END;