# PBI 24152 — IBC Pensión Prima Media / ACCAI

Solución documental y de apoyo para el **Editor de Expresiones** de Vie HCM (Ley 2381 de 2024).

## Entregables

| Archivo | Descripción |
|---------|-------------|
| [ACCEPTANCE_CRITERIA_PBI_24152.md](./ACCEPTANCE_CRITERIA_PBI_24152.md) | AC oficiales (Dado/Cuando/Entonces) |
| [SOLUCION_PBI_24152_IBC_PENSION_PRIMA_MEDIA.md](./SOLUCION_PBI_24152_IBC_PENSION_PRIMA_MEDIA.md) | Análisis, mapeo, checklist de implementación, CA y DoD |
| [mapas/PBI_24152_MAPEO_VARIABLES.md](./mapas/PBI_24152_MAPEO_VARIABLES.md) | Tabla corta label ↔ variable |
| [snippets/RegisterIbcPensionExpressionFields.vb](./snippets/RegisterIbcPensionExpressionFields.vb) | Snippet VB.NET (adaptar al API real) |
| [sql/PBI_24152_ExpressionFields_TEMPLATE.sql](./sql/PBI_24152_ExpressionFields_TEMPLATE.sql) | Plantilla SQL si el catálogo es por BD |
| [BITACORA7_PBI_24152.md](./BITACORA7_PBI_24152.md) | Filas Bitácora 7 listas para pegar |

## Resultado esperado en el producto

En **Editor de Expresiones → Campos**:

1. **IBC Pensión Prima Media** ← valor del label **IBC Pensión - CPM**
2. **IBC Pensión ACCAI** ← valor del label **IBC Pensión - ACCAI**

Origen UI: **Liquidación de Nómina → IBC - Control Nómina** (prerrequisito PBI 27042).

## Nota

Este repositorio no contiene el código fuente de Vie HCM. Los snippets son plantillas para aplicar en el repo corporativo del módulo Nómina.
