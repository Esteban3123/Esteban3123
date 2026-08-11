# FrmThirdParty.Designer.vb — PBI 29866 (sobre tu base en cero)

Solo **4 cambios**. El resto del Designer no se toca.
El layout fino Natural/Jurídico lo hace el runtime en `INDslePersonTypeEditValueChangedManual`.

---

## 1) Segmento 1 (datos básicos): incluir Clase

**Buscar:**
```vb
        Me.INDLyGrThirdPartyBasicData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemNit, Me.INDLyItemName, Me.INDLyItemPersonType, Me.INDLyVC, Me.INDLciCIIU, Me.LayoutControlItem2, Me.INDlyItemHandlesBranchOffice, Me.INDLyItemIdentificationTypeJuridic})
```

**Reemplazar por:**
```vb
        Me.INDLyGrThirdPartyBasicData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemNit, Me.INDLyItemName, Me.INDLyItemPersonType, Me.INDLyVC, Me.INDlyItemClass, Me.INDLciCIIU, Me.LayoutControlItem2, Me.INDlyItemHandlesBranchOffice, Me.INDLyItemIdentificationTypeJuridic})
```

---

## 2) Parámetros: quitar Clase

**Buscar:**
```vb
        Me.INDLyGrParameter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemRetentionType, Me.INDLyItemContributionType, Me.INDLyItemIca, Me.INDLyItemIcaPercentage, Me.INDLyItemIcaTop, Me.INDLyItemIcaTopValue, Me.INDlyItemClass, Me.INDlyItemEntityCode, Me.INDlciIVARetentionAccountPayableConceptId, Me.INDlyItemStateEnterpriseType, Me.INDLCCodDivipola, Me.INDlyItemIVARetentionConcept})
```

**Reemplazar por:**
```vb
        Me.INDLyGrParameter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemRetentionType, Me.INDLyItemContributionType, Me.INDLyItemIca, Me.INDLyItemIcaPercentage, Me.INDLyItemIcaTop, Me.INDLyItemIcaTopValue, Me.INDlyItemEntityCode, Me.INDlciIVARetentionAccountPayableConceptId, Me.INDlyItemStateEnterpriseType, Me.INDLCCodDivipola, Me.INDlyItemIVARetentionConcept})
```

---

## 3) Ocultar CIIU por defecto

En el bloque `'INDLciCIIU`, **después** de:
```vb
        Me.INDLciCIIU.TextToControlDistance = 12
```

**Agregar:**
```vb
        Me.INDLciCIIU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

Quedaría así el final del bloque:
```vb
        Me.INDLciCIIU.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCIIU.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCIIU.TextToControlDistance = 12
        Me.INDLciCIIU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

---

## 4) Ocultar grupo Actividad económica por defecto

En el bloque `'INDlygEconomicActivity`, **después** de:
```vb
        Me.INDlygEconomicActivity.Size = New System.Drawing.Size(524, 485)
```

**Agregar:**
```vb
        Me.INDlygEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

Quedaría:
```vb
        Me.INDlygEconomicActivity.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEconomicActivity, Me.INDlciFiscalResponsabilities1, Me.INDlyItemAddEconomicActivity})
        Me.INDlygEconomicActivity.Location = New System.Drawing.Point(2356, 0)
        Me.INDlygEconomicActivity.Name = "INDlygEconomicActivity"
        Me.INDlygEconomicActivity.Size = New System.Drawing.Size(524, 485)
        Me.INDlygEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

---

## (Opcional) Clase obligatoria visual

En `'INDsleClass`, cambiar:
```vb
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleClass, False)
```
por:
```vb
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleClass, True)
```

Y en Appearance del control, si quieres el fondo MistyRose como los demás obligatorios:
```vb
        Me.INDsleClass.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
```
(en vez del blanco actual).

---

## Checklist Designer

- [ ] Clase está en `INDLyGrThirdPartyBasicData`
- [ ] Clase **no** está en `INDLyGrParameter`
- [ ] `INDLciCIIU.Visibility = Never`
- [ ] `INDlygEconomicActivity.Visibility = Never`

Cuando termines, pégame el **PThirdParty.vb**.
