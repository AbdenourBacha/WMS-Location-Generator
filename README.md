
# Dynamic Location Generator

A C# utility for dynamically generating location allocations based on a user-defined format. This tool is designed to automate the creation of location strings (e.g., A101AAA1, A101AAA2, ..., Z999BBE5) between a start and end position, adhering to a custom format like AABBCDDDE.

## Features
- **Custom Format Support**: Define your own location format (e.g., AABBCDDDE where A = Aisle, B = Shelf, C = Bin, etc.).
- **Dynamic Generation**: Generate all possible locations between a start and end position.
- **Validation**: Ensures start and end locations are valid and consistent with the format.
- **Scalable**: Efficiently handles large-scale location generation.

## How It Works
The utility works in three main steps:
1. **Format Parsing**: Splits the format into segments (e.g., AA, BB, C, DDD, E) and identifies whether each segment is alphanumeric.
2. **Numeric Conversion**: Converts each segment to a numeric value for easy range calculation.
3. **Location Generation**: Uses recursive logic to generate all possible combinations between the start and end positions.

## Usage
### Input
- **Format**: A string defining the location format (e.g., AABBCDDDE).
- **Start Location**: The starting location (e.g., A101AAA1).
- **End Location**: The ending location (e.g., Z999BBE5).

### Output
A list of all valid locations between the start and end positions.

### Example
```csharp
string format = "AABBCDDDE";
string startLocation = "A101AAA1";
string endLocation = "Z999BBE5";

List<string> locations = EmplacementHelper.Generate(format, startLocation, endLocation);

foreach (var location in locations)
{
    Console.WriteLine(location);
}
```

### Output
```
A101AAA1
A101AAA2
A101AAA3
...
Z999BBE5
```

## Code Structure
- **ConvertToNumericValue**: Converts a location string to a numeric value for calculations.
- **ConvertToLocationString**: Converts a numeric value back to a location string.
- **GenerateLocationList**: Generates all locations between a start and end position.
- **CombineSegments**: Combines segments into complete location strings.
- **GetStringSubStrings**: Splits a location string into segments based on the format.

## Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/your-username/dynamic-location-generator.git
    ```
2. Open the project in your preferred C# IDE (e.g., Visual Studio).
3. Build and run the project.

## Contributing
Contributions are welcome! If you have ideas for improvements or find any issues, please open an issue or submit a pull request.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
