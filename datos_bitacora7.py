# =============================================================================
# Bitácora N° 7  |  07/08/2026 – 21/08/2026
# Esteban Andrés Perdomo Rojas — INDIRA HEALTHTECH S.A.S
# Festivos en el periodo: 07/08 (Boyacá) y 17/08 (Asunción) — no laborables
# =============================================================================

DATOS = {
    "bitacora_n": "7",
    "periodo_desde": "07/08/2026",
    "periodo_hasta": "21/08/2026",
    "fecha_entrega": "21/08/2026",
    # --- Aprendiz ---
    "nombre": "Esteban Andres Perdomo Rojas",
    "tipo_doc": "CC",
    "num_doc": "1077850163",
    "telefono": "3142032906",
    "correo_inst": "esteban_perdomo@soy.sena.edu.co",
    "correo_pers": "estebanperdomo56@gmail.com",
    "direccion": "Neiva Huila",
    "grupo": "2996976",
    "modalidad_formacion": "presencial",
    "programa": "Analisis y Desarrollo de Software",
    "modalidad_etapa": "Presencial",
    "exterior": "No",
    "pais": "Colombia",
    # --- Empresa ---
    "empresa": "INDIRA HEALTHTECH S.A.S",
    "nit": "900970948",
    "dir_empresa": "Cra 5A N° 22-31 Sevilla",
    # --- Jefe / Supervisor ---
    "jefe_nombre": "Diego Andres Roldan Lozano",
    "jefe_cargo": "VP of Technology",
    "jefe_tel": "3164187506",
    "jefe_correo": "droldan@indigo.tech",
    # --- Instructor SENA ---
    "instructor_nombre": "PAULO ANDRES RINCON",
    "instructor_correo": "princon@sena.edu.co",
    # --- Alternativa ---
    "alternativa": "Contrato de aprendizaje",
    # --- ARL ---
    "arl_afiliado": "SI",
    "arl_nivel": "I",
    "arl_corresponde": "SI",
    "arl_epp": "SI",
}

# Solo días hábiles (sin 07/08 ni 17/08 ni fines de semana)
ACTIVIDADES = [
    {
        "descripcion": (
            "Análisis del PBI 24152 (Reforma pensional Ley 2381 de 2024): creación "
            "del campo Componente Prima Media en el Editor de Expresiones de Nómina "
            "(Vie HCM). Se revisaron historia de usuario, prerrequisito 27042 y "
            "capturas de Liquidación de Nómina / IBC - Control Nómina."
        ),
        "competencias": (
            "Diseñar la solución de software de acuerdo con procedimientos y "
            "requisitos técnicos."
        ),
        "fecha_inicio": "10/08/26",
        "fecha_fin": "10/08/26",
        "evidencia": "Documento: análisis PBI 24152 / padre 24034 en Azure DevOps.",
        "observaciones": "Sin inasistencias. 07/08 festivo (Batalla de Boyacá).",
    },
    {
        "descripcion": (
            "Definición del mapeo técnico e implementación guiada de las variables "
            "IBC Pensión Prima Media e IBC Pensión ACCAI en el Editor de Expresiones, "
            "vinculadas a los labels IBC Pensión - CPM e IBC Pensión - ACCAI, con "
            "plantillas VB.NET y SQL para ERP_Presentation / ERP_Services."
        ),
        "competencias": (
            "Desarrollar la solución de software de acuerdo con el diseño y "
            "metodologías de desarrollo. Implementar la solución de software de "
            "acuerdo con los requisitos de operación y modelos de referencia."
        ),
        "fecha_inicio": "10/08/26",
        "fecha_fin": "12/08/26",
        "evidencia": "Producto: guía técnica, snippets VB.NET y plantilla SQL / PR documental.",
        "observaciones": "",
    },
    {
        "descripcion": (
            "Análisis e implementación del PBI 29866 en el maestro Personas y "
            "Terceros (FrmThirdParty / PThirdParty): reorganización de layout "
            "(Clase, NIT, segmentos Natural/Jurídico), validaciones NIT "
            "Nacional/Extranjero con dígito de verificación y doble digitación en altas."
        ),
        "competencias": (
            "Desarrollar la solución de software de acuerdo con el diseño y "
            "metodologías de desarrollo. Controlar la calidad del servicio de "
            "software de acuerdo con los estándares técnicos."
        ),
        "fecha_inicio": "11/08/26",
        "fecha_fin": "12/08/26",
        "evidencia": "Producto: ajustes en FrmThirdParty.vb / Designer / PThirdParty.vb.",
        "observaciones": "Tiempo aproximado: jornada de análisis y desarrollo UI/validaciones.",
    },
    {
        "descripcion": (
            "Análisis e implementación del parámetro Habilitar Facturación Tipo "
            "Mandato en Parámetros de Facturación (FrmSettingBilling): control "
            "DevExpress en Información Adicional, visibilidad condicionada a "
            "Aplica Facturación Básica, persistencia EnableMandateBilling y script "
            "SQL ALTER TABLE BIT DEFAULT 0."
        ),
        "competencias": (
            "Desarrollar la solución de software de acuerdo con el diseño y "
            "metodologías de desarrollo. Implementar la solución de software de "
            "acuerdo con los requisitos de operación y modelos de referencia."
        ),
        "fecha_inicio": "12/08/26",
        "fecha_fin": "12/08/26",
        "evidencia": "Documento/producto: parches VB.NET, script SQL y checklist de aceptación.",
        "observaciones": "Tiempo aproximado: 4 a 6 horas.",
    },
    {
        "descripcion": (
            "Documentación de Acceptance Criteria, checklist de verificación en QA "
            "y plan de pruebas para los PBI 24152 (Editor de Expresiones IBC CPM/ACCAI) "
            "y 29866 (Personas y Terceros), incluyendo casos de regresión y pasos de "
            "cierre en Azure DevOps."
        ),
        "competencias": (
            "Controlar la calidad del servicio de software de acuerdo con los "
            "estándares técnicos. Utilizar herramientas informáticas de acuerdo "
            "con las necesidades de manejo de información."
        ),
        "fecha_inicio": "13/08/26",
        "fecha_fin": "14/08/26",
        "evidencia": "Documento: criterios Dado/Cuando/Entonces y checklist QA.",
        "observaciones": "17/08 festivo (Asunción). Sin inasistencias en días hábiles.",
    },
]
