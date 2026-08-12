# Designer — FrmSettingBilling (Información Adicional)

Agregar un combo Si/No **igual** a `INDsleApplyBasicBilling`.

Si pegas el bloque Designer de `INDsleApplyBasicBilling` + su `LayoutControlItem`, se genera el diff exacto (como con `ISettingBilling`).

---

## Controles a crear

| Control | Tipo | Notas |
|---------|------|-------|
| `INDsleEnableMandateBilling` | Mismo tipo que `INDsleApplyBasicBilling` | ValueMember/DisplayMember del `listApply` (Boolean / String) |
| `INDLciEnableMandateBilling` | `LayoutControlItem` | Parent = layout group **Información Adicional** |

## Caption

```text
Habilitar Facturación Tipo Mandato
```

En Designer (propiedad Text del LayoutControlItem), o en `.resx` si el form localiza captions.

## Visibility inicial

```vb
Me.INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

Runtime: `HideGroup()` → `Always` si `ApplyBasicBilling = True`.

## Ubicación sugerida

Al final del segmento Información Adicional, cerca de:

- `INDsleApplyBasicBilling`
- `INDSleElectronicSalesTicket`
- `INDGleIncomeLockType`

## Cómo clonarlo en Visual Studio (recomendado)

1. Abrir `FrmSettingBilling` en diseñador.
2. Copiar el control `INDsleApplyBasicBilling` (Copy/Paste).
3. Renombrar a `INDsleEnableMandateBilling`.
4. Renombrar su LayoutControlItem a `INDLciEnableMandateBilling`.
5. Text = **Habilitar Facturación Tipo Mandato**.
6. Visibility = Never.
7. Quitar DataSource del Designer si quedó serializado (se asigna en `InitializeTuples`).

## Checklist anti-solape

- [ ] `Location` distinto a vecinos
- [ ] Mismo `TextSize` / `TextAlignMode` del segmento
- [ ] En el `Items` del layout group Información Adicional
- [ ] No queda solo dentro de un group que dependa de otra condición (salvo Básica vía `HideGroup`)
- [ ] Friend WithEvents declarado (lo genera el Designer)

## DataSource

No hardcodear Si/No en Designer. En `InitializeTuples()`:

```vb
INDsleEnableMandateBilling.Properties.DataSource = listApply.ToList()
```

igual que `INDsleApplyBasicBilling`.

## Patrón de visibilidad de referencia

El form ya hace lo mismo con MiPres / Tiquete electrónico:

- `INDsleMiPres_EditValueChanged` → muestra ClientId/Secret  
- `INDSleElectronicSalesTicket_EditValueChanged` → muestra `INDlygElectronicSalesTicket`  
- `INDsleApplyBasicBilling_EditValueChanged` → `HideGroup()` ← **aquí se engancha el mandato**
