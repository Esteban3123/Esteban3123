# Actividades bitácora — basadas en tus ejercicios reales

Fuente: sesión Cursor **“Bug de tarea asignada”** (03/08/2026 – 04/08/2026)  
Empresa / producto: **Vie RCM / Vie ERP** (Inventory + Account Management)  
Tecnologías: **VB.NET, WinForms, DevExpress, SQL Server, Azure DevOps**

> Si tu Excel de Bitácora N°1 dice 23/10/2025–07/11/2025, estas filas van en la **bitácora del periodo donde caigan el 03 y 04 de agosto 2026**.  
> Tiempo estimado real de trabajo con el agente: **~6–9 horas** en 2 días.

---

## FILA 1 — Bug alineación formulario Subgrupos

| Campo | Texto para pegar |
|--------|------------------|
| **Descripción de la actividad** | Análisis y corrección de bug de interfaz en el formulario Diseño - Subgrupos (`FrmSubGroup`): desalineación de los checkboxes Maneja Lote y Maneja Fecha Vencimiento respecto a los campos Código y Nombre. Se revisó el Designer (DevExpress LayoutControl), se identificó que los `LayoutControlItem` de los checkboxes tenían anchos distintos (180/260 vs 390/403) y se definió el ajuste de Size, MinSize, MaxSize, TextSize y TextToControlDistance para alinear los controles sin modificar la lógica de negocio del code-behind. |
| **Competencias aplicadas** | 220501096 – Desarrollar la solución de software de acuerdo con el diseño y metodologías de desarrollo. 220501098 – Controlar la calidad del servicio de software de acuerdo con los estándares técnicos. |
| **Fecha de inicio** | 03/08/26 |
| **Fecha de fin** | 03/08/26 |
| **Evidencia de cumplimiento** | Proceso: diagnóstico del Designer `FrmSubGroup.Designer.vb`. Producto: propuesta de ajuste de layout (Size 403×36, TextSize 100×21). Documento: capturas del formulario antes del ajuste. |
| **Observaciones** | Tiempo aproximado de solución: 2 a 3 horas. El code-behind `FrmSubGroup.vb` no requería cambios; la causa era únicamente el layout en el Designer. |

---

## FILA 2 — Investigación bug 39486 columna Paciente en PDF

| Campo | Texto para pegar |
|--------|------------------|
| **Descripción de la actividad** | Investigación del bug Azure DevOps 39486 (severidad High) del módulo Gestión de Cuentas / Informe Gestión de Cuentas Detallado (Vie RCM): en el PDF la columna Paciente mostraba solo el nombre y omitía la identificación. Se realizó trazabilidad capa por capa: formulario `FrmReportAccountManagement`, reporte DevExpress `rptAccountManagementReportDetailed`, entidad XPO `ViewReportAccountManagementXpo` y vista SQL `AccountManagement.ViewReportAccountManagement` en la base INDIGO636 (ambiente QA 636 – Fundación Hospital de la Misericordia). |
| **Competencias aplicadas** | 220501093 – Evaluar requisitos de la solución de software de acuerdo con metodologías de análisis y estándares. 220501098 – Controlar la calidad del servicio de software de acuerdo con los estándares técnicos. 220501046 – Utilizar herramientas informáticas de acuerdo con las necesidades de manejo de información. |
| **Fecha de inicio** | 03/08/26 |
| **Fecha de fin** | 04/08/26 |
| **Evidencia de cumplimiento** | Documento: ticket Azure DevOps 39486 / PBI 26962. Proceso: análisis Presentation → XtraReport → XPO → vista SQL. Producto: identificación de causa raíz (`IPNOMCOMP AS PatientCodeName` sin concatenar `IPCODPACI`). |
| **Observaciones** | Tiempo aproximado de investigación: 4 a 5 horas entre el 03 y 04/08/2026. Se confirmó que la causa no estaba en el formulario ni en el Designer del reporte, sino en la vista de base de datos. |

---

## FILA 3 — Consulta SQL y script ALTER VIEW (solución bug 39486)

| Campo | Texto para pegar |
|--------|------------------|
| **Descripción de la actividad** | Elaboración y documentación de la solución SQL para el bug 39486: consulta/definición de la vista `AccountManagement.ViewReportAccountManagement` mediante `OBJECT_DEFINITION` en SQL Server (BD INDIGO636) y construcción del script `ALTER VIEW` para que el campo `PatientCodeName` concatene identificación y nombre del paciente (`IPCODPACI - IPNOMCOMP`) con manejo de nulos, dejando el PDF del informe detallado en el formato esperado (ej. `1146147731 - MATIAS MORALES AYALA`). |
| **Competencias aplicadas** | 220501096 – Desarrollar la solución de software de acuerdo con el diseño y metodologías de desarrollo. 220501097 – Implementar la solución de software de acuerdo con los requisitos de operación y modelos de referencia. 220501046 – Utilizar herramientas informáticas de acuerdo con las necesidades de manejo de información. |
| **Fecha de inicio** | 04/08/26 |
| **Fecha de fin** | 04/08/26 |
| **Evidencia de cumplimiento** | Documento: script SQL `ALTER VIEW` entregado para QA. Proceso: verificación con `OBJECT_DEFINITION` / definición completa de la vista. Producto: campo Paciente con formato Identificación - Nombre Completo. |
| **Observaciones** | Tiempo aproximado de elaboración del script y validación de la definición: 2 a 3 horas. Pendiente ejecución/validación final en ambiente QA por el aprendiz/supervisor. Sin inasistencias. |

---

## Resumen de tiempos (para observaciones o tu control)

| Actividad | Fecha | Tiempo aprox. |
|-----------|--------|----------------|
| Bug Subgrupos (alineación UI) | 03/08/2026 | 2–3 h |
| Investigación bug 39486 (PDF Paciente) | 03–04/08/2026 | 4–5 h |
| Script SQL ALTER VIEW | 04/08/2026 | 2–3 h |
| **Total** | **03–04/08/2026** | **~8–11 h** |

---

## Notas para el Excel

1. Pega las 3 filas tal cual (o acórtalas un poco si tu celda es pequeña).  
2. En **Evidencia** no adjuntos datos sensibles del hospital ni scripts de producción; con “documento / proceso / producto” basta.  
3. Si el instructor pide el **código de competencia** solo con el nombre corto, puedes dejar:  
   `220501096 Desarrollar la solución de software` / `220501098 Controlar la calidad del servicio de software` / etc.
