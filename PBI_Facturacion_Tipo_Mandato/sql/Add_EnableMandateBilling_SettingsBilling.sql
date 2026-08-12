-- =============================================================================
-- PBI: Habilitar Facturación Tipo Mandato
-- Form: FrmSettingBilling · Entidad: SettingsBilling · Prop: EnableMandateBilling
-- =============================================================================
-- Ajustar schema/tabla real si difiere (buscar columna ApplyBasicBilling).
-- Ejecutar primero en QA.
-- =============================================================================

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @Schema SYSNAME = N'dbo';           -- p.ej. Billing
        @Table  SYSNAME = N'SettingsBilling'; -- confirmar con: WHERE name like '%Setting%Billing%'
        @Full   NVARCHAR(512),
        @Sql    NVARCHAR(MAX);

SET @Full = QUOTENAME(@Schema) + N'.' + QUOTENAME(@Table);

BEGIN TRAN;

IF COL_LENGTH(@Schema + '.' + @Table, 'EnableMandateBilling') IS NULL
BEGIN
    SET @Sql = N'ALTER TABLE ' + @Full + N'
        ADD EnableMandateBilling BIT NOT NULL
            CONSTRAINT DF_' + @Table + N'_EnableMandateBilling DEFAULT (0);';
    EXEC sp_executesql @Sql;
    PRINT 'Columna EnableMandateBilling agregada.';
END
ELSE
    PRINT 'Columna EnableMandateBilling ya existe — skip.';

-- Backfill (por si quedó nullable en un intento previo)
SET @Sql = N'UPDATE ' + @Full + N'
             SET EnableMandateBilling = 0
             WHERE EnableMandateBilling IS NULL;';
EXEC sp_executesql @Sql;

-- Verificación
SET @Sql = N'SELECT
                COUNT(*) AS TotalRegistros,
                SUM(CASE WHEN EnableMandateBilling = 0 THEN 1 ELSE 0 END) AS EnNo,
                SUM(CASE WHEN EnableMandateBilling = 1 THEN 1 ELSE 0 END) AS EnSi
             FROM ' + @Full + N';';
EXEC sp_executesql @Sql;

-- Checklist: incluir EnableMandateBilling en el mismo SELECT/INSERT/UPDATE
-- donde ya viaja ApplyBasicBilling (EF / SP / repositorio).

-- COMMIT;
ROLLBACK;
PRINT 'Dry-run (ROLLBACK). Cambiar a COMMIT cuando schema/tabla estén confirmados.';
