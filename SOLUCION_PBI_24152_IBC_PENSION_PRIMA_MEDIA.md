# Solución técnica — PBI 24152  
## Creación de campo Componente Prima Media (Editor de Expresiones)

| Campo | Valor |
|--------|--------|
| **Azure DevOps** | Product Backlog Item **24152** |
| **Título** | Creación de Campo componente prima media |
| **Padre** | 24034 — Reforma Pensional Ley 2381 de 2024 |
| **Prerrequisito** | PBI **27042** — Variable IBC de Componente Prima Media / Campo ACCAI en formulario liquidación de nómina |
| **Producto** | Vie HCM · Módulo Nómina · Colombia |
| **Normativa** | Ley 2381 de 2024, Artículo 20 — Obligatoriedad y monto de las cotizaciones |
| **Alcance** | Liquidación de nómina **y** liquidación de contrato |

---

## 1. Historia de usuario (resumen)

**Como** Responsable de Nómina  
**Quiero** generar el campo *(Componente prima media)* en el editor de expresiones  
**Para** cumplir el Art. 20 de la Ley 2381 de 2024 en liquidación de nómina y de contrato.

---

## 2. Qué pide exactamente el alcance

Se deben exponer **dos variables (Campos)** en el **Editor de Expresiones**, alimentadas desde la ventanilla **IBC - Control Nómina** del formulario **Liquidación de Nómina**:

| # | Nombre en editor de expresiones | Label origen (formulario) | Ventanilla |
|---|----------------------------------|---------------------------|------------|
| 1.1 | **IBC Pensión Prima Media** | **IBC Pensión - CPM** / **IBC Pensión CPM** | IBC - Control Nómina |
| 1.2 | **IBC Pensión ACCAI** | **IBC Pensión - ACCAI** / **IBC Pensión ACCAI** | IBC - Control Nómina |

> El prerrequisito 27042 ya dejó los labels/campos en el formulario. Este PBI **no recalcula** esos IBC: solo los **publica como Campos** del editor para armar fórmulas (nómina y contrato).

### Fórmula de referencia (ayudas visuales del PBI)

En las capturas del editor aparece como ejemplo de uso:

```text
= Salario mínimo * Pensión Prima Media
```

Eso es un **ejemplo de fórmula** que el usuario armaría con constantes/campos ya existentes.  
El entregable de este PBI son las **variables 1.1 y 1.2** (no reescribir esa fórmula por defecto en el motor, salvo que el analista lo pida aparte).

---

## 3. Contexto normativo (para entender el dato)

Ley 2381 / 2024 — cotización pensional con dos componentes sobre el IBC:

| Componente | Sigla en pantalla | Uso |
|------------|-------------------|-----|
| Prima Media | **CPM** | Componente de Prima Media |
| Ahorro Individual | **ACCAI** | Administradora del Componente Complementario de Ahorro Individual |

Por eso en **IBC - Control Nómina** aparecen, además de `IBC Pensión`:

- `IBC Pensión - CPM`
- `IBC Pensión - ACCAI`

---

## 4. Mapeo técnico recomendado

Usar nombres internos estables (código) y nombres visibles en español (UI del editor).

| Propiedad / clave interna sugerida | Display Name (Campos) | Origen UI | Tipo |
|------------------------------------|------------------------|-----------|------|
| `IbcPensionPrimaMedia` / `IbcPensionCpm` | IBC Pensión Prima Media | Label / control `IBC Pensión - CPM` | Decimal / Money |
| `IbcPensionAccai` | IBC Pensión ACCAI | Label / control `IBC Pensión - ACCAI` | Decimal / Money |

### Equivalencias de nombre (por si el código o el Designer difieren)

| Texto en PBI / captura | Tratarlo como |
|------------------------|---------------|
| IBC Pensión CPM | IBC Pensión - CPM |
| IBC Pensión Prima Media / Prima Medía | Misma variable (usar **Media**, sin tilde inconsistente) |
| IBC Pensión ACCAI / IBC Pensión - ACCAI | Misma variable |

---

## 5. Dónde tocar en el ERP (checklist de implementación)

Stack de la empresa: **VB.NET · WinForms · DevExpress · SQL Server · Azure DevOps**  
Estructura típica: `ERP_Presentation` / `ERP_Services` · módulos Presentation.Client / MVP.

### 5.1 Confirmar prerrequisito 27042

En **Liquidación de Nómina → IBC - Control Nómina** verificar que existan y se llenen:

1. Label/control **IBC Pensión - CPM**
2. Label/control **IBC Pensión - ACCAI**

Si están vacíos en una liquidación de prueba, el editor mostrará `0` o vacío: eso es tema del **cálculo** (27042 / liquidación), no del registro del Campo.

### 5.2 Registrar los Campos en el Editor de Expresiones

Buscar en el código del módulo Nómina (nombres orientativos; ajustar a los reales del repo):

| Qué buscar | Para qué |
|------------|----------|
| `Editor de Expresiones` / `ExpressionEditor` / `FrmExpression` | Formulario del editor |
| `Campos` / `Fields` / lista de variables IBC | Catálogo que llena la columna del medio |
| `[IBC Pensión]`, `[IBC Salud]`, `[IBC Periodo]` | Patrón de registro de Campos existentes |
| Contexto de evaluación de fórmulas en liquidación / contrato | Dónde se inyectan los valores al evaluar |

**Regla:** copiar el patrón exacto de un Campo IBC ya productivo (ej. `IBC Pensión` o `IBC Salud`) y duplicarlo para CPM y ACCAI.

### 5.3 Binding valor → contexto de expresión

Al evaluar una fórmula, el motor debe poder resolver:

```text
[IBC Pensión Prima Media]  →  valor del control/label IBC Pensión - CPM de la liquidación actual
[IBC Pensión ACCAI]        →  valor del control/label IBC Pensión - ACCAI de la liquidación actual
```

Aplicable tanto en:

- Liquidación de **nómina**
- Liquidación de **contrato** (mismo contexto IBC si el formulario lo comparte; si hay formulario aparte, repetir el binding)

### 5.4 Persistencia / catálogo (si los Campos vienen de BD)

Si el listado de `Campos` no es hardcode sino tabla/configuración, insertar/actualizar dos registros. Ejemplo genérico (adaptar nombres reales de tabla/columnas tras revisar el EDMX o la BD):

```sql
-- PLANTILLA — ajustar schema.tabla y columnas reales del módulo Nómina
-- Ejecutar primero en QA, nunca a ciegas en producción.

/*
INSERT INTO Nomina.ExpressionEditorField (Code, DisplayName, Category, SourceProperty, DataType, IsActive)
VALUES
  ('IbcPensionPrimaMedia', N'IBC Pensión Prima Media', 'Campos', 'IbcPensionCpm', 'Decimal', 1),
  ('IbcPensionAccai',      N'IBC Pensión ACCAI',       'Campos', 'IbcPensionAccai', 'Decimal', 1);
*/

-- Verificación sugerida:
-- SELECT Code, DisplayName, SourceProperty, IsActive
-- FROM Nomina.ExpressionEditorField
-- WHERE Code IN ('IbcPensionPrimaMedia', 'IbcPensionAccai');
```

### 5.5 Pseudocódigo VB.NET (patrón a seguir)

```vb
' Patrón ilustrativo — alinear con la clase real que registra Campos IBC
Public Sub RegisterPensionReformIbcFields(context As ExpressionContext, settlement As NominaSettlementDto)
    context.Fields.Add(
        name:="IBC Pensión Prima Media",
        value:=If(settlement.IbcPensionCpm, 0D))

    context.Fields.Add(
        name:="IBC Pensión ACCAI",
        value:=If(settlement.IbcPensionAccai, 0D))
End Sub
```

Si el catálogo se arma con tokens entre corchetes (como en la captura: `[IBC Pensión]`), mantener el mismo formato:

```text
[IBC Pensión Prima Media]
[IBC Pensión ACCAI]
```

---

## 6. Acceptance Criteria (oficiales del PBI)

Fuente: mensaje de Acceptance Criteria del backlog.

### AC1 — Creación de Variable "IBC Pensión Prima Media" en Editor de Expresiones

**Escenario:** Disponibilidad de nueva variable

| | |
|--|--|
| **Dado** | El usuario accede al editor de expresiones. |
| **Cuando** | Busca la variable **"IBC Pensión Prima Media"**. |
| **Entonces** | La variable debe estar disponible para su uso, mapeada al campo **"IBC Pensión CPM"** del formulario de liquidación de nómina (ventana **IBC Control Nómina**). |

### AC2 — Creación de Variable "IBC Pensión ACCAI" en Editor de Expresiones

**Escenario:** Disponibilidad de nueva variable

| | |
|--|--|
| **Dado** | El usuario accede al editor de expresiones. |
| **Cuando** | Busca la variable **"IBC Pensión ACCAI"**. |
| **Entonces** | La variable debe estar disponible para su uso, mapeada al campo **"IBC Pensión ACCAI"** del formulario de liquidación de nómina (ventana **IBC Control Nómina**). |

### Checklist de verificación (derivado de los AC)

| # | Verificar | Resultado esperado |
|---|-----------|--------------------|
| AC1.1 | Variable visible en categoría **Campos** | Aparece **IBC Pensión Prima Media** |
| AC1.2 | Mapeo de valor | Igual al campo/label **IBC Pensión CPM** (IBC Control Nómina) |
| AC1.3 | Uso en fórmula | Se puede insertar y aceptar (ej. `= [IBC Pensión Prima Media]`) |
| AC2.1 | Variable visible en categoría **Campos** | Aparece **IBC Pensión ACCAI** |
| AC2.2 | Mapeo de valor | Igual al campo/label **IBC Pensión ACCAI** (IBC Control Nómina) |
| AC2.3 | Uso en fórmula | Se puede insertar y aceptar (ej. `= [IBC Pensión ACCAI]`) |

> Regresión recomendada (no está en el AC literal, pero evita roturas): Campos IBC previos (`IBC Pensión`, `IBC Salud`, etc.) siguen funcionando.

---

## 7. Casos de prueba (datos de las capturas)

### Caso A — Empleado con IBC estándar

| Campo formulario | Valor captura |
|------------------|---------------|
| Empleado | PAULA ANDREA LARRARTE GARZON |
| Fecha | 28/02/2025 |
| IBC Periodo / IBC Pensión / IBC Salud | $ 1.718.000,00 |
| IBC Pensión - CPM | *(vacío en captura — validar tras 27042)* |
| IBC Pensión - ACCAI | *(vacío en captura — validar tras 27042)* |

**Esperado en editor:** cada variable refleja el mismo número que su label (o 0 si el label está vacío).

### Caso B — Salario integral

| Campo formulario | Valor captura |
|------------------|---------------|
| Empleado | GONZALO VIÑA AVILA |
| Fecha | 31/07/2024 |
| IBC Periodo / IBC Pensión | $ 14.506.800,00 |
| IBC Pensión - CPM / ACCAI | *(vacíos en captura)* |

Misma regla de paridad label ↔ variable.

### Caso C — Uso en fórmula

```text
= [IBC Pensión Prima Media] + [IBC Pensión ACCAI]
```

Resultado esperado ≈ `IBC Pensión` (cuando la reforma parte el IBC en CPM + ACCAI), o el total que defina la regla de negocio del 27042.

```text
= Salario mínimo * Pensión Prima Media
```

Solo aplica si existen los Campos/Constantes `Salario mínimo` y `Pensión Prima Media` en el catálogo; no sustituye a las variables 1.1 y 1.2.

---

## 8. Pasos operativos en Azure DevOps / Git

1. Crear rama desde la de desarrollo del módulo HCM/Nómina, ej. `feature/24152-ibc-pension-prima-media`.
2. Confirmar que 27042 está mergeado en el ambiente donde se prueba.
3. Implementar registro + binding (Presentation y/o Services según el patrón actual).
4. Compilar solución ERP_Presentation / ERP_Services.
5. Probar en QA con liquidación real (empleados casos A/B).
6. Adjuntar evidencia: captura del editor con los dos Campos + captura de IBC Control Nómina.
7. Vincular commit/PR al PBI **24152**.

---

## 9. Evidencia sugerida para el ticket

1. Screenshot Editor de Expresiones con **IBC Pensión Prima Media** y **IBC Pensión ACCAI** en **Campos**.
2. Screenshot Liquidación → **IBC - Control Nómina** mostrando labels CPM y ACCAI con valor.
3. Screenshot de una fórmula de prueba que use ambas variables y el resultado al liquidar.
4. Nota de prueba en contrato (si aplica formulario distinto).

---

## 10. Riesgos y notas

| Riesgo | Mitigación |
|--------|------------|
| 27042 incompleto → labels siempre vacíos | Bloquear cierre de 24152 hasta que 27042 entregue valores en QA |
| Nombre con tilde “Medía” vs “Media” | Estandarizar **Media** en display y en código |
| Editor de contrato no reutiliza el mismo catálogo | Revisar ambos entry-points del Expression Editor |
| Campos registrados pero sin binding | CA3/CA4 fallan aunque se vean en la lista — probar evaluación, no solo UI |

---

## 11. Definición de terminado (DoD)

- [ ] Variables visibles en categoría Campos  
- [ ] Valores iguales a labels CPM / ACCAI  
- [ ] Funciona en nómina y contrato  
- [ ] Regresión de Campos IBC previos OK  
- [ ] Evidencia en Azure DevOps 24152  
- [ ] Code review / merge según flujo del equipo HCM  

---

## 12. Archivos de apoyo en este repo

| Archivo | Uso |
|---------|-----|
| `ACCEPTANCE_CRITERIA_PBI_24152.md` | AC oficiales Dado/Cuando/Entonces |
| `mapas/PBI_24152_MAPEO_VARIABLES.md` | Tabla corta de mapeo label ↔ variable |
| `snippets/RegisterIbcPensionExpressionFields.vb` | Snippet VB.NET de registro |
| `sql/PBI_24152_ExpressionFields_TEMPLATE.sql` | Plantilla SQL si el catálogo es por BD |
| `BITACORA7_PBI_24152.md` | Filas listas para pegar en Bitácora 7 (SENA) |
