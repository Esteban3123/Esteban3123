# Revisión PBI 29866 — código aplicado

## Veredicto

| Archivo | Estado |
|---------|--------|
| `FrmThirdParty.vb` | ✅ Correcto (solo 1 ajuste menor opcional) |
| `PThirdParty.vb` | ✅ Correcto |
| `FrmThirdParty.Designer.vb` | ⚠️ Falta 1 corrección de layout (Clase solapa NIT) |

---

## Lo que quedó bien (`FrmThirdParty.vb`)

- Variables `_pendingIdentification` / `_identificationConfirmed`
- Gets null-safe (Boolean/Integer) → evita crash en Nuevo/Deshacer
- `INDsleClass.Enabled` en `ThirdParty_ActionsOnContros`
- `CleanControls` sin `OnlyFind` intermedio; toolbar `NewAndFind`
- Validación Clase obligatoria Natural y Jurídico
- Actividad económica solo si el grupo está visible
- `.Class = ClassThirdParty` en Natural
- Sin reconfirmación al cargar / buscar / `IdEntityLoaded`
- Doble confirmación en Enter solo si es registro nuevo
- `ApplyNitRulesByClass` + `ConfirmIdentification`
- Layout runtime Natural/Jurídico (Clase en Segmento 1, CIIU/económica ocultas)
- `INDsleClass_EditValueChanged`
- `ClickDeshacer` → solo `CleanControls()`

## Lo que quedó bien (`PThirdParty.vb`)

- Clase 2 (Extranjero): limpia DV y sale
- Clase 1 (Nacional): exige NIT numérico
- Resto del cálculo DV igual

## Lo que quedó bien (Designer)

- Clase en `INDLyGrThirdPartyBasicData.Items`
- Clase **fuera** de `INDLyGrParameter.Items`
- `INDLciCIIU.Visibility = Never`
- `INDlygEconomicActivity.Visibility = Never`

---

## Corrección obligatoria (Designer)

`INDlyItemClass` está en el grupo de datos básicos, pero su **Location sigue en (0,0)** (igual que el NIT). Eso solapa controles al abrir el form.

**Buscar el bloque `'INDlyItemClass`:**
```vb
        Me.INDlyItemClass.Control = Me.INDsleClass
        resources.ApplyResources(Me.INDlyItemClass, "INDlyItemClass")
        Me.INDlyItemClass.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemClass.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemClass.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemClass.Name = "INDlyItemClass"
        Me.INDlyItemClass.Size = New System.Drawing.Size(450, 36)
        Me.INDlyItemClass.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemClass.Spacing = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2)
        Me.INDlyItemClass.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemClass.TextSize = New System.Drawing.Size(205, 21)
        Me.INDlyItemClass.TextToControlDistance = 12
```

**Reemplazar por** (usa el hueco del CIIU, que está oculto; TextAlignMode alineado al Segmento 1):
```vb
        Me.INDlyItemClass.Control = Me.INDsleClass
        resources.ApplyResources(Me.INDlyItemClass, "INDlyItemClass")
        Me.INDlyItemClass.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemClass.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemClass.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemClass.Name = "INDlyItemClass"
        Me.INDlyItemClass.Size = New System.Drawing.Size(450, 36)
        Me.INDlyItemClass.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemClass.Spacing = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2)
        Me.INDlyItemClass.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemClass.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemClass.TextToControlDistance = 12
```

Y en el bloque `INDLciCIIU`, cambia Location para que no peleen (aunque CIIU esté Never):
```vb
        Me.INDLciCIIU.Location = New System.Drawing.Point(0, 432)
```
(o déjalo; al estar `Never` no se ve).

---

## Opcional (recomendado)

### A) Clase visualmente obligatoria (Designer)

```vb
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleClass, True)
```
y fondo MistyRose en `INDsleClass.Properties.Appearance.BackColor`.

### B) `Frm_Disposed` — tipo Boolean

Cambiar:
```vb
        _identificationConfirmed = Nothing
```
por:
```vb
        _identificationConfirmed = False
```

---

## Checklist de prueba manual

1. Nuevo NIT → pide confirmación → si no coincide: “Las identificaciones no coincidieron”
2. Buscar / F4 / cargar existente → **no** pide confirmación
3. Clase Nacional → NIT numérico + DV visible/calcula
4. Clase Extranjero → NIT alfanumérico + DV oculto / vacío
5. Guardar sin Clase → “Debe seleccionar la clase del tercero”
6. CIIU y Actividad económica no visibles
7. Tras consultar un tercero → Nuevo / Deshacer → barra en NewAndFind (no se rompe)
