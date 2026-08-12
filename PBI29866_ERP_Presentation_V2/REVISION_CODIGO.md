# Revisión PBI 29866 — código pegado por Esteban

**Fecha revisión:** 2026-08-12  
**Verdict:** Casi listo — **1 fix obligatorio** en `Frm_Disposed`

| Archivo | Estado |
|---------|--------|
| `FrmThirdParty.Designer.vb` | **OK** — 4 cambios PBI aplicados |
| `FrmThirdParty.vb` | **OK** salvo `Frm_Disposed` |
| `PThirdParty.vb` | **OK** — DV por Clase aplicado |

---

## Designer — checklist (OK)

- [x] `INDlyItemClass` en `INDLyGrThirdPartyBasicData.Items`
- [x] `INDlyItemClass` **no** en `INDLyGrParameter.Items`
- [x] `INDLciCIIU.Visibility = Never`
- [x] `INDlygEconomicActivity.Visibility = Never`

Opcional (no bloquea): MistyRose / `SetCampoObligatorio(True)` en `INDsleClass`; `TextSize` de Class en básicos (205 vs 135).

---

## FrmThirdParty.vb — lo que está bien

- Flags `_pendingIdentification` / `_identificationConfirmed`
- `ConfirmIdentification` + mensaje mismatch
- `ApplyNitRulesByClass` con `EMask.Numerico` (Nacional) / alfanumérico (Extranjero)
- Clase obligatoria en `ValidateControlsForm` (Natural y Jurídico)
- Layout runtime: Clase en Segmento 1; CIIU / Actividad económica ocultos
- KeyDown: doble confirmación solo en registro nuevo
- Búsqueda / `ThirdParty_LoadControls` / `IdEntityLoaded`: sin reconfirmación
- `ClickDeshacer` → solo `CleanControls()` (no `OnlyFind` después)
- `CleanControls` resetea flags y vuelve a `NewAndFind`

---

## Único cambio pendiente (obligatorio)

En `Frm_Disposed`:

```vb
' Cambiar:
_identificationConfirmed = Nothing

' Por:
_identificationConfirmed = False
```

`Boolean` no admite `Nothing`; al cerrar el form puede fallar.

---

## PThirdParty.vb — OK

`Calculate_VerificationCode`:

- Sale si `Not FlagLoadControls`
- Clase 2 → limpia DV y sale
- Clase 1 + NIT no numérico → limpia DV
- Resto: lógica DV Jurídico / Natural intacta

---

## Smoke test (ERP) después del fix

1. NIT nuevo: Enter → pedir confirmación; mismatch → "Las identificaciones no coincidieron"
2. Búsqueda / carga existente: sin reconfirmación
3. Clase Nacional: máscara numérica + DV
4. Clase Extranjero: alfanumérico, sin DV
5. Guardar sin Clase: mensaje obligatorio
6. CIIU y Actividad económica ocultos
7. Nuevo / Deshacer → toolbar NewAndFind (no OnlyFind)
8. Cerrar form tras editar (valida el fix de Disposed)
