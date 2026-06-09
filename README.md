# Service Desk Mini — MCP

A small Blazor support-ticket dashboard built with **Ignite UI for Blazor** using Ignite UI MCP guidance, Ignite UI skills and agents.md, theming instructions, and library-specific setup.

This repo is part of a comparison between an AI-generated Ignite UI app built **with MCP support** and the same task generated **without MCP support**.

## Purpose

This version shows how MCP guidance helps the AI assistant use Ignite UI for Blazor components more correctly and consistently.

## Built With

- Blazor
- Ignite UI for Blazor
- Ignite UI MCP server guidance
- Ignite UI theming guidance
- LocalStorage-based mock data

## Ignite UI Setup

This project is a minimal Blazor WebAssembly app with Ignite UI for Blazor wired in.

- `IgniteUI.Blazor` is referenced at version `25.2.32`.
- The Bootstrap light theme is loaded from `_content/IgniteUI.Blazor/themes/light/bootstrap.css`.
- Shared component imports live in `IgApplication/_Imports.razor`.
- Ignite UI component modules are registered in `IgApplication/Program.cs`.
- `NuGet.config` includes the Infragistics licensed feed required for current Ignite UI packages.

When adding a new Ignite UI component, add its module to `AddIgniteUIBlazor(...)` and the component will use the loaded theme styles.

## Features

- Dashboard page with KPI cards
- Ticket status and priority charts
- Tickets page with grid, filtering, sorting, and pagination
- Add, edit, delete, assign, and update ticket status
- Dialogs, inputs, buttons, badges/chips, and notifications
- Custom theme and responsive layout
- Mock data persisted in LocalStorage

## Comparison Context

This repository represents the **with-MCP** result from the benchmark.

The goal was to evaluate whether MCP guidance improves:

- Ignite UI component usage
- Correct API usage
- Theming consistency
- Feature completeness
- Build/runtime correctness
- Developer correction effort

## Notes

This is a demo/benchmark application. It uses mock data only and does not include a backend.
