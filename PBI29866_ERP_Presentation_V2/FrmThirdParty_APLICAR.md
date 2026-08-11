# FrmThirdParty.vb — PBI 29866 (aplicar sobre tu base en cero)

Aplica **en orden**. Cada bloque es: buscar el texto exacto → reemplazar por el nuevo.

---

## 1) Variables (después de `_indexEditRecord`)

**Buscar:**
```vb
    Private _indexEditRecord As Integer

#End Region
```

**Reemplazar por:**
```vb
    Private _indexEditRecord As Integer

    ''' <summary>
    ''' PBI 29866: identificación pendiente de confirmación (doble ingreso)
    ''' </summary>
    Private _pendingIdentification As String = Nothing

    ''' <summary>
    ''' PBI 29866: True si ya se confirmó o viene de consulta/búsqueda
    ''' </summary>
    Private _identificationConfirmed As Boolean = False

#End Region
```

---

## 2) Gets null-safe (evita crash al limpiar / Nuevo)

### HandlesBranchOffice
**Buscar** el Get actual y dejarlo así:
```vb
        Get
            If INDsleHandlesBranchOffice.EditValue Is Nothing Then Return False
            Return CBool(INDsleHandlesBranchOffice.EditValue)
        End Get
```

### BranchOfficeId
```vb
        Get
            If INDsleBranchOffice.EditValue Is Nothing Then Return 0
            Return CInt(INDsleBranchOffice.EditValue)
        End Get
```

### FiscalResponsabilityId
```vb
        Get
            If INDsleFiscalResponsability.EditValue Is Nothing Then Return 0
            Return CInt(INDsleFiscalResponsability.EditValue)
        End Get
```

### PersonType
```vb
        Get
            If INDslePersonType.EditValue Is Nothing Then Return 0
            Return CInt(INDslePersonType.EditValue)
        End Get
```

### ThirdPartyNit
```vb
        Get
            If INDBteThirdPartyNit.EditValue Is Nothing Then Return String.Empty
            Return CStr(INDBteThirdPartyNit.EditValue)
        End Get
```

### VerificationCode
```vb
        Get
            If INDTxtVC.EditValue Is Nothing Then Return String.Empty
            Return CStr(INDTxtVC.EditValue)
        End Get
```

### Ica
```vb
        Get
            If INDRgIca.EditValue Is Nothing Then Return False
            Return CBool(INDRgIca.EditValue)
        End Get
```

### IcaPercentage
```vb
        Get
            If INDTxtIcaPercentage.EditValue Is Nothing Then Return 0D
            Return CDec(INDTxtIcaPercentage.EditValue)
        End Get
```

### IcaTop
```vb
        Get
            If INDRgIcaTop.EditValue Is Nothing Then Return False
            Return CBool(INDRgIcaTop.EditValue)
        End Get
```

### IcaTopValue
```vb
        Get
            If INDTxtIcaTopValue.EditValue Is Nothing Then Return 0R
            Return CDbl(INDTxtIcaTopValue.EditValue)
        End Get
```

---

## 3) ThirdParty_ActionsOnContros — habilitar Clase

Dentro del `Set`, después de `INDslePersonType.Enabled = value`, agregar:
```vb
            INDsleClass.Enabled = value
```

---

## 4) Reemplazar `CleanControls` completo

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

Importante: se quitó el `PrepareToolbar(OnlyFind)` que estaba **antes** del `NewAndFind` (dejaba mal la barra).

---

## 5) `ValidateControlsForm`

### 5a) Después de validar PersonType vacío, agregar:
```vb
        If ({1, 2}.Contains(PersonType) AndAlso ClassThirdParty Is Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la clase del tercero"
            Return False
        End If
```

### 5b) Reemplazar validación de actividad económica:
```vb
        Dim economicActivityVisible = (INDlygEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        Dim requiresEconomicActivity As Boolean = economicActivityVisible AndAlso (PersonType = 2 OrElse (PersonType = 1 AndAlso ElectronicBiller))
        If requiresEconomicActivity AndAlso (ListThirdPartyEconomicActivities Is Nothing OrElse Not ListThirdPartyEconomicActivities.Any()) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos una actividad economica"
            Return False
        End If
```

### 5c) En parámetros, cambiar:
```vb
            If ClassThirdParty Is Nothing And PersonType = 2 Then
```
por:
```vb
            If ClassThirdParty Is Nothing AndAlso (PersonType = 1 OrElse PersonType = 2) Then
```

---

## 6) `AssigningValues` — Clase también en Natural

**Buscar:**
```vb
            If PersonType = 1 Then 'Si es natural
                .Class = Nothing
```

**Reemplazar por:**
```vb
            If PersonType = 1 Then 'Si es natural
                .Class = ClassThirdParty
```

---

## 7) `ThirdParty_LoadControls` — no pedir confirmación al cargar

Al inicio del `Try`, después de `AsyncLoader(True)`:
```vb
            _identificationConfirmed = True
            _pendingIdentification = Nothing
```

Después de `ClassThirdParty = .Class` (cuando carga existente), agregar:
```vb
                            ApplyNitRulesByClass(ClassThirdParty)
```

---

## 8) `MyBase_IdEntityLoaded`

Antes de `Await Me.ThirdParty_LoadControls()`:
```vb
        _identificationConfirmed = True
        _pendingIdentification = Nothing
```

---

## 9) `ReturnValue` (búsqueda)

Después de `INDBteThirdPartyNit.Text = ReturnValue`:
```vb
        _identificationConfirmed = True
        _pendingIdentification = Nothing
```

---

## 10) Métodos nuevos (antes de `#End Region` de Methods)

```vb
    ''' <summary>
    ''' PBI 29866: NIT según Clase (1=Nacional, 2=Extranjero)
    ''' </summary>
    Private Sub ApplyNitRulesByClass(classValue As Integer?)
        If classValue.HasValue AndAlso classValue.Value = 1 Then
            IndigoTextEdit1.SetMascara(INDBteThirdPartyNit, Presentation.Controls.IndigoTextEdit.EMask.Numeros)
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

---

## 11) Reemplazar `INDslePersonTypeEditValueChangedManual` completo

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

            ' Ocultar CIIU y Actividad económica (PBI 29866 / 29751)
            INDLciCIIU.HideLayout()
            INDlygEconomicActivity.HideControl()

            If PersonType = 1 Then ' Natural
                INDLyItemIdentificationTypeJuridic.HideLayout()
                INDLyItemName.HideLayout()

                ' Tipo ID Natural → Segmento 1
                If INDLyItemIdentificationType.Parent IsNot Nothing AndAlso INDLyItemIdentificationType.Parent IsNot INDLyGrThirdPartyBasicData Then
                    INDLyItemIdentificationType.Parent.Remove(INDLyItemIdentificationType)
                    INDLyGrThirdPartyBasicData.Add(INDLyItemIdentificationType)
                End If
                INDLyItemIdentificationType.ShowLayout()

                ' Devolver ciudad al grupo persona si se movió en jurídico
                If INDLyItemCityDocument.Parent IsNot Nothing AndAlso INDLyItemCityDocument.Parent IsNot INDLyGrPersonIdentification Then
                    INDLyItemCityDocument.Parent.Remove(INDLyItemCityDocument)
                    INDLyGrPersonIdentification.Add(INDLyItemCityDocument)
                End If

                ' Sucursales + Contactos → Segmento 2 (persona)
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

                ' Ciudad (lugar expedición) + Contactos en Segmento 1
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

                ' Tipo ID Natural de vuelta al grupo persona (oculto con el grupo)
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

                ' Clase ya está en Segmento 1 (NO en Parámetros)
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

## 12) Evento Clase (en región EditValueChanged)

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

## 13) Reemplazar `INDBteThirdPartyNit_KeyDown`

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

## 14) Barra Deshacer (CRÍTICO)

**Buscar:**
```vb
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub
```

**Reemplazar por:**
```vb
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
```

---

## 15) `Frm_Disposed`

Agregar:
```vb
        _pendingIdentification = Nothing
        _identificationConfirmed = Nothing
```

---

## 16) `GetThirdPartyCheck` (opcional al final del Using)

```vb
            If ClassThirdParty.HasValue Then
                ApplyNitRulesByClass(ClassThirdParty)
            End If
```

---

Cuando termines estos 16 puntos en `FrmThirdParty.vb`, pégame el **Designer** y después el **PThirdParty**.
