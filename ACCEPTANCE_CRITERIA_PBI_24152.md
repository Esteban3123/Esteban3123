# Acceptance Criteria — PBI 24152

Creación de campos / variables del Componente Prima Media en el **Editor de Expresiones** (Vie HCM · Nómina · Ley 2381 de 2024).

---

## 1. Creación de Variable "IBC Pensión Prima Media" en Editor de Expresiones

### Escenario: Disponibilidad de nueva variable

**Dado:** El usuario accede al editor de expresiones.  
**Cuando:** Busca la variable **"IBC Pensión Prima Media"**.  
**Entonces:** La variable debe estar disponible para su uso, mapeada al campo **"IBC Pensión CPM"** del formulario de liquidación de nómina (ventana **IBC Control Nómina**).

| Elemento | Valor |
|----------|--------|
| Nombre variable (editor) | IBC Pensión Prima Media |
| Campo origen (formulario) | IBC Pensión CPM |
| Formulario | Liquidación de nómina |
| Ventana | IBC Control Nómina |

---

## 2. Creación de Variable "IBC Pensión ACCAI" en Editor de Expresiones

### Escenario: Disponibilidad de nueva variable

**Dado:** El usuario accede al editor de expresiones.  
**Cuando:** Busca la variable **"IBC Pensión ACCAI"**.  
**Entonces:** La variable debe estar disponible para su uso, mapeada al campo **"IBC Pensión ACCAI"** del formulario de liquidación de nómina (ventana **IBC Control Nómina**).

| Elemento | Valor |
|----------|--------|
| Nombre variable (editor) | IBC Pensión ACCAI |
| Campo origen (formulario) | IBC Pensión ACCAI |
| Formulario | Liquidación de nómina |
| Ventana | IBC Control Nómina |

---

## Cómo evidenciar (QA)

1. Abrir **Liquidación de Nómina** → ventanilla **IBC Control Nómina** y anotar valores de **IBC Pensión CPM** e **IBC Pensión ACCAI**.  
2. Abrir el **Editor de Expresiones** → categoría **Campos**.  
3. Buscar / seleccionar **IBC Pensión Prima Media** → confirmar que está disponible y que al usarla toma el valor de **IBC Pensión CPM**.  
4. Buscar / seleccionar **IBC Pensión ACCAI** → confirmar que está disponible y que al usarla toma el valor de **IBC Pensión ACCAI**.  
5. Adjuntar capturas al PBI 24152.
