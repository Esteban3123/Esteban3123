/*
  PBI 24152 — Plantilla SQL para Campos del Editor de Expresiones
  Vie HCM / Nómina / Reforma pensional Ley 2381 de 2024

  IMPORTANTE:
  - Es una PLANTILLA. Los nombres de schema/tabla/columnas son orientativos.
  - Antes de ejecutar: localizar en QA la tabla real del catálogo de Campos
    (buscar registros existentes como 'IBC Pensión', 'IBC Salud', 'IBC Periodo').
  - Validar en ambiente QA. No ejecutar a ciegas en producción.
*/

------------------------------------------------------------------------------
-- 1) Descubrir cómo están modelados los Campos IBC actuales
------------------------------------------------------------------------------
-- Ajustar el nombre de tabla tras el descubrimiento:
/*
SELECT TOP (50) *
FROM Nomina.ExpressionEditorField -- <-- reemplazar
WHERE DisplayName LIKE N'%IBC%'
   OR Code LIKE N'%IBC%'
ORDER BY DisplayName;
*/

------------------------------------------------------------------------------
-- 2) Insertar variables CPM / ACCAI (idempotente de ejemplo)
------------------------------------------------------------------------------
/*
IF NOT EXISTS (
    SELECT 1 FROM Nomina.ExpressionEditorField WHERE Code = N'IbcPensionPrimaMedia'
)
BEGIN
    INSERT INTO Nomina.ExpressionEditorField
        (Code, DisplayName, Category, SourceProperty, DataType, IsActive, CreatedAt)
    VALUES
        (N'IbcPensionPrimaMedia', N'IBC Pensión Prima Media', N'Campos',
         N'IbcPensionCpm', N'Decimal', 1, SYSUTCDATETIME());
END;

IF NOT EXISTS (
    SELECT 1 FROM Nomina.ExpressionEditorField WHERE Code = N'IbcPensionAccai'
)
BEGIN
    INSERT INTO Nomina.ExpressionEditorField
        (Code, DisplayName, Category, SourceProperty, DataType, IsActive, CreatedAt)
    VALUES
        (N'IbcPensionAccai', N'IBC Pensión ACCAI', N'Campos',
         N'IbcPensionAccai', N'Decimal', 1, SYSUTCDATETIME());
END;
*/

------------------------------------------------------------------------------
-- 3) Verificación
------------------------------------------------------------------------------
/*
SELECT Code, DisplayName, Category, SourceProperty, DataType, IsActive
FROM Nomina.ExpressionEditorField
WHERE Code IN (N'IbcPensionPrimaMedia', N'IbcPensionAccai');
*/

------------------------------------------------------------------------------
-- 4) Rollback de ejemplo
------------------------------------------------------------------------------
/*
DELETE FROM Nomina.ExpressionEditorField
WHERE Code IN (N'IbcPensionPrimaMedia', N'IbcPensionAccai');
*/
