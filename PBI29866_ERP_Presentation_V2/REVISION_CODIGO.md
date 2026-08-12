# Revisión PBI 29866 — estado (última revisión)

## FrmThirdParty.vb — APROBADO con 1 corrección

| Requisito | Estado |
|-----------|--------|
| Flags `_pendingIdentification` / `_identificationConfirmed` | OK |
| Gets null-safe | OK |
| `INDsleClass.Enabled` | OK |
| `CleanControls` / validación Clase / ConfirmIdentification | OK |
| `ApplyNitRulesByClass` con `EMask.Numerico` | OK |
| Layout runtime + `ClickDeshacer` | OK |

### Corrección en `Frm_Disposed`
```vb
_identificationConfirmed = False   ' no Nothing
```

---

## FrmThirdParty.Designer.vb — aplicar 4 bloques
Ver `FrmThirdParty_Designer_APLICAR.md` / código enviado al usuario.

---

## PThirdParty.vb — PENDIENTE (baseline sin PBI)

El archivo pegado **no** tiene el cambio de DV por Clase.
Reemplazar solo `Calculate_VerificationCode` por la versión de `PThirdParty.vb` de esta carpeta.

```vb
Public Sub Calculate_VerificationCode(ByVal nit As String, personType As Integer, identificationType As Integer, Optional FlagLoadControls As Boolean = True)
    If Not FlagLoadControls Then
        Exit Sub
    End If

    ' Extranjero: no calcular DV
    If View.ClassThirdParty.HasValue AndAlso View.ClassThirdParty.Value = 2 Then
        View.VerificationCode = String.Empty
        Exit Sub
    End If

    ' Solo Nacional: NIT debe ser numérico
    If View.ClassThirdParty.HasValue AndAlso View.ClassThirdParty.Value = 1 Then
        If Not IsNumeric(nit) Then
            View.VerificationCode = String.Empty
            Exit Sub
        End If
    End If

    If personType = 2 Then
        Me.View.VerificationCode = GetVerificationCode(nit)
    ElseIf personType = 1 AndAlso (identificationType = 0 OrElse identificationType = 7) Then
        Me.View.VerificationCode = GetVerificationCode(nit)
    Else
        Me.View.VerificationCode = 0
    End If
End Sub
```

El resto del presentador no cambia.

---

## Orden restante
1. Fix `Frm_Disposed`
2. Designer (4 bloques)
3. Parche `Calculate_VerificationCode` en `PThirdParty.vb`
4. Compilar + smoke test

## Smoke test
- NIT nuevo: doble ingreso; mismatch → "Las identificaciones no coincidieron"
- Búsqueda existente: sin reconfirmación
- Clase Nacional: máscara numérica + DV
- Clase Extranjero: alfanumérico, sin DV
- Guardar sin Clase: mensaje obligatorio
- CIIU / Actividad económica ocultos
- Nuevo/Deshacer: toolbar NewAndFind (no OnlyFind)
