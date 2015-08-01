ALTER PROCEDURE dbo.Food_Nutrition_Get
(
  @Name VARCHAR(100) = '',
  @MinCalories DECIMAL(19,8) = NULL,
  @MaxCalories DECIMAL(19,8) = NULL,
  @MinWater DECIMAL(19,8) = NULL,
  @MaxWater DECIMAL(19,8) = NULL,
  @MinProtein DECIMAL(19,8) = NULL,
  @MaxProtein DECIMAL(19,8) = NULL,
  @MinLipid DECIMAL(19,8) = NULL,
  @MaxLipid DECIMAL(19,8) = NULL,
  @MinCarbohydrate DECIMAL(19,8) = NULL,
  @MaxCarbohydrate DECIMAL(19,8) = NULL,
  @MinFiber DECIMAL(19,8) = NULL,
  @MaxFiber DECIMAL(19,8) = NULL,
  @MinSugar DECIMAL(19,8) = NULL,
  @MaxSugar DECIMAL(19,8) = NULL,
  @MinCalcium DECIMAL(19,8) = NULL,
  @MaxCalcium DECIMAL(19,8) = NULL,
  @MinIron DECIMAL(19,8) = NULL,
  @MaxIron DECIMAL(19,8) = NULL
)
AS 
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

SELECT
  F.Name,
  ISNULL(F.Calories, 0) AS Calories,
  ISNULL(F.Water, 0) AS Water,  
  ISNULL(F.Protein, 0) AS Protein,  
  ISNULL(F.Lipid, 0) AS Lipid,  
  ISNULL(F.Carbohydrate, 0) AS Carbohydrate,  
  ISNULL(F.Fiber, 0) AS Fiber,  
  ISNULL(F.Sugar, 0) AS Sugar,  
  ISNULL(F.Calcium, 0) AS Calcium,  
  ISNULL(F.Iron, 0) AS Iron
FROM dbo.Food AS F
WHERE F.Name LIKE '%' + @Name + '%'
  AND (@MinCalories IS NULL OR ISNULL(F.Calories, 0) >= @MinCalories)
  AND (@MaxCalories IS NULL OR ISNULL(F.Calories, 0) <= @MinCalories)
  AND (@MinWater IS NULL OR ISNULL(F.Water, 0) >= @MinWater)
  AND (@MaxWater IS NULL OR ISNULL(F.Water, 0) <= @MaxWater)
  AND (@MinProtein IS NULL OR ISNULL(F.Protein, 0) >= @MinProtein)
  AND (@MaxProtein IS NULL OR ISNULL(F.Protein, 0) <= @MaxProtein)
  AND (@MinLipid IS NULL OR ISNULL(F.Lipid, 0) >= @MinLipid)
  AND (@MaxLipid IS NULL OR ISNULL(F.Lipid, 0) <= @MaxLipid)
  AND (@MinCarbohydrate IS NULL OR ISNULL(F.Carbohydrate, 0) >= @MinCarbohydrate)
  AND (@MaxCarbohydrate IS NULL OR ISNULL(F.Carbohydrate, 0) <= @MaxCarbohydrate)
  AND (@MinFiber IS NULL OR ISNULL(F.Fiber, 0) >= @MinFiber)
  AND (@MaxFiber IS NULL OR ISNULL(F.Fiber, 0) <= @MaxFiber)
  AND (@MinSugar IS NULL OR ISNULL(F.Sugar, 0) >= @MinSugar)
  AND (@MaxSugar IS NULL OR ISNULL(F.Sugar, 0) <= @MaxSugar)
  AND (@MinCalcium IS NULL OR ISNULL(F.Calcium, 0) >= @MinCalcium)
  AND (@MaxCalcium IS NULL OR ISNULL(F.Calcium, 0) <= @MaxCalcium)
  AND (@MinIron IS NULL OR ISNULL(F.Iron, 0) >= @MinIron)
  AND (@MaxIron IS NULL OR ISNULL(F.Iron, 0) <= @MaxIron)

RETURN;
