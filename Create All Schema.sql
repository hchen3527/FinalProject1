-- Altering a file
CREATE TABLE [User]
(
  User_Key INT IDENTITY(1,1),
  [User_Name] VARCHAR(50) NOT NULL,
  [Password] VARCHAR(50) NOT NULL
);

ALTER TABLE [User]
ADD CONSTRAINT PK_User PRIMARY KEY (User_Key);

ALTER TABLE [User] WITH CHECK 
ADD CONSTRAINT [CK_User_User_Name_Required] CHECK  (([User_Name] != N''));

ALTER TABLE [User] WITH CHECK 
ADD CONSTRAINT [CK_User_Password_Required] CHECK  (([Password] != N''));

CREATE TABLE Password_History
(
  Password_History_Key INT IDENTITY(1,1),
  User_Key INT NOT NULL,
  [Password] VARCHAR(50)
);

ALTER TABLE Password_History
ADD CONSTRAINT PK_Password_History PRIMARY KEY (Password_History_Key);

ALTER TABLE [Password_History] WITH CHECK 
ADD CONSTRAINT [CK_Password_History_Password_Required] CHECK  (([Password] != N''));

ALTER TABLE Password_History
ADD CONSTRAINT FK_Password_History_User_Key
FOREIGN KEY (User_Key)
REFERENCES [User](User_Key);

CREATE TABLE User_Profile
(
  [User_Key] INT NOT NULL,
  Last_Name VARCHAR(30) NOT NULL,
  First_Name VARCHAR(30) NOT NULL,
  Join_Date DATETIME DEFAULT DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0),
  Middle_Name VARCHAR(30),
  [Address] VARCHAR(100),
  City VARCHAR(50),
  [State] VARCHAR(50),
  Country VARCHAR(50)
);

ALTER TABLE User_Profile
ADD CONSTRAINT PK_User_Profile PRIMARY KEY (User_Key);

ALTER TABLE User_Profile
ADD CONSTRAINT FK_User_Profile_User_Key
FOREIGN KEY (User_Key)
REFERENCES [User](User_Key);

ALTER TABLE User_Profile WITH CHECK 
ADD CONSTRAINT [CK_User_Profile_Last_Name_Required] CHECK  (([Last_Name] != N''));

ALTER TABLE User_Profile WITH CHECK 
ADD CONSTRAINT [CK_User_Profile_First_Name_Required] CHECK  (([First_Name] != N''));

CREATE TABLE Unit_Group
(
  Unit_Group_Key INT IDENTITY(1,1),
  Unit_Group_Name VARCHAR(50)
);

ALTER TABLE Unit_Group
ADD CONSTRAINT PK_Unit_Group PRIMARY KEY (Unit_Group_Key);

CREATE TABLE Unit
(
  Unit_Key INT IDENTITY(1,1),
  Unit_Name VARCHAR(50),
  Unit_Group_Key INT NOT NULL
);

ALTER TABLE [Unit]
ADD CONSTRAINT PK_Unit PRIMARY KEY (Unit_Key);

ALTER TABLE Unit
ADD CONSTRAINT FK_Unit_Unit_Group_Key
FOREIGN KEY (Unit_Group_Key)
REFERENCES Unit_Group(Unit_Group_Key);

CREATE TABLE Unit_Conversion
(
  Unit_Key1 INT NOT NULL,
  Unit_Key2 INT NOT NULL,
  Conversion DECIMAL(19,8)
);

ALTER TABLE Unit_Conversion
ADD CONSTRAINT PK_Unit_Conversion PRIMARY KEY (Unit_Key1, Unit_Key2);

ALTER TABLE Unit_Conversion
ADD CONSTRAINT FK_Unit_Conversion_Unit_Key1
FOREIGN KEY (Unit_Key1)
REFERENCES Unit(Unit_Key);

ALTER TABLE Unit_Conversion
ADD CONSTRAINT FK_Unit_Conversion_Unit_Key2
FOREIGN KEY (Unit_Key2)
REFERENCES Unit(Unit_Key);

CREATE TABLE Food
(
  Food_Key INT IDENTITY(1,1),
  Name VARCHAR(100),
  Calories DECIMAL(19,8) DEFAULT 0,
  Water DECIMAL(19,8) DEFAULT 0,
  Water_Unit_Key INT NOT NULL,
  Protein DECIMAL(19,8) DEFAULT 0,
  Protein_Unit_Key INT NOT NULL,
  Lipid DECIMAL(19,8) DEFAULT 0,
  Lipid_Unit_Key INT NOT NULL,
  Carbohydrate DECIMAL(19,8) DEFAULT 0,
  Carbohydrate_Unit_Key INT NOT NULL,
  Fiber DECIMAL(19,8) DEFAULT 0,
  Fiber_Unit_Key INT NOT NULL,
  Sugar DECIMAL(19,8) DEFAULT 0,
  Sugar_Unit_Key INT NOT NULL,
  Calcium DECIMAL(19,8) DEFAULT 0,
  Calcium_Unit_Key INT NOT NULL,
  Iron DECIMAL(19,8) DEFAULT 0,
  Iron_Unit_Key INT NOT NULL
);

ALTER TABLE Food
ADD CONSTRAINT PK_Food PRIMARY KEY (Food_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Water_Unit_Key
FOREIGN KEY (Water_Unit_Key)
REFERENCES Unit(Unit_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Protein_Unit_Key
FOREIGN KEY (Protein_Unit_Key)
REFERENCES Unit(Unit_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Lipid_Unit_Key
FOREIGN KEY (Lipid_Unit_Key)
REFERENCES Unit(Unit_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Carbohydrate_Unit_Key
FOREIGN KEY (Carbohydrate_Unit_Key)
REFERENCES Unit(Unit_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Fiber_Unit_Key
FOREIGN KEY (Fiber_Unit_Key)
REFERENCES Unit(Unit_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Sugar_Unit_Key
FOREIGN KEY (Sugar_Unit_Key)
REFERENCES Unit(Unit_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Iron_Unit_Key
FOREIGN KEY (Iron_Unit_Key)
REFERENCES Unit(Unit_Key);

ALTER TABLE Food
ADD CONSTRAINT FK_Unit_Conversion_Calcium_Unit_Key
FOREIGN KEY (Calcium_Unit_Key)
REFERENCES Unit(Unit_Key);

CREATE TABLE Nutrition_History
(
  Nutrition_History_Key INT IDENTITY(1,1),
  [User_Key] INT NOT NULL,
  Add_Date DATETIME DEFAULT DATEADD(dd, DATEDIFF(dd, 0, GETDATE()), 0),
  Food_Key INT NOT NULL,
  Quantity DECIMAL(19,8) DEFAULT 0,
  Unit_Key INT NOT NULL
);

ALTER TABLE Nutrition_History
ADD CONSTRAINT PK_Nutrition_History PRIMARY KEY (Nutrition_History_Key, [User_Key]);

ALTER TABLE Nutrition_History
ADD CONSTRAINT FK_Nutrition_History_User_Key
FOREIGN KEY (User_Key)
REFERENCES [User](User_Key);

ALTER TABLE Nutrition_History
ADD CONSTRAINT FK_Nutrition_History_Food_Key
FOREIGN KEY (Food_Key)
REFERENCES [Food](Food_Key);

ALTER TABLE Nutrition_History
ADD CONSTRAINT FK_Nutrition_History_Unit_Key
FOREIGN KEY (Unit_Key)
REFERENCES Unit(Unit_Key);