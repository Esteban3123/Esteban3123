# APLICAR — Habilitar Facturación Tipo Mandato (código real)

Formulario confirmado: **`FrmSettingBilling`** (`Presentacion.Billing`)  
Interfaz: **`ISettingBilling`** · Entidad: **`SettingsBilling`** · Modelo: **`MSettingBilling`**

Ancla existente: **`ApplyBasicBilling`** / control **`INDsleApplyBasicBilling`** (“Aplica Facturación Básica”).

Nombre técnico del nuevo parámetro (alineado al estilo del form):

| Capa | Nombre |
|------|--------|
| Label UI | Habilitar Facturación Tipo Mandato |
| Propiedad | `EnableMandateBilling` As Boolean |
| Control | `INDsleEnableMandateBilling` |
| Layout item | `INDLciEnableMandateBilling` |
| Columna BD / entidad | `EnableMandateBilling` (BIT, default 0) |

Valores: **True = Si**, **False = No** (mismo `listApply` que `ApplyBasicBilling`).  
Default: **False (No)**.

Aplica los cambios **en este orden**.

---

## 0) Designer (UI)

En el segmento **Información Adicional** (junto a `INDsleApplyBasicBilling` / `INDSleElectronicSalesTicket`):

1. Agregar `SearchLookUpEdit` o el mismo tipo de editor que use `INDsleApplyBasicBilling` → nombre **`INDsleEnableMandateBilling`**.
2. Agregar `LayoutControlItem` → **`INDLciEnableMandateBilling`**.
3. Caption / Text: **Habilitar Facturación Tipo Mandato**.
4. Visibility inicial: `Never` (se muestra en runtime con `HideGroup`).
5. No solapar Location con otros ítems del segmento.

Checklist Designer:

- [ ] Control creado
- [ ] Layout item en Información Adicional
- [ ] Caption correcto

Detalle de ejemplo: [snippets/FrmSettingBilling_Designer_APLICAR.md](snippets/FrmSettingBilling_Designer_APLICAR.md).

---

## 1) Interfaz `ISettingBilling`

Agregar (junto a `ApplyBasicBilling`):

```vb
''' <summary>
''' Habilitar Facturación Tipo Mandato (Si/No). Default: False (No).
''' Solo aplica cuando ApplyBasicBilling = True.
''' </summary>
Property EnableMandateBilling As Boolean
```

Archivo de referencia: [snippets/ISettingBilling_EnableMandateBilling.vb](snippets/ISettingBilling_EnableMandateBilling.vb).

---

## 2) Entidad `SettingsBilling` + BD

### 2.1 Propiedad en la entidad

```vb
''' <summary>
''' Habilitar Facturación Tipo Mandato. Default False = No.
''' </summary>
Public Property EnableMandateBilling As Boolean
```

Si la entidad se genera desde EDMX/DBML, regenerar tras el script SQL o mapear la columna a mano.

### 2.2 Script SQL

Usar [sql/Add_EnableMandateBilling_SettingsBilling.sql](sql/Add_EnableMandateBilling_SettingsBilling.sql):

- Columna `EnableMandateBilling BIT NOT NULL DEFAULT(0)`
- Backfill a `0` (No) en todas las UO
- Ajustar el nombre real de la tabla si no es `SettingsBilling` / `Billing.SettingsBilling`

También actualizar repositorio/SP/EF que lea y guarde `SettingsBilling` para incluir la columna (mismo sitio donde ya va `ApplyBasicBilling`).

---

## 3) `FrmSettingBilling.vb` — parches

### A1. Propiedad (después de `ApplyBasicBilling`)

Buscar:

```vb
    Public Property ApplyBasicBilling As Boolean Implements ISettingBilling.ApplyBasicBilling
        Get
            Return INDsleApplyBasicBilling.EditValue
        End Get
        Set(value As Boolean)
            INDsleApplyBasicBilling.EditValue = value
        End Set
    End Property
```

Agregar **inmediatamente después**:

```vb
    ''' <summary>
    ''' Habilitar Facturación Tipo Mandato (Si/No). Default: False (No).
    ''' Visible solo cuando ApplyBasicBilling = True.
    ''' </summary>
    Public Property EnableMandateBilling As Boolean Implements ISettingBilling.EnableMandateBilling
        Get
            If INDsleEnableMandateBilling.EditValue Is Nothing Then
                Return False
            End If
            Return CBool(INDsleEnableMandateBilling.EditValue)
        End Get
        Set(value As Boolean)
            INDsleEnableMandateBilling.EditValue = value
        End Set
    End Property
```

### A2. `InitializeTuples` — DataSource Si/No

Buscar el bloque donde se asigna `listApply` a `INDsleApplyBasicBilling`:

```vb
        INDsleApplyBasicBilling.Properties.DataSource = listApply.ToList()
        INDSleElectronicSalesTicket.Properties.DataSource = listApply.ToList()
```

Agregar una línea:

```vb
        INDsleApplyBasicBilling.Properties.DataSource = listApply.ToList()
        INDsleEnableMandateBilling.Properties.DataSource = listApply.ToList()
        INDSleElectronicSalesTicket.Properties.DataSource = listApply.ToList()
```

### A3. `HideGroup` — visibilidad condicionada

El form ya muestra/oculta controles de facturación básica en `HideGroup()` cuando cambia `ApplyBasicBilling`.  
Extender **ambos** branches.

En el `If ApplyBasicBilling Then` (después de los `Visibility = Always` existentes), agregar:

```vb
            INDLciEnableMandateBilling.AllowHide = False
            INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
```

En el `Else` (después de los `Visibility = Never` existentes), agregar:

```vb
            INDLciEnableMandateBilling.AllowHide = True
            INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            EnableMandateBilling = False
```

> Al ocultar se fuerza **No**, para no persistir Si “escondido” y cumplir el AC del gate.

`INDsleApplyBasicBilling_EditValueChanged` ya llama `HideGroup()` — no hace falta otro handler.

### A4. `FrmSettingBilling_Load` — default No

Buscar:

```vb
        ApplyBasicBilling = False
        GeneratePromissoryNote = False
```

Agregar:

```vb
        ApplyBasicBilling = False
        EnableMandateBilling = False
        GeneratePromissoryNote = False
```

### A5. `CleanControls` — reset No

Buscar:

```vb
        AssociateCostCenter = Nothing
        ApplyBasicBilling = False
        ValidateAgeOfMajority = False
```

Agregar:

```vb
        AssociateCostCenter = Nothing
        ApplyBasicBilling = False
        EnableMandateBilling = False
        ValidateAgeOfMajority = False
```

### A6. `LoadControls` — lectura desde entidad

Buscar:

```vb
                            ApplyBasicBilling = .ApplyBasicBilling
                            LiquidateSinceControlOutPatientService = .LiquidateSinceControlOutPatientService
```

Agregar:

```vb
                            ApplyBasicBilling = .ApplyBasicBilling
                            EnableMandateBilling = .EnableMandateBilling
                            LiquidateSinceControlOutPatientService = .LiquidateSinceControlOutPatientService
```

`HideGroup()` se dispara por el `EditValueChanged` de `ApplyBasicBilling` al asignar; si en algún build no se dispara durante carga, llamar `HideGroup()` al final del `With`.

### A7. `AssigningValues` — guardado

Buscar:

```vb
            .ApplyBasicBilling = ApplyBasicBilling
            .ValidateAgeOfMajority = ValidateAgeOfMajority
```

Agregar la asignación del mandato. Recomendado: solo persistir Si cuando aplica facturación básica:

```vb
            .ApplyBasicBilling = ApplyBasicBilling
            If ApplyBasicBilling Then
                .EnableMandateBilling = EnableMandateBilling
            Else
                .EnableMandateBilling = False
            End If
            .ValidateAgeOfMajority = ValidateAgeOfMajority
```

Alternativa mínima (si preferís una sola línea y ya forzás False en `HideGroup`):

```vb
            .EnableMandateBilling = EnableMandateBilling
```

---

## 4) Gate (AC6)

Cuando `EnableMandateBilling = False` (o `ApplyBasicBilling = False`), las funciones de Facturación Mandato no deben estar disponibles.

Usar [snippets/Gate_FacturacionMandato.vb](snippets/Gate_FacturacionMandato.vb):

```vb
Public Function IsMandateBillingEnabled(settings As SettingsBilling) As Boolean
    If settings Is Nothing Then Return False
    If Not settings.ApplyBasicBilling Then Return False
    Return settings.EnableMandateBilling
End Function
```

Aplicar en menús/botones de mandato (Presentation) y al inicio de operaciones de mandato (servicio).

---

## 5) Prueba rápida (AC)

| # | Acción | Esperado |
|---|--------|----------|
| 1 | UO con `ApplyBasicBilling = True` | Se ve **Habilitar Facturación Tipo Mandato** |
| 2 | Abrir combo | Solo Si / No |
| 3 | Registro nuevo / migrado | No |
| 4 | Guardar Si → cerrar app → abrir | Persiste Si |
| 5 | `ApplyBasicBilling = False` | Campo oculto; valor efectivo No |
| 6 | Parámetro No | Funciones de mandato no disponibles |

Detalle formal: [ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md).

---

## Archivos a tocar (checklist)

| Archivo | Cambio |
|---------|--------|
| `FrmSettingBilling.Designer.vb` | Control + LayoutItem |
| `FrmSettingBilling.vb` | A1–A7 |
| `ISettingBilling` (MVP) | Property |
| `SettingsBilling` (Domain.Entities) | Property + mapping |
| BD / repositorio | Columna BIT default 0 |
| UI/servicios mandato | Gate |

Snippet consolidado del form: [snippets/FrmSettingBilling_EnableMandateBilling.vb](snippets/FrmSettingBilling_EnableMandateBilling.vb).
