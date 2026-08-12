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

1. **[APLICAR.md](APLICAR.md)** — parches exactos sobre el formulario que enviaste  
2. [ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md)  
3. [SOLUCION.md](SOLUCION.md)  

## Entregables

| Archivo | Uso |
|---------|-----|
| [APLICAR.md](APLICAR.md) | Pasos A1–A7 + Designer + BD + gate |
| [sql/Add_EnableMandateBilling_SettingsBilling.sql](sql/Add_EnableMandateBilling_SettingsBilling.sql) | Columna BIT default 0 |
| [snippets/FrmSettingBilling_EnableMandateBilling.vb](snippets/FrmSettingBilling_EnableMandateBilling.vb) | Fragmentos del form |
| [snippets/ISettingBilling_APLICAR.md](snippets/ISettingBilling_APLICAR.md) | Diff exacto de la interfaz |
| [snippets/ISettingBilling_EnableMandateBilling.vb](snippets/ISettingBilling_EnableMandateBilling.vb) | Bloque a pegar en `ISettingBilling` |
| [snippets/SettingsBilling_EnableMandateBilling.vb](snippets/SettingsBilling_EnableMandateBilling.vb) | Entidad |
| [snippets/FrmSettingBilling_Designer_APLICAR.md](snippets/FrmSettingBilling_Designer_APLICAR.md) | Designer |
| [snippets/Gate_FacturacionMandato.vb](snippets/Gate_FacturacionMandato.vb) | Gate AC6 |
| [mapas/UBICACION_UI.md](mapas/UBICACION_UI.md) | Ubicación en mockup |
