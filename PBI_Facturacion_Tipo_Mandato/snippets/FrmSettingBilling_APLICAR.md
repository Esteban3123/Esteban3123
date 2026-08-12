# APLICAR — FrmSettingBilling.vb (Presentacion.Billing)

Archivo confirmado. Resumen de parches A1–A7. Detalle consolidado también en [../APLICAR.md](../APLICAR.md).

Snippet de fragmentos: [FrmSettingBilling_EnableMandateBilling.vb](FrmSettingBilling_EnableMandateBilling.vb)

---

## Dependencias previas

1. Designer: `INDsleEnableMandateBilling` + `INDLciEnableMandateBilling`
2. `ISettingBilling.EnableMandateBilling` ([ISettingBilling_APLICAR.md](ISettingBilling_APLICAR.md))
3. `SettingsBilling.EnableMandateBilling` + columna BD ([SettingsBilling_APLICAR.md](SettingsBilling_APLICAR.md))

---

## A1 — Propiedad (después de ApplyBasicBilling)

**Buscar** el bloque `ApplyBasicBilling` Implements…  
**Agregar después:**

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

---

## A2 — InitializeTuples

Tras:

```vb
        INDsleApplyBasicBilling.Properties.DataSource = listApply.ToList()
```

agregar:

```vb
        INDsleEnableMandateBilling.Properties.DataSource = listApply.ToList()
```

---

## A3 — HideGroup

En `If ApplyBasicBilling Then` agregar:

```vb
            INDLciEnableMandateBilling.AllowHide = False
            INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
```

En `Else` agregar:

```vb
            INDLciEnableMandateBilling.AllowHide = True
            INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            EnableMandateBilling = False
```

`INDsleApplyBasicBilling_EditValueChanged` ya llama `HideGroup()` — no crear otro handler.

---

## A4 — Load (default No)

Tras `ApplyBasicBilling = False` en `FrmSettingBilling_Load`:

```vb
        EnableMandateBilling = False
```

---

## A5 — CleanControls

Tras `ApplyBasicBilling = False`:

```vb
        EnableMandateBilling = False
```

---

## A6 — LoadControls

Tras `ApplyBasicBilling = .ApplyBasicBilling`:

```vb
                            EnableMandateBilling = .EnableMandateBilling
```

---

## A7 — AssigningValues

Tras `.ApplyBasicBilling = ApplyBasicBilling`:

```vb
            If ApplyBasicBilling Then
                .EnableMandateBilling = EnableMandateBilling
            Else
                .EnableMandateBilling = False
            End If
```

---

## Checklist form

- [ ] A1–A7 aplicados
- [ ] Compila Presentation.Billing
- [ ] Básica=Si → se ve el campo
- [ ] Básica=No → se oculta y queda No
- [ ] Guardar Si / No persiste tras reiniciar
