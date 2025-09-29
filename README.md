# Sistema de Matrícula Universitaria

## Descripción del Proyecto

Este es un sistema de matrícula universitaria desarrollado en C# que permite a los estudiantes registrarse, gestionar sus datos personales y realizar el proceso de matrícula académica de manera automatizada. El sistema está diseñado siguiendo el patrón de arquitectura Modelo-Vista-Controlador (MVC).

## Características Principales

### 🎓 Gestión de Usuarios
- **Registro de nuevos estudiantes** con validación completa de datos
- **Autenticación por DNI** para acceso al sistema
- **Actualización de datos personales** para usuarios existentes
- **Validaciones robustas** para DNI, email, teléfono, nombres, apellidos, edad, sexo y dirección

### 📚 Sistema de Matrícula
- **Selección de grado académico** de una lista de carreras disponibles
- **Elección de turno** (mañana o tarde)
- **Dos modalidades de selección de materias**:
  - Manual: El estudiante elige sus 6 materias
  - Automática: El sistema selecciona aleatoriamente 6 materias
- **Gestión completa de materias**: agregar, remover y actualizar
- **Resumen detallado** de la matrícula antes de confirmar
- **Validación de matrícula completa** (exactamente 6 materias sin repeticiones)

### 💾 Persistencia de Datos
- **Almacenamiento en JSON** para mantener los datos entre sesiones
- **Carga automática** de datos al iniciar el sistema
- **Guardado automático** después de cada operación

## Estructura del Proyecto

```
proyecto-final-terminado/
├── controlador/                    # Lógica de negocio
│   ├── ControladorUsuario.cs      # Gestión de usuarios y validaciones
│   ├── ControladorMatricula.cs    # Gestión de matrículas y materias
│   └── controlador.csproj         # Configuración del proyecto controlador
├── modelo/                        # Modelos de datos
│   ├── Usuario.cs                 # Clase modelo para usuarios
│   ├── Matricula.cs              # Clase modelo para matrículas
│   ├── Materia.cs                # Clase modelo para materias
│   ├── usuarios.json             # Base de datos JSON
│   └── modelo.csproj             # Configuración del proyecto modelo
├── proyecto-final/               # Interfaz de usuario (Vista)
│   ├── Program.cs                # Punto de entrada de la aplicación
│   ├── Iniciador.cs              # Lógica de interfaz y menús
│   └── proyecto-final.csproj     # Configuración del proyecto principal
└── proyecto-final.sln            # Archivo de solución
```

## Arquitectura MVC

### Modelo (modelo/)
- **Usuario.cs**: Define la estructura de datos del estudiante
- **Matricula.cs**: Define la estructura de la matrícula académica
- **Materia.cs**: Define la estructura de las materias/cursos
- **usuarios.json**: Almacena persistentemente todos los datos

### Vista (proyecto-final/)
- **Program.cs**: Punto de entrada principal
- **Iniciador.cs**: Maneja toda la interacción con el usuario a través de consola

### Controlador (controlador/)
- **ControladorUsuario.cs**: Lógica de negocio para gestión de usuarios
- **ControladorMatricula.cs**: Lógica de negocio para gestión de matrículas

## Funcionalidades Detalladas

### Validaciones Implementadas

#### Datos Personales
- **DNI**: Exactamente 8 dígitos numéricos
- **Nombres/Apellidos**: Solo letras y espacios, mínimo 2 caracteres
- **Edad**: Entre 16 y 100 años
- **Sexo**: Masculino o Femenino (case-insensitive)
- **Teléfono**: Mínimo 9 dígitos, acepta formato nacional e internacional
- **Email**: Formato válido con @ y dominio
- **Dirección**: Mínimo 5 caracteres

#### Datos Académicos
- **Grado**: Debe existir en la lista de grados disponibles
- **Materias**: Exactamente 6, sin repeticiones, deben existir en el sistema
- **Matrícula**: Validación completa de todos los campos

### Grados Académicos Disponibles
- Ingeniería de Sistemas
- Ingeniería Civil
- Medicina
- Derecho
- Administración
- Psicología
- Arquitectura
- Contabilidad

### Materias por Grado
Cada grado tiene un conjunto específico de materias con sus respectivos créditos académicos.

## Requisitos del Sistema

- **.NET Framework 4.7.2** o superior
- **Newtonsoft.Json 13.0.3** (incluido en packages/)
- **Visual Studio 2019** o superior (recomendado)

## Instalación y Ejecución

1. **Clonar o descargar** el proyecto
2. **Abrir** `proyecto-final.sln` en Visual Studio
3. **Restaurar paquetes NuGet** si es necesario
4. **Compilar** la solución (Build → Build Solution)
5. **Ejecutar** el proyecto principal (F5 o Ctrl+F5)

## Uso del Sistema

### Primer Uso
1. El sistema solicita tu DNI
2. Si no estás registrado, te guía por el proceso de registro
3. Completa todos los datos personales requeridos
4. Procede con el proceso de matrícula

### Usuario Existente
1. Ingresa tu DNI
2. El sistema te da la bienvenida
3. Puedes actualizar tus datos o proceder con la matrícula
4. Sigue el flujo de matrícula según tus preferencias

### Proceso de Matrícula
1. **Selecciona tu grado académico**
2. **Elige tu turno** (mañana/tarde)
3. **Selecciona el método de elección de materias**:
   - Manual: Elige tus 6 materias una por una
   - Automático: El sistema elige por ti
4. **Revisa el resumen** de tu matrícula
5. **Confirma o modifica** según sea necesario

## Contribuidores

Este proyecto fue desarrollado como trabajo final por un equipo de estudiantes:
- **Joel**: Arquitectura MVC y distribución de tareas
- **Lucinda**: Módulo de registro y gestión de usuarios
- **Andree**: Sistema de matrícula y selección de materias
- **Yacson**: Resumen de matrícula y confirmación final

## Tecnologías Utilizadas

- **Lenguaje**: C#
- **Framework**: .NET Framework 4.7.2
- **Persistencia**: JSON (Newtonsoft.Json)
- **Arquitectura**: Modelo-Vista-Controlador (MVC)
- **IDE**: Visual Studio

## Notas Técnicas

- Los datos se guardan automáticamente en `usuarios.json`
- El sistema valida todos los datos de entrada
- La arquitectura MVC permite fácil mantenimiento y extensión
- Cada usuario puede tener solo una matrícula activa
- Las materias se asignan según el grado académico seleccionado

---

*Sistema desarrollado como proyecto final de curso - Universidad*