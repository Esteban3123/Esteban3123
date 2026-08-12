# PBI 29866 — Cambios exactos sobre el FrmThirdParty.vb que pegaste

Tu archivo es la **base en cero**. Aplica estos cambios en Visual Studio (o cópialos uno a uno).  
También aplica `PThirdParty.vb` de esta carpeta y el Designer (`FrmThirdParty_Designer_APLICAR.md`).

---

## Resumen de requisitos cubiertos

| # | Requisito | Cambio |
|---|-----------|--------|
| 1 | Layout Clase en Segmento 1; Natural/Jurídico | `INDslePersonTypeEditValueChangedManual` |
| 2 | NIT Nacional numérico + DV; Extranjero alfanumérico sin DV | `ApplyNitRulesByClass` + evento Clase + `PThirdParty` |
| 3 | Doble confirmación identificación (nuevo) | `ConfirmIdentification` + KeyDown |
| 4 | Búsqueda/carga sin reconfirmar | flags en Load / ReturnValue / IdEntityLoaded |
| 5 | Clase obligatoria Natural y Jurídico | `ValidateControlsForm` + `.Class = ClassThirdParty` en Natural |
| 6 | Sin regresión (null-safe, Deshacer) | Gets + quitar `OnlyFind` en ClickDeshacer |

---

## 1. Variables (después de `_indexEditRecord`)

Agregar:

```vb
    ''' <summary>
    ''' PBI 29866: identificación pendiente de confirmación (doble ingreso)
    ''' </summary>
    Private _pendingIdentification As String = Nothing

    ''' <summary>
    ''' PBI 29866: True si ya se confirmó o viene de consulta/búsqueda
    ''' </summary>
    Private _identificationConfirmed As Boolean = False
```

---

## 2. Gets null-safe

Reemplazar cada `Get` problemático:

**HandlesBranchOffice**
```vb
        Get
            If INDsleHandlesBranchOffice.EditValue Is Nothing Then Return False
            Return CBool(INDsleHandlesBranchOffice.EditValue)
        End Get
```

**BranchOfficeId**
```vb
        Get
            If INDsleBranchOffice.EditValue Is Nothing Then Return 0
            Return CInt(INDsleBranchOffice.EditValue)
        End Get
```

**FiscalResponsabilityId**
```vb
        Get
            If INDsleFiscalResponsability.EditValue Is Nothing Then Return 0
            Return CInt(INDsleFiscalResponsability.EditValue)
        End Get
```

**PersonType**
```vb
        Get
            If INDslePersonType.EditValue Is Nothing Then Return 0
            Return CInt(INDslePersonType.EditValue)
        End Get
```

**ThirdPartyNit**
```vb
        Get
            If INDBteThirdPartyNit.EditValue Is Nothing Then Return String.Empty
            Return CStr(INDBteThirdPartyNit.EditValue)
        End Get
```

**VerificationCode**
```vb
        Get
            If INDTxtVC.EditValue Is Nothing Then Return String.Empty
            Return CStr(INDTxtVC.EditValue)
        End Get
```

**Ica**
```vb
        Get
            If INDRgIca.EditValue Is Nothing Then Return False
            Return CBool(INDRgIca.EditValue)
        End Get
```

**IcaPercentage**
```vb
        Get
            If INDTxtIcaPercentage.EditValue Is Nothing Then Return 0D
            Return CDec(INDTxtIcaPercentage.EditValue)
        End Get
```

**IcaTop**
```vb
        Get
            If INDRgIcaTop.EditValue Is Nothing Then Return False
            Return CBool(INDRgIcaTop.EditValue)
        End Get
```

**IcaTopValue**
```vb
        Get
            If INDTxtIcaTopValue.EditValue Is Nothing Then Return 0R
            Return CDbl(INDTxtIcaTopValue.EditValue)
        End Get
```

---

## 3. ThirdParty_ActionsOnContros

Después de `INDslePersonType.Enabled = value` agregar:

```vb
            INDsleClass.Enabled = value
```

---

## 4. Reemplazar `CleanControls` completo

```vb
    Public Sub CleanControls()
        FlagLoadControls = False
        INDLyThirdParty.BeginUpdate()

        thirdparty = Nothing
        Person = Nothing
        _pendingIdentification = Nothing
        _identificationConfirmed = False

        INDLcgDocuments.HideControl()
        INDlyItemStateEnterpriseType.HideLayout()
        INDLCCodDivipola.HideLayout()
        INDlygBranchOffice.HideControl()
        INDlygThirdPartyTaxExemptions.HideControl()
        INDLyGrPersonIdentification.HideControl()
        INDLyGrParameter.HideControl()
        INDlygFiscalResponsability.HideControl()
        INDLyItemName.HideLayout()
        INDLyItemIdentificationTypeJuridic.HideLayout()
        INDLycgOtherParams.HideControl()
        INDLciCIIU.HideLayout()
        INDlygEconomicActivity.HideControl()

        HandlesBranchOffice = False
        BranchOfficeId = Nothing
        INDgcInfo.DataSource = Nothing
        ListThirdPartyBranchOffice = Nothing
        ListDeleteThirdPartyBranchOffice = Nothing

        FiscalResponsabilityId = Nothing
        INDgcFiscalResponsabilities.DataSource = Nothing
        ListThirdPartyFiscalResponsability = Nothing
        ListDeleteThirdPartyFiscalResponsability = Nothing

        IVARetentionConceptId = Nothing
        INDsleIVARetentionConcept.Properties.NullText = String.Empty

        IdentificationTypeXpo = Nothing
        ThirdParty_ActionsOnContros = False
        PersonType = Nothing
        INDTxtCIIU.Text = String.Empty
        INDCodDivipola.Text = String.Empty
        INDBteThirdPartyNit.Text = String.Empty
        VerificationCode = String.Empty
        ThridPartyName = String.Empty
        CityId = Nothing
        INDsleCity.Properties.NullText = String.Empty
        IdentificationType = Nothing
        Me.IdentificationTypeId = Nothing
        INDsleIdentificationType.EditValue = Nothing
        INDsleIdentificationTypeJuridic.EditValue = Nothing
        Me.INDsleIdentificationType.Properties.NullText = Nothing
        Me.INDsleIdentificationTypeJuridic.Properties.NullText = Nothing
        Me.IdentificationTypeDatasource = Nothing
        Me.IdentificationTypeJuridicDatasource = Nothing
        FirstName = String.Empty
        SecondName = String.Empty
        FirstLastName = String.Empty
        SecondLastName = String.Empty
        RetentionType = -1
        ContributionType = -1
        Ica = False
        IcaPercentage = Nothing
        IcaTop = False
        IcaTopValue = Nothing
        CtrContacts.LimpiarControles()
        ClassThirdParty = Nothing
        ApplyNitRulesByClass(Nothing)

        EconomicActivityId = Nothing
        INDgcEconomicActivities.DataSource = Nothing
        ListThirdPartyEconomicActivities = Nothing
        ListDeleteThirdPartyEconomicActivities = Nothing
        ListDeleteThirdPartyTaxExemptions = Nothing

        ElectronicBiller = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.INDBteThirdPartyNit.Properties.MaxLength = 25
        Me._doc = Nothing
        Me._validIdentificationType = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDLyThirdParty.EndUpdate()

        BarraBotones.CleanAuditBasic()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        DeleteBlockedRecord()
        FlagLoadControls = True
    End Sub
```

> Quitar el `PrepareToolbar(OnlyFind)` que tenías antes del `NewAndFind`.

---

## 5. ValidateControlsForm

**5a.** Después de `If PersonType = Nothing` agregar (y puedes dejar o no la validación de IdentificationTypeId que ya tenías):

```vb
        If ({1, 2}.Contains(PersonType) AndAlso ClassThirdParty Is Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la clase del tercero"
            Return False
        End If
```

**5b.** Reemplazar el bloque de actividad económica por:

```vb
        Dim economicActivityVisible = (INDlygEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        Dim requiresEconomicActivity As Boolean = economicActivityVisible AndAlso (PersonType = 2 OrElse (PersonType = 1 AndAlso ElectronicBiller))
        If requiresEconomicActivity AndAlso (ListThirdPartyEconomicActivities Is Nothing OrElse Not ListThirdPartyEconomicActivities.Any()) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos una actividad economica"
            Return False
        End If
```

**5c.** Cambiar:
```vb
            If ClassThirdParty Is Nothing And PersonType = 2 Then
```
por:
```vb
            If ClassThirdParty Is Nothing AndAlso (PersonType = 1 OrElse PersonType = 2) Then
```

---

## 6. AssigningValues — Natural también guarda Clase

Cambiar:
```vb
                .Class = Nothing
```
por:
```vb
                .Class = ClassThirdParty
```
(dentro del bloque `If PersonType = 1`)

---

## 7. ThirdParty_LoadControls

Tras `AsyncLoader(True)`:
```vb
            _identificationConfirmed = True
            _pendingIdentification = Nothing
```

Tras `ClassThirdParty = .Class`:
```vb
                            ApplyNitRulesByClass(ClassThirdParty)
```

---

## 8. MyBase_IdEntityLoaded

Antes de `Await Me.ThirdParty_LoadControls()`:
```vb
        _identificationConfirmed = True
        _pendingIdentification = Nothing
```

---

## 9. ReturnValue (búsqueda)

Tras `INDBteThirdPartyNit.Text = ReturnValue`:
```vb
        _identificationConfirmed = True
        _pendingIdentification = Nothing
```

---

## 10. Métodos nuevos (antes del `#End Region` de ICRUD, después de `Nuevo`)

```vb
    ''' <summary>
    ''' PBI 29866: NIT según Clase (1=Nacional, 2=Extranjero)
    ''' </summary>
    Private Sub ApplyNitRulesByClass(classValue As Integer?)
        If classValue.HasValue AndAlso classValue.Value = 1 Then
            IndigoTextEdit1.SetMascara(INDBteThirdPartyNit, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
            INDBteThirdPartyNit.Properties.MaxLength = 25
            INDLyVC.ShowLayout()
        ElseIf classValue.HasValue AndAlso classValue.Value = 2 Then
            IndigoTextEdit1.SetMascara(INDBteThirdPartyNit, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
            INDBteThirdPartyNit.Properties.MaxLength = 25
            VerificationCode = String.Empty
            INDLyVC.HideLayout()
            INDLyItemNit.MinSize = New Drawing.Size(410, INDLyItemNit.MinSize.Height)
            INDLyItemNit.MaxSize = New Drawing.Size(410, INDLyItemNit.MaxSize.Height)
            INDLyItemNit.Size = New Drawing.Size(411, INDLyItemNit.Size.Height)
            INDBteThirdPartyNit.Width = 259
        Else
            IndigoTextEdit1.SetMascara(INDBteThirdPartyNit, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
            INDBteThirdPartyNit.Properties.MaxLength = 25
        End If
    End Sub

    ''' <summary>
    ''' PBI 29866: doble confirmación solo en registro nuevo
    ''' </summary>
    Private Function ConfirmIdentification(currentNit As String) As Boolean
        If _identificationConfirmed Then
            Return True
        End If

        If String.IsNullOrEmpty(_pendingIdentification) Then
            _pendingIdentification = currentNit
            Mensaje(EeventViewerImages.Informacion) = "Confirme la identificación ingresándola nuevamente"
            INDBteThirdPartyNit.Text = String.Empty
            INDBteThirdPartyNit.Focus()
            Return False
        End If

        If Not String.Equals(_pendingIdentification, currentNit, StringComparison.OrdinalIgnoreCase) Then
            Mensaje(EeventViewerImages.Advertencia) = "Las identificaciones no coincidieron"
            _pendingIdentification = Nothing
            INDBteThirdPartyNit.Text = String.Empty
            INDBteThirdPartyNit.Focus()
            Return False
        End If

        _identificationConfirmed = True
        _pendingIdentification = Nothing
        Return True
    End Function
```

> Si `EMask.Numerico` da BC30456, mira en IntelliSense el miembro de solo dígitos (nunca uses `.Numeros`).

---

## 11. Reemplazar `INDslePersonTypeEditValueChangedManual` completo

```vb
    Private Async Function INDslePersonTypeEditValueChangedManual() As Task
        If PersonType <> Nothing Then
            FlagLoadControls = False

            ' Clase siempre en Segmento 1 (datos básicos)
            If INDlyItemClass.Parent IsNot Nothing AndAlso INDlyItemClass.Parent IsNot INDLyGrThirdPartyBasicData Then
                INDlyItemClass.Parent.Remove(INDlyItemClass)
                INDLyGrThirdPartyBasicData.Add(INDlyItemClass)
            End If
            INDlyItemClass.ShowLayout()

            ' Ocultar CIIU y Actividad económica
            INDLciCIIU.HideLayout()
            INDlygEconomicActivity.HideControl()

            If PersonType = 1 Then ' Natural
                INDLyItemIdentificationTypeJuridic.HideLayout()
                INDLyItemName.HideLayout()

                If INDLyItemIdentificationType.Parent IsNot Nothing AndAlso INDLyItemIdentificationType.Parent IsNot INDLyGrThirdPartyBasicData Then
                    INDLyItemIdentificationType.Parent.Remove(INDLyItemIdentificationType)
                    INDLyGrThirdPartyBasicData.Add(INDLyItemIdentificationType)
                End If
                INDLyItemIdentificationType.ShowLayout()

                If INDLyItemCityDocument.Parent IsNot Nothing AndAlso INDLyItemCityDocument.Parent IsNot INDLyGrPersonIdentification Then
                    INDLyItemCityDocument.Parent.Remove(INDLyItemCityDocument)
                    INDLyGrPersonIdentification.Add(INDLyItemCityDocument)
                End If

                If INDlyItemHandlesBranchOffice.Parent IsNot Nothing AndAlso INDlyItemHandlesBranchOffice.Parent IsNot INDLyGrPersonIdentification Then
                    INDlyItemHandlesBranchOffice.Parent.Remove(INDlyItemHandlesBranchOffice)
                    INDLyGrPersonIdentification.Add(INDlyItemHandlesBranchOffice)
                End If
                If LayoutControlItem2.Parent IsNot Nothing AndAlso LayoutControlItem2.Parent IsNot INDLyGrPersonIdentification Then
                    LayoutControlItem2.Parent.Remove(LayoutControlItem2)
                    INDLyGrPersonIdentification.Add(LayoutControlItem2)
                End If

                INDLyGrPersonIdentification.HideControl(False)
                INDLyGrParameter.HideControl(False)
                INDlygFiscalResponsability.HideControl(False)
                INDLycgOtherParams.HideControl(False)

                INDlciIVARetentionAccountPayableConceptId.HideLayout()
                INDlyItemEntityCode.HideLayout()
                INDLyItemContributionType.ShowLayout()
                INDLyItemIca.ShowLayout()
                INDLyItemRetentionType.ShowLayout()
                INDLyItemIcaPercentage.ShowLayout()
                INDLyItemIcaTop.ShowLayout()
                INDLyItemIcaTopValue.ShowLayout()

            Else ' Jurídico
                INDLyItemIdentificationTypeJuridic.ShowLayout()
                INDLyItemName.ShowLayout()

                If INDLyItemCityDocument.Parent IsNot Nothing AndAlso INDLyItemCityDocument.Parent IsNot INDLyGrThirdPartyBasicData Then
                    INDLyItemCityDocument.Parent.Remove(INDLyItemCityDocument)
                    INDLyGrThirdPartyBasicData.Add(INDLyItemCityDocument)
                End If
                INDLyItemCityDocument.ShowLayout()

                If LayoutControlItem2.Parent IsNot Nothing AndAlso LayoutControlItem2.Parent IsNot INDLyGrThirdPartyBasicData Then
                    LayoutControlItem2.Parent.Remove(LayoutControlItem2)
                    INDLyGrThirdPartyBasicData.Add(LayoutControlItem2)
                End If

                If INDlyItemHandlesBranchOffice.Parent IsNot Nothing AndAlso INDlyItemHandlesBranchOffice.Parent IsNot INDLyGrThirdPartyBasicData Then
                    INDlyItemHandlesBranchOffice.Parent.Remove(INDlyItemHandlesBranchOffice)
                    INDLyGrThirdPartyBasicData.Add(INDlyItemHandlesBranchOffice)
                End If

                If INDLyItemIdentificationType.Parent IsNot Nothing AndAlso INDLyItemIdentificationType.Parent IsNot INDLyGrPersonIdentification Then
                    INDLyItemIdentificationType.Parent.Remove(INDLyItemIdentificationType)
                    INDLyGrPersonIdentification.Add(INDLyItemIdentificationType)
                End If

                INDLyGrPersonIdentification.HideControl()
                INDLyGrParameter.HideControl(False)
                INDlygFiscalResponsability.HideControl(False)
                INDLycgOtherParams.HideControl(False)

                IdentificationType = -1
                Dim Obj = Await Me.LegalIdentificationType
                IdentificationTypeId = Obj.ID
                INDsleIdentificationTypeJuridic.Properties.NullText = Obj.CodeName

                INDLyItemRetentionType.ShowLayout()
                INDLyItemContributionType.ShowLayout()
                INDlciIVARetentionAccountPayableConceptId.ShowLayout()
                INDlyItemEntityCode.ShowLayout()
                INDLyItemIca.ShowLayout()
                INDLyItemIcaPercentage.ShowLayout()
                INDLyItemIcaTop.ShowLayout()
                INDLyItemIcaTopValue.ShowLayout()
            End If

            ApplyNitRulesByClass(ClassThirdParty)
            Await Person_LoadControls()
            Await Me.NitLengthValidator(Me.IdentificationTypeId, ThirdPartyNit, PersonType, True)
            FlagLoadControls = True
        Else
            INDLyGrPersonIdentification.HideControl()
            INDLyGrParameter.HideControl()
            INDlygFiscalResponsability.HideControl()
            INDLyItemName.HideLayout()
            INDLyItemIdentificationTypeJuridic.HideLayout()
            INDLycgOtherParams.HideControl()
            ApplyNitRulesByClass(Nothing)
        End If
    End Function
```

---

## 12. Evento Clase (región EditValueChanged)

```vb
    Private Sub INDsleClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleClass.EditValueChanged
        If Not FlagLoadControls Then Exit Sub
        ApplyNitRulesByClass(ClassThirdParty)
        If IdentificationTypeId IsNot Nothing AndAlso Not String.IsNullOrEmpty(ThirdPartyNit) Then
            presenter.Calculate_VerificationCode(ThirdPartyNit, PersonType, IdentificationType, FlagLoadControls)
        End If
    End Sub
```

---

## 13. Reemplazar INDBteThirdPartyNit_KeyDown

```vb
    Private Async Sub INDBteThirdPartyNit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteThirdPartyNit.KeyDown
        If Not String.IsNullOrEmpty(INDBteThirdPartyNit.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If Not BarraBotones.PermiteConsultar Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                    Exit Sub
                End If

                Dim isExistingRecord = (thirdparty IsNot Nothing AndAlso thirdparty.Id > 0)
                If Not isExistingRecord Then
                    If Not ConfirmIdentification(INDBteThirdPartyNit.Text.Trim()) Then
                        Exit Sub
                    End If
                Else
                    _identificationConfirmed = True
                End If

                If Not INDBteThirdPartyNit.Enabled Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If

                Await ThirdParty_LoadControls()
                INDBteThirdPartyNit.Enabled = False
            End If
        End If
        If e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub
```

---

## 14. ClickDeshacer (CRÍTICO)

Quitar la segunda línea:

```vb
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
```

---

## 15. Frm_Disposed

Al final, antes del `End Sub`:

```vb
        _pendingIdentification = Nothing
        _identificationConfirmed = False
```

---

## Archivos adicionales (obligatorios)

1. **PThirdParty.vb** → reemplaza el tuyo con `/PBI29866_ERP_Presentation_V2/PThirdParty.vb` (DV solo Nacional).
2. **FrmThirdParty.Designer.vb** → sigue `FrmThirdParty_Designer_APLICAR.md`:
   - `INDlyItemClass` dentro de `INDLyGrThirdPartyBasicData` (no en Parámetros)
   - `Location = (0, 108)`, `TextSize = (135, 21)`
   - CIIU y grupo actividad económica en `Visibility = Never`

---

Cuando termines `FrmThirdParty.vb`, pégame el archivo (o los errores de compilación) y revisamos. Luego Designer si aún no lo aplicaste.
