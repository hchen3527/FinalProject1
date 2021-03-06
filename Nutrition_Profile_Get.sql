USE [Nutrition]
GO
/****** Object:  StoredProcedure [dbo].[Nutrition_Profile_Get]    Script Date: 9/7/2015 4:09:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Nutrition_Profile_Get]
(
  @User_Key INT,
  @Add_Date_From DATETIME = NULL,
  @Add_Date_To DATETIME = NULL
)
AS 
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

SET @Add_Date_From = DATEADD(dd, 0, DATEDIFF(dd, 0, @Add_Date_From));
SET @Add_Date_To = DATEADD(dd, 1, DATEADD(dd, 0, DATEDIFF(dd, 0, @Add_Date_To)));

SELECT
  N.Nutrition_History_Key,
  F.Name,
  N.Quantity,
  U.Unit_Name,
  N.Add_Date,
  ISNULL(F.Calories, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Calories,
  ISNULL(F.Water, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Water,
  ISNULL(F.Protein, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Protein,
  ISNULL(F.Lipid, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Lipid,
  ISNULL(F.Carbohydrate, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Carbohydrate,
  ISNULL(F.Fiber, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Fiber,
  ISNULL(F.Sugar, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Sugar,
  ISNULL(F.Calcium, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Calcium,
  ISNULL(F.Iron, 0) * N.Quantity * ISNULL(CONV.Conversion, 1) AS Iron
FROM dbo.Nutrition_History AS N
JOIN dbo.Food AS F
  ON F.Food_Key = N.Food_Key
JOIN dbo.Unit AS U
  ON U.Unit_Key = N.Unit_Key
OUTER APPLY
(
  SELECT
    UC.Conversion
  FROM dbo.Unit_Conversion AS UC
  WHERE UC.Unit_Key1 = U.Unit_Key
    AND UC.Unit_Key2 = F.Food_Unit_Key
) AS CONV
WHERE N.User_Key = @User_Key
  AND (@Add_Date_From IS NULL OR N.Add_Date >= @Add_Date_From)
  AND (@Add_Date_To IS NULL OR N.Add_Date < @Add_Date_To);

RETURN;

