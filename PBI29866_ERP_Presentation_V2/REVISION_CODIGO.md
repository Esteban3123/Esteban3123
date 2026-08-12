# Revisión PBI 29866 — estado (última revisión)

## FrmThirdParty.vb — APROBADO con 1 corrección

El `.vb` que pegó Esteban ya incluye casi todo el PBI:

| Requisito | Estado |
|-----------|--------|
| Flags `_pendingIdentification` / `_identificationConfirmed` | OK |
| Gets null-safe | OK |
| `INDsleClass.Enabled` en ActionsOnControls | OK |
| `CleanControls` con flags + ocultar CIIU/Actividad | OK |
| Clase obligatoria Natural y Jurídico | OK |
| `.Class = ClassThirdParty` en Natural | OK |
| `ApplyNitRulesByClass` con `EMask.Numerico` | OK |
| `ConfirmIdentification` + KeyDown | OK |
| Búsqueda / Load / IdEntityLoaded sin reconfirmar | OK |
| Layout runtime (Clase → Segmento 1) | OK |
| `ClickDeshacer` solo `CleanControls` | OK |

### Corrección obligatoria en `Frm_Disposed`

```vb
' MAL (Boolean no admite Nothing):
_identificationConfirmed = Nothing

' BIEN:
_identificationConfirmed = False
```

### Opcional (más seguro en layout)

En `INDslePersonTypeEditValueChangedManual`, preferir:

```vb
If INDlyItemClass.Parent IsNot Nothing AndAlso INDlyItemClass.Parent IsNot INDLyGrThirdPartyBasicData Then
    INDlyItemClass.Parent.Remove(INDlyItemClass)
    INDLyGrThirdPartyBasicData.Add(INDlyItemClass)
End If
```

en lugar de `INDLyGrParameter.Remove(INDlyItemClass)` (falla si Clase ya no está en Parámetros tras el Designer).

---

## FrmThirdParty.Designer.vb — PENDIENTE (sigue en baseline)

El Designer pegado **aún no** tiene los 4 cambios. Aplicar `FrmThirdParty_Designer_APLICAR.md`:

1. Agregar `Me.INDlyItemClass` al `Items` de `INDLyGrThirdPartyBasicData`
2. Quitar `Me.INDlyItemClass` del `Items` de `INDLyGrParameter`
3. `INDLciCIIU.Visibility = Never`
4. `INDlygEconomicActivity.Visibility = Never`

---

## PThirdParty.vb — pendiente de pegar para revisión

Copiar el archivo completo de esta carpeta (`PThirdParty.vb`). Debe omitir DV cuando Clase = Extranjero.

---

## Orden restante
1. Fix `Frm_Disposed` → `_identificationConfirmed = False`
2. Aplicar 4 parches Designer
3. Copiar `PThirdParty.vb` y pegarlo aquí para revisión
4. Compilar + smoke test

## Smoke test
- NIT nuevo: doble ingreso; mismatch → "Las identificaciones no coincidieron"
- Búsqueda existente: sin reconfirmación
- Clase Nacional: máscara numérica + DV
- Clase Extranjero: alfanumérico, sin DV
- Guardar sin Clase: mensaje obligatorio
- CIIU / Actividad económica ocultos
- Nuevo/Deshacer: toolbar NewAndFind (no OnlyFind)

## Nota EMask
Usar `EMask.Numerico` (no `.Numeros` → BC30456).
