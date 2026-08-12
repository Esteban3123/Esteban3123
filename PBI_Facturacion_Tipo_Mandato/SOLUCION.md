# Solución técnica — Habilitar Facturación Tipo Mandato

| Campo | Valor |
|--------|--------|
| **Producto** | Vie Cloud Platform · VIE RCM · Facturación Salud |
| **Formulario** | Parámetros de Facturación |
| **Segmento UI** | Información Adicional |
| **Normativa / contexto** | Res. 165 de 2023 (FEV salud/comercial); falta contemplar factura mandato |
| **Prerrequisitos** | N/A |

---

## 1. Historia de usuario

**Como** usuario del proceso de facturación  
**Quiero** un campo nuevo que permita parametrizar si se va a generar facturación de tipo mandato o no  
**Así** puedo configurar la factura mandato a necesidad y cumplir con la necesidad de los clientes.

---

## 2. Qué pide exactamente el alcance

Crear un parámetro en **Parámetros de Facturación** con estas reglas:

| # | Especificación | Detalle |
|---|----------------|---------|
| 1 | Ubicación | Segmento **Información Adicional** |
| 2 | Nombre visible | **Habilitar Facturación Tipo Mandato** |
| 3 | Tipo | Combo / LookUp (SI / NO) |
| 4 | Default | **NO** para todas las unidades operativas de cada cliente |
| 5 | Condición de visualización | Solo se postula cuando **Aplica Facturación Básica** = **SI** |
| 6 | Persistencia | Guardar y recuperar el valor configurado |
| 7 | Efecto funcional (AC) | Si el parámetro = **NO**, las funcionalidades asociadas a Facturación de Mandato **no** deben estar disponibles |

### Fuera de alcance (este PBI)

- Generación completa del XML/DIAN de factura mandato (lógica tributaria de mandante/mandatario).
- Cambios al anexo técnico RIPS / FEV más allá del **flag de habilitación**.
- Rediseño del formulario de Parámetros de Facturación.

Este PBI entrega el **interruptor de parametrización** y el **gate** para que, cuando exista (o exista parcialmente) la funcionalidad de mandato, solo opere si el parámetro está en SI.

---

## 3. Contexto funcional

Hoy el sistema genera facturación electrónica de tipo **salud** y **comercial** (Res. 165/2023), pero **no** contempla la opción de factura mandato. El parámetro permite activar por cliente/UO esa modalidad sin afectar a quienes no la usan.

Relación con el campo existente:

```text
Aplica Facturación Básica = SI  →  se muestra "Habilitar Facturación Tipo Mandato"
Aplica Facturación Básica = NO  →  el campo se oculta; valor efectivo tratado como NO
```

---

## 4. Mapeo técnico recomendado

Usar nombres internos estables y texto UI en español.

| Capa | Nombre sugerido | Notas |
|------|-----------------|-------|
| Label UI | Habilitar Facturación Tipo Mandato | Exacto al PBI / mockup |
| Columna BD | `HabilitarFacturacionTipoMandato` o `EnableMandateBilling` | Bit / char(1) / tinyint — alinear al patrón de `AplicaFacturacionBasica` |
| Propiedad entidad/DTO | `EnableMandateBilling` / `HabilitarFacturacionTipoMandato` | Boolean: `False` = NO, `True` = SI |
| Control WinForms | `INDsleEnableMandateBilling` (LookUpEdit SI/NO) | Prefijo IND + patrón sle del formulario |
| Layout item | `INDLciEnableMandateBilling` | Dentro del grupo Información Adicional |
| Campo disparador | `AplicaFacturacionBasica` / control existente del form | `EditValueChanged` → mostrar/ocultar |

### Valores

| UI | Persistencia típica (Bit) | Persistencia típica (SI/NO char) |
|----|---------------------------|----------------------------------|
| NO | `0` / `False` | `'N'` |
| SI | `1` / `True` | `'S'` |

**Default al crear / migrar registros existentes:** NO.

---

## 5. Dónde tocar en el ERP (checklist)

Stack típico: **VB.NET · WinForms · DevExpress LayoutControl · SQL Server · Azure DevOps**  
Capas: Presentation / Business / Data · módulos Facturación Salud.

### 5.1 Localizar el formulario y el campo disparador

Buscar en el repo corporativo:

| Qué buscar | Para qué |
|------------|----------|
| `Parámetros de Facturación` / `ParametrosFacturacion` / `BillingParameters` | Formulario objetivo |
| `Aplica Facturación Básica` / `AplicaFacturacionBasica` / `BasicBilling` | Campo que condiciona la visibilidad |
| `Información Adicional` / `AdditionalInformation` | Segmento/layout group del mockup |
| `Estado de Folio Nuevo`, `Tiquete Electrónico de Venta` | Controles vecinos para ubicar el nuevo ítem |
| Tabla/SP de parámetros por UO | Persistencia |

**Regla:** copiar el patrón exacto del combo SI/NO de **Aplica Facturación Básica** (mismo data source, mismo binding, mismo tipo de columna).

### 5.2 Base de datos

1. Agregar columna nullable o NOT NULL con default NO (preferir NOT NULL + default).
2. Backfill: `UPDATE ... SET HabilitarFacturacionTipoMandato = 0` (o `'N'`) en todos los registros existentes.
3. Incluir la columna en SELECT/INSERT/UPDATE del SP o repositorio que lee/guarda parámetros.
4. No romper contratos de API: si hay DTO versionado, agregar la propiedad con default `false`.

Plantilla: [sql/Add_HabilitarFacturacionTipoMandato.sql](sql/Add_HabilitarFacturacionTipoMandato.sql).

### 5.3 Backend / servicios

| Pieza | Cambio |
|-------|--------|
| Entidad / EDMX / DTO de parámetros | Nueva propiedad Boolean |
| Mapper / AutoMapper | Mapear columna ↔ propiedad |
| Get parámetros por UO/cliente | Devolver el valor (default NO si null) |
| Save parámetros | Persistir el valor del formulario |
| Gate de negocio (recomendado) | Método `IsMandateBillingEnabled(operatingUnitId)` usado por menús/procesos de mandato |

### 5.4 Frontend / cliente (Parámetros de Facturación)

1. Agregar LookUpEdit SI/NO en segmento **Información Adicional** (debajo de los campos existentes del segmento; el mockup lo marca al final de esa columna).
2. Caption: `Habilitar Facturación Tipo Mandato`.
3. En `Load` / `New` / `CleanControls`: valor por defecto **NO**.
4. En `EditValueChanged` de **Aplica Facturación Básica**:
   - SI → `Visibility = Always` (o `True`) del layout item.
   - NO → ocultar el layout item y forzar valor **NO** (evita guardar SI “oculto”).
5. Binding get/set null-safe (mismo estilo que otros combos del form).
6. Incluir el valor en el objeto que se envía al guardar.

Snippets: [snippets/BillingParameters_Mandate.vb](snippets/BillingParameters_Mandate.vb).

### 5.5 Seguridad / gate de funcionalidades de mandato

Cuando el parámetro es **NO** (o el campo no aplica porque Facturación Básica = NO):

- No mostrar opciones de menú / botones / pestañas de Facturación Mandato.
- Si un usuario intenta invocar la función por otra vía, el servicio debe rechazar con mensaje claro.
- Auditoría: el valor del parámetro queda en el registro de parámetros (panel Auditoría del form si ya existe).

Snippet: [snippets/Gate_FacturacionMandato.vb](snippets/Gate_FacturacionMandato.vb).

---

## 6. Consideraciones técnicas (relleno del PBI)

### 6.1 Base de datos

- Nueva columna en la tabla de parámetros de facturación por unidad operativa.
- Script idempotente + backfill a NO.
- Actualizar SP/queries de lectura y escritura.

### 6.2 Servicios / Backend

- Extender contrato de parámetros.
- Exponer helper/gate `IsMandateBillingEnabled`.
- Sin cambios de integración DIAN en este PBI (solo flag).

### 6.3 Frontend / Cliente

- Control SI/NO en Información Adicional.
- Visibilidad condicionada a Aplica Facturación Básica = SI.
- Default NO; persistencia al guardar.

### 6.4 Seguridad y rendimiento

- Sin impacto de performance (1 bit/flag por UO).
- Respeta permisos existentes del formulario.
- Gate evita exposición de funciones de mandato cuando el flag es NO.

---

## 7. Plan de pruebas (QA)

| # | Caso | Resultado esperado |
|---|------|--------------------|
| 1 | Abrir Parámetros con Facturación Básica = SI | Se ve el nuevo campo; valor NO si nunca se configuró |
| 2 | Cambiar a SI, guardar, cerrar app, volver a abrir | Persiste SI |
| 3 | Cambiar Facturación Básica a NO | El campo desaparece; al guardar queda efectivo NO |
| 4 | Volver Facturación Básica a SI | Campo visible; valor recuperado o NO según regla de producto (recomendado: mantener último guardado solo si se forzó NO al ocultar — documentar en QA) |
| 5 | Parámetro = NO | Menús/acciones de mandato no disponibles |
| 6 | Parámetro = SI | Funciones de mandato disponibles (si ya existen en el build) |
| 7 | Migración de UO existentes | Todas quedan en NO sin intervención manual |

**Recomendación de producto al ocultar:** al pasar Aplica Facturación Básica a NO, setear el parámetro mandato a NO antes de guardar, para cumplir el AC de “funcionalidades no disponibles”.

---

## 8. Riesgos y supuestos

| Riesgo / supuesto | Mitigación |
|-------------------|------------|
| Nombres reales de tabla/formulario distintos | Buscar por `AplicaFacturacionBasica` y ajustar plantillas |
| Ya existen pantallas de mandato sin gate | Aplicar el gate en Presentation **y** en servicio |
| El combo SI/NO usa códigos distintos (1/0 vs S/N) | Reutilizar el mismo `DataSource` que Aplica Facturación Básica |
| Multi-UO | Default NO por cada registro de UO; no un solo flag global de cliente |

---

## 9. Entregables para Azure DevOps / Bitácora

- Documentar en el PBI: columna, formulario, condición de visibilidad, default NO.
- Adjuntar evidencia: captura del campo en Información Adicional con Facturación Básica = SI, y captura oculto con = NO.
- Evidencia de persistencia tras reiniciar sesión.
