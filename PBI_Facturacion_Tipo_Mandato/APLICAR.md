# APLICAR — Habilitar Facturación Tipo Mandato

Aplica estos cambios **en orden** sobre el código corporativo de Facturación Salud (VIE RCM).  
Archivos típicos (nombres orientativos — confirmar con búsqueda):

| Capa | Archivos a ubicar |
|------|-------------------|
| UI | `FrmParametrosFacturacion*.vb` / `FrmBillingParameters*.vb` + `.Designer.vb` |
| Contrato | DTO / interfaz de parámetros de facturación |
| Datos | Tabla + SP Get/Save de parámetros por UO |
| Gate | Menús / presenters / servicios de Facturación Mandato |

**Ancla de búsqueda:** `Aplica Facturación Básica` / `AplicaFacturacionBasica`.

---

## 0) Confirmación previa (15 min)

1. Abrir el formulario **Parámetros de Facturación** en el código.
2. Localizar el layout group **Información Adicional**.
3. Anotar:
   - Nombre real del control de **Aplica Facturación Básica**
   - Tipo de valor (Boolean / `S`/`N` / `1`/`0`)
   - Tabla y columna equivalentes en BD
4. Copiar **exactamente** ese patrón para el nuevo campo.

---

## 1) Base de datos

1. Abrir [sql/Add_HabilitarFacturacionTipoMandato.sql](sql/Add_HabilitarFacturacionTipoMandato.sql).
2. Reemplazar `dbo.BillingParameters` por el nombre real.
3. Elegir Opción A (BIT) u Opción B (CHAR) según el patrón existente.
4. Ejecutar en **QA** (el script viene con `ROLLBACK` por defecto).
5. Actualizar SP/repositorio Get y Save para incluir la columna.
6. Verificar backfill: todos los registros en **NO**.

Checklist:

- [ ] Columna creada
- [ ] Default NO
- [ ] Backfill OK
- [ ] Get devuelve el valor
- [ ] Save persiste el valor

---

## 2) DTO / entidad / mapper

Agregar propiedad (ejemplo):

```vb
''' <summary>Habilitar Facturación Tipo Mandato. Default False = NO.</summary>
Public Property EnableMandateBilling As Boolean
```

o

```vb
Public Property HabilitarFacturacionTipoMandato As Boolean
```

- [ ] Default = `False` en constructor / materialización
- [ ] Mapper BD ↔ DTO
- [ ] Null de BD se interpreta como `False`

---

## 3) Designer (UI)

En el segmento **Información Adicional**:

1. Agregar `LookUpEdit` → `INDsleEnableMandateBilling`.
2. Agregar `LayoutControlItem` → `INDLciEnableMandateBilling`.
3. Text/Caption: **Habilitar Facturación Tipo Mandato**.
4. Mismo `DataSource` SI/NO que Aplica Facturación Básica.
5. Posición: al final del segmento (después de Tiquete Electrónico de Venta, según mockup).
6. `Visibility = Never` inicial (el runtime la activa).

Ver [mapas/UBICACION_UI.md](mapas/UBICACION_UI.md).

- [ ] Campo visible en diseño dentro de Información Adicional
- [ ] No solapa otros layout items (Location distinto)

---

## 4) Code-behind del formulario

Tomar [snippets/BillingParameters_Mandate.vb](snippets/BillingParameters_Mandate.vb) y adaptar nombres.

Orden sugerido de enganches:

| Momento | Qué llamar |
|---------|------------|
| Constructor / Load data source SI-NO | Asignar DataSource al nuevo LookUp |
| Nuevo / CleanControls | `ApplyMandateBillingDefaults()` |
| Después de cargar entidad | `LoadMandateBillingFromEntity(...)` |
| `EditValueChanged` de Aplica Facturación Básica | `RefreshMandateBillingVisibility()` |
| Antes de Save | `MapMandateBillingToEntity(...)` |

Reglas obligatorias:

1. Default **NO**.
2. Si Aplica Facturación Básica ≠ SI → ocultar y forzar **NO**.
3. Guardar el valor solo efectivo (con Básica = NO → NO).

- [ ] Default NO
- [ ] Visibilidad condicionada
- [ ] Save/Load OK

---

## 5) Gate de funcionalidades de mandato (AC6)

Usar [snippets/Gate_FacturacionMandato.vb](snippets/Gate_FacturacionMandato.vb).

1. En Presentation: ocultar/deshabilitar botones, pestañas o ítems de menú de mandato cuando `IsMandateBillingEnabled` = False.
2. En servicios: `EnsureMandateBillingAllowed` al inicio de operaciones de mandato.
3. No confiar solo en ocultar UI.

- [ ] UI oculta con parámetro NO
- [ ] Servicio rechaza con parámetro NO
- [ ] Con parámetro SI (y Básica SI) las funciones se habilitan

---

## 6) Prueba manual (AC)

Seguir [ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md):

| Paso | Acción | Esperado |
|------|--------|----------|
| 1 | Básica = SI | Se ve el nuevo campo |
| 2 | Solo opciones SI/NO | OK |
| 3 | Registro nuevo / migrado | NO |
| 4 | Guardar SI → cerrar app → abrir | Sigue SI |
| 5 | Básica = NO | Campo oculto |
| 6 | Parámetro NO | Sin funciones de mandato |

---

## 7) Evidencias para el PBI

Adjuntar en Azure DevOps:

1. Captura Information Adicional con el campo (Básica = SI, valor NO).
2. Captura con valor SI guardado.
3. Captura con Básica = NO (campo no visible).
4. Script SQL ejecutado en QA + resultado del SELECT de verificación.
5. Checklist AC marcado.

---

## Notas rápidas de implementación

- **No inventar** un DataSource SI/NO distinto: reutilizar el del formulario.
- Si el layout usa `LayoutVisibility`, preferirlo sobre `Visible` del control suelto.
- Multi-UO: el flag es **por unidad operativa**, no un switch global del cliente.
- Este PBI **no** implementa el XML DIAN de mandato; solo el parámetro y el gate.
