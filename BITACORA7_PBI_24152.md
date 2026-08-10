# Bitácora N° 7 — filas listas para pegar (PBI 24152)

**Periodo:** Desde **07/08/2026** hasta **21/08/2026**  
**Nota de fechas:** 07/08 festivo (Boyacá) · 17/08 festivo (Asunción) → actividades hábiles desde **10/08**.  
**Tema principal:** PBI Azure DevOps **24152** — Campo Componente Prima Media / variables IBC en Editor de Expresiones (Ley 2381 de 2024).  
**Producto:** Vie HCM · Nómina · Colombia  

> Estilo: frases cortas y técnicas · competencias **sin código** · fechas lun–vie.

---

### Fila 1
| Campo | Texto |
|--------|--------|
| Descripción | Análisis del Product Backlog Item 24152 (Reforma Pensional Ley 2381 de 2024): creación del campo Componente Prima Media en el Editor de Expresiones. Se revisaron historia de usuario, prerrequisito 27042, alcance y capturas del formulario Liquidación de Nómina / ventanilla IBC - Control Nómina. |
| Competencias | Evaluar requisitos de la solución de software de acuerdo con metodologías de análisis y estándares. Diseñar la solución de software de acuerdo con procedimientos y requisitos técnicos. |
| Inicio | 10/08/26 |
| Fin | 10/08/26 |
| Evidencia | Documento: PBI 24152 / mapeo de variables CPM y ACCAI. |
| Observaciones | Sin inasistencias. (lunes hábil; 07/08 festivo) |

### Fila 2
| Campo | Texto |
|--------|--------|
| Descripción | Definición del mapeo técnico para las variables del Editor de Expresiones: IBC Pensión Prima Media ← label IBC Pensión - CPM e IBC Pensión ACCAI ← label IBC Pensión - ACCAI, ambas en la ventanilla IBC - Control Nómina, aplicables a liquidación de nómina y de contrato. |
| Competencias | Diseñar la solución de software de acuerdo con procedimientos y requisitos técnicos. |
| Inicio | 10/08/26 |
| Fin | 11/08/26 |
| Evidencia | Documento: tabla de mapeo label ↔ Campo / criterios de aceptación. |
| Observaciones | (lun–mar) |

### Fila 3
| Campo | Texto |
|--------|--------|
| Descripción | Elaboración de la guía de implementación del PBI 24152: checklist en ERP_Presentation/ERP_Services, patrón de registro de Campos IBC existente, binding al contexto de evaluación de fórmulas y plantilla SQL/VB.NET para catálogo del Editor de Expresiones. |
| Competencias | Desarrollar la solución de software de acuerdo con el diseño y metodologías de desarrollo. Utilizar herramientas informáticas de acuerdo con las necesidades de manejo de información. |
| Inicio | 11/08/26 |
| Fin | 12/08/26 |
| Evidencia | Producto: documento de solución técnica, snippet VB.NET y plantilla SQL. |
| Observaciones | (mar–mié) |

### Fila 4
| Campo | Texto |
|--------|--------|
| Descripción | Definición de casos de prueba y criterios de aceptación para las variables IBC Pensión Prima Media e IBC Pensión ACCAI: paridad con labels del formulario, uso en fórmulas del editor, regresión de Campos IBC previos y cobertura en liquidación de nómina y contrato. |
| Competencias | Controlar la calidad del servicio de software de acuerdo con los estándares técnicos. |
| Inicio | 12/08/26 |
| Fin | 13/08/26 |
| Evidencia | Documento: casos de prueba A/B/C y DoD del PBI 24152. |
| Observaciones | (mié–jue) |

### Fila 5
| Campo | Texto |
|--------|--------|
| Descripción | Documentación de evidencia y pasos de cierre del PBI 24152 en Azure DevOps: vinculación a historia padre 24034, dependencia del 27042, checklist de capturas del Editor de Expresiones e IBC - Control Nómina, y preparación para pruebas en ambiente QA con el equipo HCM. |
| Competencias | Implementar la solución de software de acuerdo con los requisitos de operación y modelos de referencia. Controlar la calidad del servicio de software de acuerdo con los estándares técnicos. |
| Inicio | 14/08/26 |
| Fin | 14/08/26 |
| Evidencia | Documento: plan de evidencia / checklist de merge y prueba en QA. |
| Observaciones | (viernes hábil; 17/08 festivo — no laborable) |

---

## Resumen de tiempos (control interno)

| Actividad | Fecha | Tiempo aprox. |
|-----------|--------|----------------|
| Análisis PBI 24152 / Ley 2381 | 10/08/2026 | 2–3 h |
| Mapeo variables CPM / ACCAI | 10–11/08/2026 | 2–3 h |
| Guía implementación + snippets | 11–12/08/2026 | 3–4 h |
| Casos de prueba / CA | 12–13/08/2026 | 2–3 h |
| Evidencia y cierre documental | 14/08/2026 | 2 h |
| **Total** | **10–14/08/2026** | **~11–15 h** |

---

## Archivos de la solución (para tu evidencia / repo personal)

| Archivo | Contenido |
|---------|-----------|
| `SOLUCION_PBI_24152_IBC_PENSION_PRIMA_MEDIA.md` | Solución completa |
| `mapas/PBI_24152_MAPEO_VARIABLES.md` | Mapeo corto |
| `snippets/RegisterIbcPensionExpressionFields.vb` | Snippet VB.NET |
| `sql/PBI_24152_ExpressionFields_TEMPLATE.sql` | Plantilla SQL |

## Qué falta hacer en la empresa (código real Vie HCM)

1. Abrir el repo del módulo Nómina / HCM.  
2. Buscar el registrador de Campos del Editor de Expresiones (patrón `[IBC Pensión]`).  
3. Confirmar que PBI 27042 ya deja valores en labels CPM/ACCAI.  
4. Registrar las dos variables + binding.  
5. Probar en QA y adjuntar capturas al 24152.  
