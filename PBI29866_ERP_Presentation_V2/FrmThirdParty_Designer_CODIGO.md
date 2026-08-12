# FrmThirdParty.Designer.vb — BLOQUES FINALES (reemplaza cada bloque completo)

## BLOQUE A — INDLyGrThirdPartyBasicData.Items.AddRange

```vb
        Me.INDLyGrThirdPartyBasicData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemNit, Me.INDLyItemName, Me.INDLyItemPersonType, Me.INDLyVC, Me.INDlyItemClass, Me.INDLciCIIU, Me.LayoutControlItem2, Me.INDlyItemHandlesBranchOffice, Me.INDLyItemIdentificationTypeJuridic})
```

## BLOQUE B — INDLyGrParameter.Items.AddRange

```vb
        Me.INDLyGrParameter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemRetentionType, Me.INDLyItemContributionType, Me.INDLyItemIca, Me.INDLyItemIcaPercentage, Me.INDLyItemIcaTop, Me.INDLyItemIcaTopValue, Me.INDlyItemEntityCode, Me.INDlciIVARetentionAccountPayableConceptId, Me.INDlyItemStateEnterpriseType, Me.INDLCCodDivipola, Me.INDlyItemIVARetentionConcept})
```

## BLOQUE C — INDLciCIIU (final del bloque)

```vb
        Me.INDLciCIIU.Control = Me.INDTxtCIIU
        resources.ApplyResources(Me.INDLciCIIU, "INDLciCIIU")
        Me.INDLciCIIU.Location = New System.Drawing.Point(0, 108)
        Me.INDLciCIIU.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLciCIIU.MinSize = New System.Drawing.Size(450, 36)
        Me.INDLciCIIU.Name = "INDLciCIIU"
        Me.INDLciCIIU.Size = New System.Drawing.Size(450, 36)
        Me.INDLciCIIU.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCIIU.Spacing = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2)
        Me.INDLciCIIU.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCIIU.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCIIU.TextToControlDistance = 12
        Me.INDLciCIIU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

## BLOQUE D — INDlygEconomicActivity (final del bloque de grupo)

```vb
        Me.INDlygEconomicActivity.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEconomicActivity, Me.INDlciFiscalResponsabilities1, Me.INDlyItemAddEconomicActivity})
        Me.INDlygEconomicActivity.Location = New System.Drawing.Point(2356, 0)
        Me.INDlygEconomicActivity.Name = "INDlygEconomicActivity"
        Me.INDlygEconomicActivity.Size = New System.Drawing.Size(524, 485)
        Me.INDlygEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

## BLOQUE E (opcional) — INDsleClass obligatorio

```vb
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleClass, True)
```
y
```vb
        Me.INDsleClass.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
```
