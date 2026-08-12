# Revisión PBI 29866 — estado

## Contexto
Esteban pegó el `FrmThirdParty.vb` **base en cero** (sin PBI) y pidió aplicar los requisitos.

El repo no contiene el ERP; el entregable es la guía de cambios lista para copiar.

## Entregable actual
- `CAMBIOS_SOBRE_TU_BASE.md` — parches exactos sobre el archivo que pegó
- `FrmThirdParty_APLICAR.md` — misma guía (versión anterior)
- `PThirdParty.vb` — presentador completo (copiar entero)
- `FrmThirdParty_Designer_APLICAR.md` + `FrmThirdParty_Designer_CODIGO.md`

## Orden de aplicación en ERP_Presentation_V2
1. `FrmThirdParty.vb` ← `CAMBIOS_SOBRE_TU_BASE.md`
2. `PThirdParty.vb` ← archivo completo de esta carpeta
3. `FrmThirdParty.Designer.vb` ← guía Designer
4. Compilar y smoke test manual

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
