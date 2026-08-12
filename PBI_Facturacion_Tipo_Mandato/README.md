# PBI — Habilitar Facturación Tipo Mandato

Parámetro nuevo en **Parámetros de Facturación** (VIE RCM · Facturación Salud) para configurar si el cliente genera factura electrónica de tipo mandato.

| Campo | Valor |
|--------|--------|
| **Módulo** | Facturación Salud · VIE RCM |
| **Formulario** | Parámetros de Facturación |
| **Segmento** | Información Adicional |
| **Campo UI** | Habilitar Facturación Tipo Mandato |
| **Valores** | SI / NO |
| **Default** | NO (todas las UO / clientes) |
| **Visibilidad** | Solo si **Aplica Facturación Básica** = SI |

> Este repositorio no contiene el código fuente corporativo de Vie ERP. Los entregables son **guías, scripts y snippets** para aplicar en el repo de Facturación Salud (`ERP_Presentation` / servicios / BD).

## Entregables

| Archivo | Uso |
|---------|-----|
| [SOLUCION.md](SOLUCION.md) | Análisis técnico completo (BD, backend, frontend, AC) |
| [ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md) | Criterios de aceptación en formato Dado/Cuando/Entonces |
| [APLICAR.md](APLICAR.md) | Pasos ordenados para implementar en el código real |
| [mapas/UBICACION_UI.md](mapas/UBICACION_UI.md) | Ubicación del campo en el mockup |
| [sql/Add_HabilitarFacturacionTipoMandato.sql](sql/Add_HabilitarFacturacionTipoMandato.sql) | Plantilla SQL (columna + default NO) |
| [snippets/BillingParameters_Mandate.vb](snippets/BillingParameters_Mandate.vb) | Propiedad, binding y visibilidad condicionada |
| [snippets/Gate_FacturacionMandato.vb](snippets/Gate_FacturacionMandato.vb) | Gate: ocultar/deshabilitar funciones de mandato si = NO |

## Orden de aplicación recomendado

1. Confirmar en el repo corporativo el formulario y la tabla de parámetros (buscar `AplicaFacturacionBasica` / `Aplica Facturación Básica`).
2. Ejecutar el script SQL en QA (ajustar schema/tabla/columna reales).
3. Extender DTO/entidad/servicio de guardado-lectura.
4. Agregar el control en el segmento **Información Adicional** y la lógica de visibilidad.
5. Aplicar el gate en menús/acciones de Facturación Mandato.
6. Validar los AC de [ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md).
