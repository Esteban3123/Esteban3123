# PBI 29866 — Parches sobre código en cero (ERP_Presentation_V2)

Aplica estos cambios **en orden** sobre tus archivos originales.
Archivos: `FrmThirdParty.vb`, `FrmThirdParty.Designer.vb`, `PThirdParty.vb`.

`PThirdParty.vb` completo ya está en esta carpeta: cópialo entero sobre el original.

---

## A) FrmThirdParty.vb

### A1. Variables (después de `_indexEditRecord`)

Agregar:

```vb
    ''' <summary>
    ''' PBI 29866: NIT pendiente de confirmación (doble ingreso en nuevo)
    ''' </summary>
    Private _pendingIdentification As String = Nothing

    ''' <summary>
    ''' PBI 29866: True si la identificación ya fue confirmada o viene de consulta
    ''' </summary>
    Private _identificationConfirmed As Boolean = False
```

### A2. Gets null-safe (reemplazar cada propiedad)

**HandlesBranchOffice:**
```vb
    Public Property HandlesBranchOffice As Boolean Implements IThirdParty.HandlesBranchOffice
        Get
            If INDsleHandlesBranchOffice.EditValue Is Nothing Then
                Return False
            End If
            Return CBool(INDsleHandlesBranchOffice.EditValue)
        End Get
        Set(value As Boolean)
            INDsleHandlesBranchOffice.EditValue = value
        End Set
    End Property
```

**BranchOfficeId:**
```vb
    Public Property BranchOfficeId As Integer Implements IThirdParty.BranchOfficeId
        Get
            If INDsleBranchOffice.EditValue Is Nothing Then
                Return 0
            End If
            Return CInt(INDsleBranchOffice.EditValue)
        End Get
        Set(value As Integer)
            INDsleBranchOffice.EditValue = value
        End Set
    End Property
```

**FiscalResponsabilityId:**
```vb
    Public Property FiscalResponsabilityId As Integer Implements IThirdParty.FiscalResponsabilityId
        Get
            If INDsleFiscalResponsability.EditValue Is Nothing Then
                Return 0
            End If
            Return CInt(INDsleFiscalResponsability.EditValue)
        End Get
        Set(value As Integer)
            INDsleFiscalResponsability.EditValue = value
        End Set
    End Property
```

**PersonType:**
```vb
    Public Property PersonType As Integer Implements IThirdParty.PersonType
        Get
            If INDslePersonType.EditValue Is Nothing Then
                Return 0
            End If
            Return CInt(INDslePersonType.EditValue)
        End Get
        Set(value As Integer)
            INDslePersonType.EditValue = value
        End Set
    End Property
```

**ThirdPartyNit:**
```vb
    Public Property ThirdPartyNit As String Implements IThirdParty.ThirdPartyNit
        Get
            If INDBteThirdPartyNit.EditValue Is Nothing Then
                Return String.Empty
            End If
            Return CStr(INDBteThirdPartyNit.EditValue)
        End Get
        Set(value As String)
            INDBteThirdPartyNit.EditValue = value
        End Set
    End Property
```

**VerificationCode:**
```vb
    Public Property VerificationCode As String Implements IThirdParty.VerificationCode
        Get
            If INDTxtVC.EditValue Is Nothing Then
                Return String.Empty
            End If
            Return CStr(INDTxtVC.EditValue)
        End Get
        Set(value As String)
            INDTxtVC.EditValue = value
        End Set
    End Property
```

**RetentionType:**
```vb
    Public Property RetentionType As Integer Implements IThirdParty.RetentionType
        Get
            Return INDCbeRetentionType.SelectedIndex
        End Get
        Set(value As Integer)
            INDCbeRetentionType.SelectedIndex = value
        End Set
    End Property
```
(sin cambio obligatorio; deja el original si ya está así)

**Ica / IcaTop / IcaPercentage / IcaTopValue:**
```vb
    Public Property Ica As Boolean Implements IThirdParty.Ica
        Get
            If INDRgIca.EditValue Is Nothing Then
                Return False
            End If
            Return CBool(INDRgIca.EditValue)
        End Get
        Set(value As Boolean)
            INDRgIca.EditValue = value
        End Set
    End Property

    Public Property IcaPercentage As Decimal Implements IThirdParty.IcaPercentage
        Get
            If INDTxtIcaPercentage.EditValue Is Nothing Then
                Return 0D
            End If
            Return CDec(INDTxtIcaPercentage.EditValue)
        End Get
        Set(value As Decimal)
            INDTxtIcaPercentage.EditValue = value
        End Set
    End Property

    Public Property IcaTop As Boolean Implements IThirdParty.IcaTop
        Get
            If INDRgIcaTop.EditValue Is Nothing Then
                Return False
            End If
            Return CBool(INDRgIcaTop.EditValue)
        End Get
        Set(value As Boolean)
            INDRgIcaTop.EditValue = value
        End Set
    End Property

    Public Property IcaTopValue As Double Implements IThirdParty.IcaTopValue
        Get
            If INDTxtIcaTopValue.EditValue Is Nothing Then
                Return 0R
            End If
            Return CDbl(INDTxtIcaTopValue.EditValue)
        End Get
        Set(value As Double)
            INDTxtIcaTopValue.EditValue = value
        End Set
    End Property
```

### A3. Reemplazar `CleanControls` completo

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

### A4. `ValidateControlsForm` — cambios puntuales

1) Después de validar `PersonType = Nothing`, agregar:

```vb
        If ({1, 2}.Contains(PersonType) AndAlso ClassThirdParty Is Nothing) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la clase del tercero"
            Return False
        End If
```

2) Reemplazar el bloque de actividad económica por:

```vb
        Dim economicActivityVisible = (INDlygEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        Dim requiresEconomicActivity As Boolean = economicActivityVisible AndAlso (PersonType = 2 OrElse (PersonType = 1 AndAlso ElectronicBiller))
        If requiresEconomicActivity AndAlso (ListThirdPartyEconomicActivities Is Nothing OrElse Not ListThirdPartyEconomicActivities.Any()) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos una actividad economica"
            Return False
        End If
```

3) En la sección `INDLyGrParameter`, cambiar:

```vb
            If ClassThirdParty Is Nothing And PersonType = 2 Then
                Return False
            End If
```

por (ya validamos clase arriba; puedes dejar solo retención/contribución/ICA o):

```vb
            If ClassThirdParty Is Nothing AndAlso (PersonType = 1 OrElse PersonType = 2) Then
                Return False
            End If
```

### A5. `AssigningValues` — Clase también en Natural

En el bloque `If PersonType = 1 Then` (dentro de `With thirdparty`), cambiar:

```vb
                .Class = Nothing
```

por:

```vb
                .Class = ClassThirdParty
```

(El de Jurídico `.Class = ClassThirdParty` se mantiene.)

### A6. Al inicio de carga exitosa en `ThirdParty_LoadControls`

Justo después de `ThirdParty_ActionsOnContros = True` (inicio del Try), agregar:

```vb
            _identificationConfirmed = True
            _pendingIdentification = Nothing
```

### A7. `MyBase_IdEntityLoaded`

Antes de `Await Me.ThirdParty_LoadControls()`:

```vb
        _identificationConfirmed = True
        _pendingIdentification = Nothing
```

### A8. `ReturnValue` (búsqueda)

Después de `INDBteThirdPartyNit.Text = ReturnValue`:

```vb
        _identificationConfirmed = True
        _pendingIdentification = Nothing
```

### A9. Agregar métodos nuevos (antes de `#End Region` de Methods, o al final de Methods)

```vb
    ''' <summary>
    ''' PBI 29866: reglas de NIT según Clase (1 Nacional / 2 Extranjero)
    ''' </summary>
    Private Sub ApplyNitRulesByClass(classValue As Integer?)
        If classValue.HasValue AndAlso classValue.Value = 1 Then
            ' Nacional: solo numérico, max 25, DV visible/calculable
            IndigoTextEdit1.SetMascara(INDBteThirdPartyNit, Presentation.Controls.IndigoTextEdit.EMask.Numeros)
            INDBteThirdPartyNit.Properties.MaxLength = 25
            INDLyVC.ShowLayout()
        ElseIf classValue.HasValue AndAlso classValue.Value = 2 Then
            ' Extranjero: alfanumérico, max 25, sin DV
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
    ''' PBI 29866: doble confirmación de identificación en registro nuevo
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

### A10. Reemplazar `INDslePersonTypeEditValueChangedManual` completo

```vb
    Private Async Function INDslePersonTypeEditValueChangedManual() As Task
        If PersonType <> Nothing Then
            FlagLoadControls = False

            ' PBI 29866: Clase siempre en Segmento 1 (datos básicos)
            If INDlyItemClass.Parent IsNot INDLyGrThirdPartyBasicData Then
                INDLyGrParameter.Remove(INDlyItemClass)
                INDLyGrThirdPartyBasicData.Add(INDlyItemClass)
            End If
            INDlyItemClass.ShowLayout()

            ' Ocultar CIIU y Actividad económica (PBI 29866 / 29751)
            INDLciCIIU.HideLayout()
            INDlygEconomicActivity.HideControl()

            If PersonType = 1 Then ' Natural
                INDLyItemIdentificationTypeJuridic.HideLayout()
                INDLyItemName.HideLayout()

                ' Tipo ID Natural en Segmento 1
                If INDLyItemIdentificationType.Parent IsNot INDLyGrThirdPartyBasicData Then
                    INDLyGrPersonIdentification.Remove(INDLyItemIdentificationType)
                    INDLyGrThirdPartyBasicData.Add(INDLyItemIdentificationType)
                End If
                INDLyItemIdentificationType.ShowLayout()

                ' Segmento 2 Natural: nombres, sucursales, contactos, firma
                If INDlyItemHandlesBranchOffice.Parent IsNot INDLyGrPersonIdentification Then
                    INDLyGrThirdPartyBasicData.Remove(INDlyItemHandlesBranchOffice)
                    INDLyGrPersonIdentification.Add(INDlyItemHandlesBranchOffice)
                End If
                If LayoutControlItem2.Parent IsNot INDLyGrPersonIdentification Then
                    INDLyGrThirdPartyBasicData.Remove(LayoutControlItem2)
                    INDLyGrPersonIdentification.Add(LayoutControlItem2)
                End If
                If LayoutControlItem1.Parent IsNot INDLyGrPersonIdentification Then
                    ' firma ya suele estar en person group
                End If

                INDLyGrPersonIdentification.HideControl(False)
                INDLyGrParameter.HideControl(False)
                INDlygFiscalResponsability.HideControl(False)
                INDLycgOtherParams.HideControl(False)

                INDlciIVARetentionAccountPayableConceptId.HideLayout()
                INDLyItemContributionType.ShowLayout()
                INDLyItemIca.ShowLayout()
                INDLyItemRetentionType.ShowLayout()
                INDLyItemIcaPercentage.ShowLayout()
                INDLyItemIcaTop.ShowLayout()
                INDLyItemIcaTopValue.ShowLayout()
                INDlyItemEntityCode.HideLayout()

            Else ' Jurídico
                INDLyItemIdentificationTypeJuridic.ShowLayout()
                INDLyItemName.ShowLayout()

                ' Lugar expedición (ciudad) y contactos en Segmento 1 / básicos para jurídico
                If INDLyItemCityDocument.Parent IsNot INDLyGrThirdPartyBasicData Then
                    INDLyGrPersonIdentification.Remove(INDLyItemCityDocument)
                    INDLyGrThirdPartyBasicData.Add(INDLyItemCityDocument)
                End If
                INDLyItemCityDocument.ShowLayout()

                If LayoutControlItem2.Parent IsNot INDLyGrThirdPartyBasicData Then
                    If LayoutControlItem2.Parent IsNot Nothing Then
                        LayoutControlItem2.Parent.Remove(LayoutControlItem2)
                    End If
                    INDLyGrThirdPartyBasicData.Add(LayoutControlItem2)
                End If

                ' Devolver sucursales a básicos si estaban en person
                If INDlyItemHandlesBranchOffice.Parent IsNot INDLyGrThirdPartyBasicData Then
                    If INDlyItemHandlesBranchOffice.Parent IsNot Nothing Then
                        INDlyItemHandlesBranchOffice.Parent.Remove(INDlyItemHandlesBranchOffice)
                    End If
                    INDLyGrThirdPartyBasicData.Add(INDlyItemHandlesBranchOffice)
                End If

                ' Devolver Tipo ID Natural al grupo persona (oculto)
                If INDLyItemIdentificationType.Parent IsNot INDLyGrPersonIdentification Then
                    If INDLyItemIdentificationType.Parent IsNot Nothing Then
                        INDLyItemIdentificationType.Parent.Remove(INDLyItemIdentificationType)
                    End If
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

                ' Clase NO está en Parámetros (ya en Segmento 1)
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

### A11. Evento Clase (agregar en región EditValueChanged)

```vb
    Private Sub INDsleClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleClass.EditValueChanged
        If Not FlagLoadControls Then
            Exit Sub
        End If
        ApplyNitRulesByClass(ClassThirdParty)
        If IdentificationTypeId IsNot Nothing AndAlso Not String.IsNullOrEmpty(ThirdPartyNit) Then
            presenter.Calculate_VerificationCode(ThirdPartyNit, PersonType, IdentificationType, FlagLoadControls)
        End If
    End Sub
```

### A12. Reemplazar `INDBteThirdPartyNit_KeyDown`

```vb
    Private Async Sub INDBteThirdPartyNit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteThirdPartyNit.KeyDown
        If Not String.IsNullOrEmpty(INDBteThirdPartyNit.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If Not BarraBotones.PermiteConsultar Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                    Exit Sub
                End If

                ' PBI 29866: doble confirmación solo en registro nuevo
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

### A13. Barra — Deshacer / Nuevo (CRÍTICO: no romper botón Nuevo)

```vb
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        ' NO llamar PrepareToolbar(OnlyFind) aquí: CleanControls ya deja NewAndFind
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub
```

Elimina la línea `Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)` del ClickDeshacer original.

### A14. `GetThirdPartyCheck`

Al final del `If setting.ThirdPartyCheckDigit Then` / Else, respetar Clase Extranjero:

Después de cargar setting, si ya hay clase extranjero forzar hide — o en el Else dejar como está. Lo importante: `ApplyNitRulesByClass` manda cuando hay clase.

Opcional al final del Using:

```vb
            If ClassThirdParty.HasValue Then
                ApplyNitRulesByClass(ClassThirdParty)
            End If
```

### A15. `Frm_Disposed`

Agregar limpieza:

```vb
        _pendingIdentification = Nothing
        _identificationConfirmed = Nothing
```

---

## B) FrmThirdParty.Designer.vb

### B1. Mover Clase a datos básicos

En `INDLyGrThirdPartyBasicData.Items.AddRange(...)`, **agregar** `Me.INDlyItemClass` y quitarlo del `INDLyGrParameter.Items.AddRange`.

Ejemplo datos básicos (incluye Clase; CIIU puede quedar pero Visibility Never):

```vb
Me.INDLyGrThirdPartyBasicData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {
    Me.INDLyItemNit, Me.INDLyItemName, Me.INDLyItemPersonType, Me.INDLyVC,
    Me.INDlyItemClass, Me.LayoutControlItem2, Me.INDlyItemHandlesBranchOffice,
    Me.INDLyItemIdentificationTypeJuridic, Me.INDLciCIIU})
```

Parámetros **sin** `INDlyItemClass`:

```vb
Me.INDLyGrParameter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {
    Me.INDLyItemRetentionType, Me.INDLyItemContributionType, Me.INDLyItemIca,
    Me.INDLyItemIcaPercentage, Me.INDLyItemIcaTop, Me.INDLyItemIcaTopValue,
    Me.INDlyItemEntityCode, Me.INDlciIVARetentionAccountPayableConceptId,
    Me.INDlyItemStateEnterpriseType, Me.INDLCCodDivipola, Me.INDlyItemIVARetentionConcept})
```

### B2. Ocultar CIIU y Actividad económica por defecto

```vb
Me.INDLciCIIU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
Me.INDlygEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
```

### B3. Contactos / Sucursales

Pueden quedar en BasicData en Designer; el runtime de `INDslePersonTypeEditValueChangedManual` los reubica según Natural/Jurídico.

---

## C) PThirdParty.vb

Reemplaza el archivo completo por `PThirdParty.vb` de esta carpeta.

---

## Checklist de prueba

1. Nuevo → ingresar NIT → Enter → pedir confirmación → segundo Enter distinto → "Las identificaciones no coincidieron".
2. Nuevo → confirmar NIT igual dos veces → carga / guarda OK.
3. Buscar tercero existente → **no** pide confirmación.
4. Clase Nacional → NIT solo números + DV.
5. Clase Extranjero → alfanumérico, sin DV.
6. Clase obligatoria Natural y Jurídico al guardar.
7. Layout: Clase en Segmento 1; CIIU y Actividad Económica ocultos.
8. Con tercero cargado → Deshacer / Nuevo → toolbar con Nuevo + Buscar (no se queda en OnlyFind).
