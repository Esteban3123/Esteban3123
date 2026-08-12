# PBI — Habilitar Facturación Tipo Mandato

Parámetro en **Parámetros de Facturación** (`FrmSettingBilling`) para habilitar factura electrónica tipo mandato.

| Campo | Valor |
|--------|--------|
| **Form** | `FrmSettingBilling` |
| **Entidad** | `SettingsBilling` |
| **Propiedad** | `EnableMandateBilling` |
| **UI** | Habilitar Facturación Tipo Mandato (Si/No, default No) |
| **Visible si** | `ApplyBasicBilling = True` |

## Empezar aquí

1. **[CHECKLIST.md](CHECKLIST.md)** — estado y orden de aplicación  
2. **[APLICAR.md](APLICAR.md)** — guía maestra  
3. Diffs por archivo en `snippets/*_APLICAR.md`

## Entregables

| Archivo | Uso |
|---------|-----|
| [CHECKLIST.md](CHECKLIST.md) | Estado ✅/⚠️ y bloqueos |
| [APLICAR.md](APLICAR.md) | Pasos 0–5 + A1–A7 |
| [ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md) | AC1–AC6 |
| [SOLUCION.md](SOLUCION.md) | Análisis técnico |
| [sql/Add_EnableMandateBilling_SettingsBilling.sql](sql/Add_EnableMandateBilling_SettingsBilling.sql) | Columna BIT default 0 |
| [snippets/ISettingBilling_APLICAR.md](snippets/ISettingBilling_APLICAR.md) | Diff exacto interfaz ✅ |
| [snippets/FrmSettingBilling_APLICAR.md](snippets/FrmSettingBilling_APLICAR.md) | Parches form ✅ |
| [snippets/SettingsBilling_APLICAR.md](snippets/SettingsBilling_APLICAR.md) | Entidad + BD ⚠️ |
| [snippets/FrmSettingBilling_Designer_APLICAR.md](snippets/FrmSettingBilling_Designer_APLICAR.md) | Designer ⚠️ |
| [snippets/Gate_FacturacionMandato.vb](snippets/Gate_FacturacionMandato.vb) | Gate AC6 |
| [mapas/UBICACION_UI.md](mapas/UBICACION_UI.md) | Ubicación mockup |
