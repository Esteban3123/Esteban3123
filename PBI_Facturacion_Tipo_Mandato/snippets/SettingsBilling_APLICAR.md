# APLICAR — SettingsBilling (Domain.Entities)

La entidad se usa en `FrmSettingBilling` como:

```vb
Dim settingBilling As SettingsBilling
' Load:  Model.GetSettingsBillingByIdUnitOperative(...)
' Save:  Model.SaveSettingBilling(settingBilling)
' Map:   .ApplyBasicBilling = ApplyBasicBilling
```

Todavía **no** tenemos el `.vb` de la entidad en este repo. Aplica buscando `ApplyBasicBilling` en `SettingsBilling` (o en el EDMX / partial class).

---

## 1) Propiedad (mismo tipo que ApplyBasicBilling)

**Buscar** en `SettingsBilling` (o partial):

```vb
Public Property ApplyBasicBilling As Boolean
```

**Agregar inmediatamente después** (o en el mismo bloque de flags booleanos):

```vb
''' <summary>
''' Habilitar Facturación Tipo Mandato (Si/No). Default: False (No).
''' Solo aplica cuando ApplyBasicBilling = True.
''' </summary>
Public Property EnableMandateBilling As Boolean
```

Si la entidad usa backing field + ChangeTracker (self-tracking), **copiar el patrón exacto** de `ApplyBasicBilling`. Ejemplo típico Indigo/Vie:

```vb
Private _enableMandateBilling As Boolean

Public Property EnableMandateBilling As Boolean
    Get
        Return _enableMandateBilling
    End Get
    Set(value As Boolean)
        If _enableMandateBilling <> value Then
            _enableMandateBilling = value
            OnPropertyChanged("EnableMandateBilling")
            ' o MarkAsModified / ChangeTracker según el patrón del archivo
        End If
    End Set
End Property
```

> Si `ApplyBasicBilling` es auto-property simple, usa auto-property. No inventes ChangeTracker si el resto del flag no lo usa.

---

## 2) Default en constructor / inicialización

Si `SettingsBilling` tiene constructor o inicialización de flags:

```vb
EnableMandateBilling = False
```

junto a donde ya pongan `ApplyBasicBilling = False` (si existe).

En alta desde el form (`New SettingsBilling`), el form también fuerza default en Load/Clean; el default de BD cubre registros migrados.

---

## 3) Base de datos

Script: [../sql/Add_EnableMandateBilling_SettingsBilling.sql](../sql/Add_EnableMandateBilling_SettingsBilling.sql)

1. Localizar tabla real:

```sql
SELECT s.name AS [Schema], t.name AS [Table], c.name AS [Column]
FROM sys.columns c
JOIN sys.tables t ON c.object_id = t.object_id
JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE c.name = 'ApplyBasicBilling';
```

2. Ajustar `@Schema` / `@Table` en el script.
3. Ejecutar en QA → verificar EnNo = TotalRegistros.
4. `COMMIT` solo tras validar.

---

## 4) Persistencia (EF / SP / repositorio)

Donde ya se lea/guarde `ApplyBasicBilling`, incluir `EnableMandateBilling`:

| Pieza | Acción |
|-------|--------|
| EDMX / Code First / Fluent map | Mapear columna ↔ propiedad |
| SP Get por UO | Agregar columna al SELECT |
| SP Save/Update | Agregar parámetro + SET |
| DTO intermedio (si hay) | Misma propiedad Boolean |
| `MSettingBilling` | Suele reutilizar la entidad; no requiere lógica extra si el mapping está completo |

No hace falta tocar `PSettingBilling` salvo que valide campos a mano.

---

## 5) Checklist entidad + BD

- [ ] Propiedad `EnableMandateBilling` en `SettingsBilling`
- [ ] Mismo patrón de notificación/ChangeTracker que `ApplyBasicBilling`
- [ ] Columna BIT NOT NULL DEFAULT(0)
- [ ] Backfill No en todas las UO
- [ ] Get/Save incluyen la columna
- [ ] Prueba: guardar Si → reabrir → sigue Si

---

## 6) Si pegas el archivo

Con el contenido de `SettingsBilling.vb` (o el partial + fragmento EDMX) se genera el **diff línea por línea** igual que en `ISettingBilling_APLICAR.md`.
