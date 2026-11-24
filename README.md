# Sample Navisworks Project — Grouped Clash Report Exporter

![C#](https://img.shields.io/badge/C%23-.NET%20Framework%204.8-512BD4) ![Navisworks](https://img.shields.io/badge/target%20API-Navisworks%20Manage%202024-blue) ![HTML](https://img.shields.io/badge/reports-HTML-orange) ![License](https://img.shields.io/badge/license-MIT-green)

A sample Autodesk Navisworks Manage add-in demonstrating how to extract grouped clash detection results through the Navisworks .NET API and generate individual HTML reports for BIM coordination.

## Project status

**Development sample — not production-ready.** The current clash reader constructs test and group data but never adds the completed test objects to the returned list. As a result, the exporter currently creates the root output directory but receives no tests to report. The fix and additional development priorities are documented below.

## Features and intended workflow

- Access existing clash tests and groups from the active Navisworks document.
- Extract clash names, both model item display names, status and distance.
- Represent tests, groups and individual clashes through strongly typed C# data models.
- Generate a separate HTML report for each clash group, organized by clash test.
- Display clash names, item names and statuses in HTML tables.
- Separate data extraction, report generation and filesystem operations into dedicated classes.
- Inspect existing clash results without modifying clash tests or model geometry.

The tool exports existing groups; it does not automatically group clashes or run clash detection.

## Technical architecture

```text
Navisworks AddInPlugin
          |
ExportGroupedClashes.Execute
          |
ClashReaderService
          |
ClashTestData -> ClashGroupData -> ClashData
          |
HtmlReportBuilder + FileHelper
          |
HTML reports organized by test and group
```

## Requirements

| Component | Configuration |
| --- | --- |
| Target API | Autodesk Navisworks Manage 2024 |
| Framework | .NET Framework 4.8 |
| Language | C# |
| Platform | Windows |
| Development | Visual Studio with .NET desktop development tools and .NET Framework 4.8 targeting pack |
| Output | HTML files readable in a standard browser |

The checked-in project references Navisworks Manage 2024 API, Clash, COM API, Controls, COM interop and Clash Plugin assemblies. Other releases require version-specific references and testing.

## Build from source

```powershell
git clone https://github.com/youssefhesham98/SampleNavisworksProject.git
cd SampleNavisworksProject
```

1. Open `SampleNavisworksProject.sln` in Visual Studio.
2. Verify Autodesk assembly reference paths against your local Navisworks Manage 2024 installation.
3. Review the known issues and implement the reader fix below.
4. Replace the placeholder developer ID `YOURCOMPANY` with an appropriate unique developer identifier.
5. Build Release and validate the generated `SampleNavisworksProject.dll` in the target host.

No build or runtime testing was performed during this documentation update.

## Deployment

The repository does not currently include an installer or application bundle manifest. Prepare a Navisworks-compatible managed plugin deployment or an Autodesk application bundle targeting your installed release.

For conventional plugin-folder deployment, ensure the DLL and folder naming match the plugin registration requirements for the target release. The registered plugin name is `ExportGroupedClashes`, with the placeholder developer ID `YOURCOMPANY`. Verify the naming and installation location against the Navisworks SDK before distributing.

## Usage

After correcting the reader and successfully deploying the plugin:

1. Open a coordinated model in Navisworks Manage.
2. Run the required clash tests in Clash Detective.
3. Organize clash results into groups.
4. Launch **Export Grouped Clash Reports**.
5. Open the generated HTML reports from the desktop output folder.

## Output structure

The intended output is organized as follows (illustrative names):

```text
Desktop/
└── Navisworks_Clash_Reports/
    ├── MEP_vs_Structure/
    │   ├── Group_01.html
    │   └── Group_02.html
    └── Architecture_vs_MEP/
        └── Group_01.html
```

Each report contains a test heading, group heading, and a table with these columns:

| Clash | Item 1 | Item 2 | Status |
| --- | --- | --- | --- |
| Clash 001 | Duct | Beam | New |
| Clash 002 | Pipe | Wall | Active |

Example data only. Clash distance is collected in the data model but is not currently rendered in the HTML table.

## Repository structure

```text
SampleNavisworksProject/
├── SampleNavisworksProject.sln
├── SampleNavisworksProject/
│   ├── SampleNavisworksProject.csproj
│   ├── ExportGroupedClashes.cs    # Plugin entry point
│   ├── ClashReaderService.cs     # Clash test and group extraction
│   ├── ClashData.cs              # Data models
│   ├── HtmlReportBuilder.cs      # HTML generation
│   ├── FileHelper.cs             # Output directory management
│   └── Properties/
├── README.md
└── LICENSE.txt
```

## Known issues and development priorities

### Critical: empty report data

In `ClashReaderService.ReadGroupedClashes`, add the completed test object to the result list after processing its children and before leaving the outer test loop:

```csharp
result.Add(testData);
```

Without this statement, the method always returns an empty list. The success message in the plugin is also unconditional after export and does not verify that any reports were generated.

### Additional improvements

- Use the supplied Document argument consistently instead of reading Application.ActiveDocument again.
- Handle non-test saved items, folders, nested groups and ungrouped clash results explicitly.
- HTML-encode names and status values before interpolating them into report markup.
- Sanitize test and group names for invalid path characters and prevent path traversal or filename collisions. Current folder naming only replaces spaces, and group filenames are used directly.
- Include clash distance in the HTML output with verified units.
- Add export counts, exception handling and meaningful completion messages.
- Consider a report index, styling, configurable output folder and summary statistics.
- Add a validated deployment package and test against representative Navisworks documents.

These are proposed improvements, not implemented features.

## Contributing

Bug reports, improvements and contributions are welcome. Include the Navisworks version and reproduction steps, and avoid sharing confidential project models or clash data.

## License

Distributed under the [MIT License](LICENSE.txt). Autodesk API assemblies and third-party assets remain subject to their respective license terms.

## Author

**[Youssef Hesham](https://github.com/youssefhesham98)** — AEC Software Developer | BIM Developer.

Autodesk and Navisworks are trademarks of their respective owners.
