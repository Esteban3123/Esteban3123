# Solución técnica — Habilitar Facturación Tipo Mandato

| Campo | Valor |
|--------|--------|
| **Formulario** | `FrmSettingBilling` (`Presentacion.Billing`) — Parámetros de Facturación |
| **Interfaz** | `ISettingBilling` |
| **Entidad** | `SettingsBilling` |
| **Modelo** | `MSettingBilling` |
| **Campo disparador** | `ApplyBasicBilling` / `INDsleApplyBasicBilling` |
| **Nuevo campo** | `EnableMandateBilling` / `INDsleEnableMandateBilling` |
| **Segmento UI** | Información Adicional |
| **Label** | Habilitar Facturación Tipo Mandato |

---

## 1. Historia

**Como** usuario del proceso de facturación  
**Quiero** parametrizar si se genera facturación tipo mandato  
**Así** configuro la factura mandato según el cliente.

---

## 2. Comportamiento (alineado al código real)

El form ya usa:

- Combo Si/No con `listApply` (`True`/`False`)
- `HideGroup()` al cambiar `ApplyBasicBilling`
- `LoadControls` / `AssigningValues` / `CleanControls` para persistir `SettingsBilling`

El nuevo parámetro **copia ese patrón**:

| Evento | Comportamiento |
|--------|----------------|
| `ApplyBasicBilling = True` | Muestra `INDLciEnableMandateBilling` |
| `ApplyBasicBilling = False` | Oculta el campo y fuerza `EnableMandateBilling = False` |
| Alta / Clean / Load sin valor | Default **False (No)** |
| Guardar | Persiste en `SettingsBilling.EnableMandateBilling` |
| Gate | Funciones de mandato solo si Básica=Si **y** Mandato=Si |

---

## 3. Mapeo confirmado

| UI (mockup) | Código |
|-------------|--------|
| Aplica Facturación Básica | `ApplyBasicBilling` |
| Habilitar Facturación Tipo Mandato | `EnableMandateBilling` |
| Si / No | `listApply` Boolean |
| Parámetros por UO | `GetSettingsBillingByIdUnitOperative` / `SaveSettingBilling` |

---

## 4. Capas a tocar

Ver checklist en [APLICAR.md](APLICAR.md).

1. Designer + `FrmSettingBilling.vb` (A1–A7)
2. `ISettingBilling` + `SettingsBilling`
3. Columna BD `EnableMandateBilling BIT DEFAULT 0`
4. Gate en menús/servicios de mandato

---

## 5. Fuera de alcance de este PBI

- Generación XML DIAN de factura mandato (mandante/mandatario)
- Cambios RIPS / anexo técnico más allá del flag

---

## 6. Criterios de aceptación

[ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md)
