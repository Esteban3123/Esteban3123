# Bitácora — Habilitar Facturación Tipo Mandato

Filas sugeridas para seguimiento de etapa productiva / Azure DevOps.

| # | Actividad | Descripción | Evidencia |
|---|-----------|-------------|-----------|
| 1 | Análisis PBI | Revisar narrativa, alcance, AC y mockup (Información Adicional) | Documento PBI + captura |
| 2 | Localización técnica | Identificar form Parámetros de Facturación, campo Aplica Facturación Básica, tabla/SP | Nombres reales en repo corporativo |
| 3 | Script BD | Agregar columna + default NO + backfill | Script QA + SELECT verificación |
| 4 | Backend | Extender DTO/entidad/Get/Save + gate `IsMandateBillingEnabled` | PR / changeset |
| 5 | Frontend | Control SI/NO, visibilidad condicionada, default NO, persistencia | Capturas UI |
| 6 | Pruebas AC | Ejecutar matriz AC1–AC6 | Checklist firmado |
| 7 | Cierre | Adjuntar evidencias al PBI y pasar a revisión | Link ADO |

## Texto corto para actividad diaria

> Se elaboró la solución técnica del parámetro **Habilitar Facturación Tipo Mandato** en Parámetros de Facturación (segmento Información Adicional): visible solo si Aplica Facturación Básica = SI, opciones SI/NO, default NO, persistencia y gate para deshabilitar funciones de mandato cuando el valor es NO. Entregables: guía APLICAR, SQL plantilla y snippets VB.NET.
