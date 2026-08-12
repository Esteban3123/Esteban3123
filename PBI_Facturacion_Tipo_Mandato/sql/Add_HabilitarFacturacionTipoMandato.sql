-- =============================================================================
-- PBI: Habilitar Facturación Tipo Mandato
-- Formulario: Parámetros de Facturación (VIE RCM · Facturación Salud)
-- =============================================================================
-- PLANTILLA — ajustar:
--   1) Schema y nombre real de la tabla de parámetros por UO
--   2) Tipo de dato usado por "Aplica Facturación Básica" (BIT vs CHAR(1))
--   3) Nombres de SP de lectura/escritura si aplican
-- Ejecutar primero en QA. No aplicar a ciegas en producción.
-- =============================================================================

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRAN;

/* --------------------------------------------------------------------------
   1) Columna nueva (idempotente)
   Opción A — BIT (recomendado si AplicaFacturacionBasica es BIT)
   -------------------------------------------------------------------------- */
IF COL_LENGTH('dbo.BillingParameters', 'HabilitarFacturacionTipoMandato') IS NULL
BEGIN
    ALTER TABLE dbo.BillingParameters
        ADD HabilitarFacturacionTipoMandato BIT NOT NULL
            CONSTRAINT DF_BillingParameters_HabilitarFacturacionTipoMandato DEFAULT (0);
    PRINT 'Columna HabilitarFacturacionTipoMandato (BIT) agregada.';
END
ELSE
BEGIN
    PRINT 'Columna HabilitarFacturacionTipoMandato ya existe — skip ALTER.';
END

/* --------------------------------------------------------------------------
   Opción B — CHAR(1) 'S'/'N' (descomentar y comentar Opción A si el patrón
   del formulario usa SI/NO como carácter)
   --------------------------------------------------------------------------
IF COL_LENGTH('dbo.BillingParameters', 'HabilitarFacturacionTipoMandato') IS NULL
BEGIN
    ALTER TABLE dbo.BillingParameters
        ADD HabilitarFacturacionTipoMandato CHAR(1) NOT NULL
            CONSTRAINT DF_BillingParameters_HabilitarFacturacionTipoMandato DEFAULT ('N');

    ALTER TABLE dbo.BillingParameters
        ADD CONSTRAINT CK_BillingParameters_HabilitarFacturacionTipoMandato
            CHECK (HabilitarFacturacionTipoMandato IN ('S', 'N'));
END
-------------------------------------------------------------------------- */

/* --------------------------------------------------------------------------
   2) Backfill — todas las UO / clientes existentes en NO
   (por si la columna se creó nullable en un intento previo)
   -------------------------------------------------------------------------- */
UPDATE dbo.BillingParameters
SET    HabilitarFacturacionTipoMandato = 0   -- o 'N' en Opción B
WHERE  HabilitarFacturacionTipoMandato IS NULL;

/* --------------------------------------------------------------------------
   3) Verificación
   -------------------------------------------------------------------------- */
SELECT
    COUNT(*) AS TotalRegistros,
    SUM(CASE WHEN HabilitarFacturacionTipoMandato = 0 THEN 1 ELSE 0 END) AS EnNo,
    SUM(CASE WHEN HabilitarFacturacionTipoMandato = 1 THEN 1 ELSE 0 END) AS EnSi
FROM dbo.BillingParameters;

/* --------------------------------------------------------------------------
   4) SP / consultas — checklist manual
   Buscar y agregar la columna en:
     - SP Get / List de parámetros de facturación
     - SP Insert / Update de parámetros de facturación
     - Vistas o TVF usadas por el formulario
   Ejemplo ilustrativo (NO ejecutar sin adaptar):

   -- ALTER PROCEDURE dbo.usp_BillingParameters_Get ...
   --   SELECT ..., HabilitarFacturacionTipoMandato FROM dbo.BillingParameters ...

   -- ALTER PROCEDURE dbo.usp_BillingParameters_Save ...
   --   UPDATE dbo.BillingParameters
   --   SET ..., HabilitarFacturacionTipoMandato = @HabilitarFacturacionTipoMandato
   -------------------------------------------------------------------------- */

-- COMMIT;   -- descomentar tras validar en QA
ROLLBACK;    -- por defecto hace rollback hasta confirmar nombres reales
PRINT 'Script en modo dry-run (ROLLBACK). Cambiar a COMMIT cuando la tabla/columna estén validadas.';
