# Designer — FrmSettingBilling (Información Adicional)

Agregar un combo Si/No igual a `INDsleApplyBasicBilling`.

## Controles

| Control | Tipo | Notas |
|---------|------|-------|
| `INDsleEnableMandateBilling` | Mismo editor que `INDsleApplyBasicBilling` (SearchLookUpEdit / LookUpEdit / GridLookUpEdit) | ValueMember/DisplayMember iguales al de `listApply` (Boolean / String) |
| `INDLciEnableMandateBilling` | `LayoutControlItem` | Parent = layout group **Información Adicional** |

## Caption

```text
Habilitar Facturación Tipo Mandato
```

## Visibility inicial (Designer)

```vb
Me.INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

En runtime `HideGroup()` la pone en `Always` si `ApplyBasicBilling = True`.

## Ubicación sugerida

Al final del segmento Información Adicional, cerca de:

- `INDsleApplyBasicBilling` (Aplica Facturación Básica)
- `INDSleElectronicSalesTicket` (Tiquete Electrónico de Venta)
- `INDGleIncomeLockType` (Tipo Bloqueo de Ingreso)

## Checklist anti-solape

- [ ] `Location` distinto al de los ítems vecinos
- [ ] Mismo `TextSize` / `TextAlignMode` del segmento
- [ ] Incluido en el `Items` del layout group de Información Adicional
- [ ] **No** queda dentro de un group que solo aparece con otra condición (salvo Básica)

## DataSource

No hardcodear en Designer: se asigna en `InitializeTuples()` con `listApply` (True="Si", False="No"), igual que `INDsleApplyBasicBilling`.
