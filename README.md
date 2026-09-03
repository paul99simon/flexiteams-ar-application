# FlexiTeams

FlexiTeams is a workforce and workflow modeling project for organizing resources, tasks, data, and workflows in a connected scenario. It combines a reusable C#/.NET library for validated scenario data with a Unity XR prototype for exploring that information in an interactive 3D environment.

<!-- Add project screenshots here. Suggested files: images/overview.png, images/workflow-graph.png, images/vr-interface.png -->
<!--
![FlexiTeams overview](images/overview.png)
![Workflow graph](images/workflow-graph.png)
![VR interface](images/vr-interface.png)
-->

<video src="./images/FlexiTeams-VR-Mockup.mp4" controls="controls" muted="muted" width="100%"></video>


## What it includes

- **Scenario model:** resources, professions, tasks, workflows, data, work agreements, and related metadata.
- **Graph representation:** an adjacency-list graph connecting workflow, task, resource, and data nodes.
- **XML import and export:** scenario files are validated against XML schemas before they are imported or written.
- **CSV support:** the core library includes CSV support for workflow-related data.
- **Unity XR prototype:** a 3D interface for viewing workflow layouts and inspecting resource, task, data, and workflow information.
- **Automated tests:** NUnit tests cover the data classes, graph, XML I/O, validation, and utility types.

## Repository structure

```text
.
├── flexiTeams/
│   ├── FlexiTeams/          # Reusable C#/.NET library
│   ├── FlexiTeamsTests/     # NUnit test project and XML fixtures
│   └── FlexiTeams.sln
├── unityProjectVR/          # Unity XR application and scene
├── images/                  # Project images and future README screenshots
└── models/                  # Shared or source 3D models
```

## Core library

The library targets **.NET Standard 2.1** and is organized around a small set of cooperating components:

| Area | Purpose |
| --- | --- |
| `DataClasses/` | Domain objects for resources, tasks, workflows, data, IDs, professions, and venues |
| `Inventory/` | Pools containing resources, tasks, workflows, and data |
| `Graph/` | Adjacency-list graph and typed graph nodes |
| `IO/` | XML import, export, schema validation, and import configuration |
| `ConstructionClasses/` | Builders and directors used while constructing domain objects |
| `Util/` | Time, ISO 8601, mapping, XML writing, and equality helpers |
| `Exceptions/` | Domain-specific validation and staffing exceptions |

### Import a scenario

An XML scenario can be loaded after validating it against the schemas referenced by the document:

```csharp
using FlexiTeams.IO;

var scenario = new Import("path/to/scenario.xml");

var resources = scenario.ResourcePool;
var tasks = scenario.TaskPool;
var workflows = scenario.WorkflowPool;
var data = scenario.DataPool;
var graph = scenario.Graph;
```

The test fixtures in `flexiTeams/FlexiTeamsTests/Resources/` provide example scenario XML and the associated schemas.

## Build and test

From the repository root:

```bash
dotnet restore flexiTeams/FlexiTeams.sln
dotnet build flexiTeams/FlexiTeams.sln
dotnet test flexiTeams/FlexiTeams.sln
```

The library project targets .NET Standard 2.1. The test project targets .NET 7 and uses NUnit.

## Unity XR application

The Unity project is a separate application that presents the FlexiTeams domain in an XR-oriented interface. It includes:

- OpenXR support and Unity XR Interaction Toolkit integration
- The `FlexiTeams-VR-Mockup` scene
- 3D workflow and task graph layout components
- UI panels for resources, tasks, workflows, and data
- XML scenario assets under `unityProjectVR/Assets/Resources/Xml/`

### Open the Unity project

1. Install **Unity 2021.3.27f1**.
2. Open `unityProjectVR/` in Unity Hub or the Unity Editor.
3. Open `Assets/Scenes/FlexiTeams-VR-Mockup.unity`.
4. Configure an OpenXR-compatible headset or use the available XR simulator tooling.
5. Enter Play Mode to run the prototype.

The Unity package manifest includes OpenXR, XR Management, XR Interaction Toolkit, TextMesh Pro, and the standard Unity UI modules.

## Scenario data

Scenario XML is structured into the following pools and graph sections:

```text
Scenario
├── ResourcePool
├── DataPool
├── WorkflowPool
├── TaskPool
└── Graph
    ├── Nodes
    └── Edges
```

The schemas used by the library and Unity sample are kept with the test and Unity resources. When creating new scenario files, use the existing fixtures as a starting point and keep IDs consistent between pool entries and graph references.

## Project status

This repository contains the FlexiTeams domain library, its test suite, and a Unity XR prototype. The Unity project is intended as an interactive visualization and exploration layer over the scenario model.